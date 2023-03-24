using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PixelizerManager : MonoBehaviour
{
    private DynamicPixelizer dynamicPixelizer;
    [SerializeField] private int pixelSize;
    public DrawingCanvas drawCanvas;
    public PaletteSwapper swapper;
  
    public Vector2Int imgSize;


    public Texture inputTexture;    //Texture you import
    public RawImage SourceTexture;  //Original img
    public RawImage outputTexture;  //Pixelate img


    public TMP_Text text;
    public Slider slider;

    private void Awake()
    {
        dynamicPixelizer = new DynamicPixelizer();
        UpdateText(slider.value);
        slider.onValueChanged.AddListener(UpdateText);
    }

    public void UpdateText(float _val)
    {
        text.text = slider.value.ToString("00");
        pixelSize = (int)_val;
        Pixelate();

        
    }

    public void Pixelate()
    {
        if(inputTexture != null)
        {
            outputTexture.texture = dynamicPixelizer.Pixelize(inputTexture, pixelSize);
            swapper.inputTexture = outputTexture.texture;
            
            

            if (SourceTexture.texture != inputTexture)
            {
                SourceTexture.texture = inputTexture;
            }
        }
    }
}
