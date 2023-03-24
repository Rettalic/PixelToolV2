using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

public class DrawingCanvas : MonoBehaviour
{
    [SerializeField] public Vector2Int canvasScale;
  
    public Texture2D texture;
    public BoxCollider2D collider;
    public SpriteRenderer spriteRenderer;
       
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = GenerateSprite(canvasScale.x, canvasScale.y);
        spriteRenderer.size = new Vector2(canvasScale.x, canvasScale.y);

    }

    private Sprite GenerateSprite(int width, int height)
    {
        texture = new Texture2D(width, height)
        {
            name = name,
            filterMode = FilterMode.Point,
        };

        Color[] pixels = Enumerable.Repeat(Color.white, texture.width * texture.height).ToArray();
        texture.SetPixels(pixels);
        texture.Apply();

        Rect spriteRect = new Rect(0, 0, texture.width, texture.height);
        Sprite sprite = Sprite.Create(texture, spriteRect, Vector2.one * 0.5f, width);
        sprite.name =  "Sprite";

        
        return sprite;
    }

    private void Update()
    {
        var bounds =  spriteRenderer.sprite.bounds;
        var factor = 1 / bounds.size.y;
        transform.localScale = new Vector3(factor, factor, factor);
    }

    private Texture2D GenerateTex(int width, int height)
    {
        texture = new Texture2D(width, height)
        {
            name = name,
            filterMode = FilterMode.Point,
        };

       

        return texture;
    }


    public void LoadTexture(byte[] data)
    {
        if(data != null)
        {
            texture.LoadImage(data);
            spriteRenderer.size = new Vector2(canvasScale.x, canvasScale.y);
        }
    }

    public Texture2D GetTexture()
    {
        return texture;
    }

 
}