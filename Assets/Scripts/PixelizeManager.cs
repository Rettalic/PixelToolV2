using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PixelizeManager : MonoBehaviour
{
    public  Texture2D sourceTexture;
    public Texture2D inputTexture;
    public RawImage outputImage;

    public TMP_Text text;
    public Slider slider;

    public int size;
    public byte[] image;

    public DrawingCanvas drawingCanvas;


    private void Awake()
    {
        UpdateText(slider.value);
        slider.onValueChanged.AddListener(UpdateText);
    }

    public void PixelizeImage()
    {
        inputTexture = new Texture2D((int)(size), (int)(size));

        float xScale = sourceTexture.width / (float)size;
        float yScale = sourceTexture.height / (float)size;

        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < Mathf.FloorToInt(size); y++)
            {
                Color pixelColor = sourceTexture.GetPixel((int)((x + 0.5f) * xScale), (int)((y + 0.5f) * yScale));
                inputTexture.SetPixel(x, y, pixelColor, 0);
            }
        }

        inputTexture.Apply();
        inputTexture.filterMode = FilterMode.Point;

        outputImage.texture = inputTexture;
    }

    public void UpdateText(float _val)
    {
        text.text = slider.value.ToString("00");
        Debug.Log(Mathf.Pow(2, _val));
        size = (Mathf.FloorToInt(Mathf.Pow(2, _val)));
        PixelizeImage();
    }

    public void updateBut()
    {
            Sprite sprite = Sprite.Create(inputTexture,
            new Rect(0, 0, inputTexture.width, inputTexture.height),
            new Vector2(0.5f, 0.5f) // Pivot point in the center of the sprite
            );

        // Apply the sprite to the SpriteRenderer

        drawingCanvas.spriteRenderer.sprite = sprite;

    }
}
