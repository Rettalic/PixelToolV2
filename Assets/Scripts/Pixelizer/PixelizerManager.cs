using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PixelizerManager : MonoBehaviour
{
    [SerializeField] private Material pixelizeMaterial;
    [SerializeField] private Texture texture;
    [SerializeField] private int pixelSize;
    private DynamicPixelizer dynamicPixelizer;

    public TMP_Text text;
    public Slider slider;

    private void Awake()
    {
        dynamicPixelizer = new DynamicPixelizer();
        UpdateText(slider.value);
        slider.onValueChanged.AddListener(UpdateText);
    }

    private void UpdateText(float _val)
    {
        text.text = slider.value.ToString("00");
        pixelSize = (int)_val;
        Pixelate();
    }

    public void Pixelate()
    {
        pixelizeMaterial.SetTexture("_MainTex", dynamicPixelizer.Pixelize(texture, pixelSize));
    }
}
