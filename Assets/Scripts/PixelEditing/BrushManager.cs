using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BrushManager : MonoBehaviour
{
    [SerializeField] public Color drawColour = Color.black;
    public int drawSize = 1;

    private Vector2Int textureCoord;      
    private Vector2Int lastTexureCoord;

    private Color[] originalColour;
    private Color[] backupColour;

    private Texture2D texture;
    private RaycastHit2D hit;


    public TMP_Text text;
    public Slider slider;

    private void Awake()
    {
        UpdateText(slider.value);
        slider.onValueChanged.AddListener(UpdateText);
    }

    private void Update()
    {
        if (texture != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                backupColour = texture.GetPixels();
            }
        }

        if (!hit) lastTexureCoord = textureCoord;
        
        hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit)
        {
            SpriteRenderer sr = hit.transform.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                texture = sr.sprite.texture;

                Vector2 coord = (Vector2) hit.transform.position - hit.point;
                coord *= -1;
                coord += Vector2.one * 0.5f;

                coord.x *= texture.width;
                coord.y *= texture.height;

                textureCoord.x = (int) coord.x;
                textureCoord.y = (int) coord.y;
                
                if (Input.GetMouseButton(0))
                {
                    PlotLine(lastTexureCoord, textureCoord, texture, drawColour);
                    texture.Apply();
                }

                if (Input.GetMouseButtonUp(0))
                {
                    originalColour = texture.GetPixels();
                    DrawCommand(originalColour, backupColour, texture);
                }
            }
        }
    }
    
    private void LateUpdate()
    {
        lastTexureCoord = textureCoord;
    }
    
    private void DrawCommand(Color[] original, Color[] backup, Texture2D texture)
    {
        Draw draw = new Draw(original, backup, texture);
        CommandHandler.Add(draw);
    }


    //This uses the Bresenham's Line Algorithm: https://en.wikipedia.org/wiki/Bresenham%27s_line_algorithm 
    private void PlotLine(Vector2Int start, Vector2Int end, Texture2D tex, Color color)
    {
        int dx = Mathf.Abs(end.x - start.x);  //Absolute difference between x-coordinates
        int dy = -Mathf.Abs(end.y - start.y); //Absolute difference between y-coordinates
       
        int sx = start.x < end.x ? 1 : -1;    //set sx to either 1 or -1 to check if the start is either on the right or left side
        int sy = start.y < end.y ? 1 : -1;    //set sy to either 1 or -1 to check if the start is either on the top or bottom side
       
        int error = dx + dy;                  //Determine the next pixel to draw

        while (true)
        {
            DrawCircle(tex, color, start, drawSize);
            if (start.x == end.x && start.y == end.y) break; //End loop if reached the end
            
            int e2 = 2 * error; //Calculate error term of algorithm
            if (e2 >= dy)       //If error term >= than the change in y, move into x                         
            {
                if (start.x == end.x) break;
                error += dy;    //Update Error term
                start.x += sx;  //move x
            }

            if (e2 > dx) continue;
            if (start.y == end.y) break;
            error += dx;    
            start.y += sy;  //move y
        }
    }
    
    private void DrawCircle(Texture2D _tex, Color _colour, Vector2Int _pos, int _radius)
    {
        float rSquared = _radius * _radius;

        for (int x = _pos.x - _radius; x < _pos.x + _radius + 1; x++)
        {
            for (int y = _pos.y - _radius; y < _pos.y + _radius + 1; y++)
            {
                //Original formula of calculating what is inside a circle (X1-X2)^2 + (Y1-Y2)^2 = r^2 
                if (!((_pos.x - x) * (_pos.x - x) + (_pos.y - y) * (_pos.y - y) < rSquared)) continue; //check if pixel is within the radius

                if (x < 0 || y < 0 || x >= _tex.width || y >= _tex.height) continue; //check if it is within the bounds of the canvas

                _tex.SetPixel(x, y, _colour); //Apply the new colour to the coordinate
            }
        }
    }

    public void UpdateText(float _val)
    {
        text.text = slider.value.ToString("00");
        drawSize = (int)_val;
    }
}
