using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaletteSwapper : MonoBehaviour 
{
    public Shader paletteShader;

    public Texture inputTexture;
    public Texture colorPalette;
    public bool invert = false;
    
    public Material resultMaterial;

    private RenderTexture sourceRT;
    private RenderTexture endRT;

    private Material paletteMat;

    //Gradient implementeren
    public Gradient test;
    
    void OnEnable() 
    {
        paletteMat ??= new Material(paletteShader);
        paletteMat.hideFlags = HideFlags.HideAndDontSave;

        sourceRT = new RenderTexture(200, 200, 16);
        endRT    = new RenderTexture(200, 200, 16);
    }


    private void Update()
    {
        Graphics.Blit(inputTexture, sourceRT);
        PaletteSwap(sourceRT, endRT);
        resultMaterial.SetTexture("_MainTex", endRT);
    }

    public void PaletteSwap(RenderTexture source, RenderTexture destination) {
        paletteMat.SetTexture("_ColorPalette", colorPalette);
        paletteMat.SetInt("_Invert", invert ? 1 : 0);
        Graphics.Blit(source, destination, paletteMat);
    }
}
