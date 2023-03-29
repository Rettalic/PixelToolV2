using System.Linq;
using UnityEngine;

public class DrawingCanvas : MonoBehaviour
{
    public Vector2Int canvasScale;
  
    public Texture2D texture;
    public SpriteRenderer spriteRenderer;
       
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = GenerateSprite(canvasScale.x, canvasScale.y);
        spriteRenderer.size = new Vector2(canvasScale.x, canvasScale.y);
        UpdateSize();
    }

    private Sprite GenerateSprite(int _width, int _height)
    {
        texture = new Texture2D(_width, _height)
        {
            name = name,
            filterMode = FilterMode.Point,
        };

        Color[] pixels = Enumerable.Repeat(Color.white, texture.width * texture.height).ToArray();
        texture.SetPixels(pixels);
        texture.Apply();

        Rect spriteRect = new Rect(0, 0, texture.width, texture.height);
        Sprite sprite = Sprite.Create(texture, spriteRect, Vector2.one * 0.5f, _width);
        sprite.name =  "Sprite";

        
        return sprite;
    }

    public void UpdateSize()
    {
        var bounds =  spriteRenderer.sprite.bounds;
        var factor = 1 / bounds.size.y;
        transform.localScale = new Vector3(factor, factor, factor);
    }

    public void LoadTexture(byte[] data)
    {
        if(data != null)
        {
            texture.LoadImage(data);
            spriteRenderer.size = new Vector2(canvasScale.x, canvasScale.y);
        }
    } 
}