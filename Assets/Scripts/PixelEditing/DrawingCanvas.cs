using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

public class DrawingCanvas : MonoBehaviour
{
    [SerializeField] private Vector2Int canvasScale;
  
    public Texture2D texture;
    
    private void Start()
    {
        SpriteRenderer rawImage = GetComponent<SpriteRenderer>();
        rawImage.sprite = GenerateSprite(canvasScale.x, canvasScale.y);
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

    
    public void LoadTexture(byte[] data)
    {
        if(data != null)
        {
            texture.LoadImage(data);
        }
    }
    public void LoadTexture(RenderTexture data)
    {
        if (data != null)
        {            
            RenderTexture.active = data;
            Texture2D tex = new Texture2D(1024, 1024);
            tex.ReadPixels(new Rect(0, 0, 1024, 1024), 0, 0);
            tex.Apply();
            RenderTexture.active = null;

        }
    }

    public Texture2D GetTexture()
    {
        return texture;
    }

 
}