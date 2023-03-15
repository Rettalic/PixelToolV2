using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PixelateEffect : MonoBehaviour
{
    public Slider slider;
    public TMP_Text text;
    public Material pixelateMaterial;
    public float pixelSize = 10;

    private void Start()
    {
        UpdateText(slider.value);
        slider.onValueChanged.AddListener(UpdateText);
    }

    public void Pixelate()
    {
        pixelateMaterial.SetFloat("_PixelSize", pixelSize);
    }

    private void UpdateText(float _val)
    {
        text.text = slider.value.ToString("0,00");
        pixelSize = (int)_val;
        Pixelate();
    }
 
}
