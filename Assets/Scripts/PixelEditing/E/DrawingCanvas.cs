using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

public class DrawingCanvas : MonoBehaviour
{
    [SerializeField] private Vector2Int canvasScale;
    private Texture2D texture;
    
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

        var pixels = Enumerable.Repeat(Color.white, texture.width * texture.height).ToArray();
        texture.SetPixels(pixels);
        texture.Apply();

        Rect spriteRect = new Rect(0, 0, texture.width, texture.height);
        Sprite sprite = Sprite.Create(texture, spriteRect, Vector2.one * 0.5f, width);
        sprite.name =  "Sprite";

        return sprite;
    }

    
    public void LoadTexture(byte[] data)
    {
        texture.LoadImage(data);
    }

    public Texture2D GetTexture()
    {
        return texture;
    }

 
}