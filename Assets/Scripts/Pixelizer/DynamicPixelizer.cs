using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class DynamicPixelizer
{
    private ComputeShader pixelShader;

    private int pixelizeKernelID;       //ID to function
    private int readPixelizedKernelID;  //ID to function
    
    private Vector3 threadGroupSize; //basically how many times it runs the forloop

    private CustomRenderTexture rt; //Original texture
    
    //3 different textures because InterlockedAdd takes only 1 parameter.
    private CustomRenderTexture colourR;
    private CustomRenderTexture colourG;
    private CustomRenderTexture colourB;
    
    public DynamicPixelizer()
    {
        pixelShader           = Resources.Load<ComputeShader>("DynamicPixelizer"); //get from resource folder
        pixelizeKernelID      = pixelShader.FindKernel("Pixelize"); //allocate function to ID
        readPixelizedKernelID = pixelShader.FindKernel("ReadPixelized");
    }

    public CustomRenderTexture Pixelize(Texture texture, int pixelSize)
    {
        //If texture doesn't exist, create it.
        if (rt == null) CreateRT(texture.width, texture.height);

        //If texture size doesn't match, make it match
        if (rt.width != texture.width || rt.height != texture.height) CreateRT(texture.width, texture.height);
        
        //Copy texture to rendertexture
        Graphics.Blit(texture, rt);

        if (pixelSize <= 1) return rt; 
        
        //Clear colours (clean sheet)
        colourR.Release();
        colourG.Release();
        colourB.Release();

        //Calculate how many times to run the shader with the NumThreads.
        pixelShader.GetKernelThreadGroupSizes(pixelizeKernelID, out uint threadGroupSizeX, out uint threadGroupSizeY, out _);
        threadGroupSize.x = Mathf.CeilToInt((float)texture.width  / threadGroupSizeX);
        threadGroupSize.y = Mathf.CeilToInt((float)texture.height / threadGroupSizeY);
        
        //set shader values
        pixelShader.SetInt("_PixelSize", pixelSize);
        
        pixelShader.SetTexture(pixelizeKernelID, "_Source", rt); //set texture for kernel function
        pixelShader.SetTexture(pixelizeKernelID, "_ResultR", colourR);
        pixelShader.SetTexture(pixelizeKernelID, "_ResultG", colourG);
        pixelShader.SetTexture(pixelizeKernelID, "_ResultB", colourB);
        pixelShader.Dispatch(pixelizeKernelID, (int)threadGroupSize.x, (int)threadGroupSize.y, 1); //run and apply function
        
        pixelShader.SetTexture(readPixelizedKernelID, "_Source", rt);
        pixelShader.SetTexture(readPixelizedKernelID, "_ResultR", colourR);
        pixelShader.SetTexture(readPixelizedKernelID, "_ResultG", colourG);
        pixelShader.SetTexture(readPixelizedKernelID, "_ResultB", colourB);
        pixelShader.Dispatch(readPixelizedKernelID, (int)threadGroupSize.x, (int)threadGroupSize.y, 1);

        return rt;
    }

    // Create RenderTexture
    private void CreateRT(int textureWidth, int textureHeight)
    {
        rt = new CustomRenderTexture(textureWidth, textureHeight, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear)   {enableRandomWrite = true};
        
        colourR = new CustomRenderTexture(textureWidth, textureHeight, RenderTextureFormat.RInt, RenderTextureReadWrite.Linear) {enableRandomWrite = true};
        colourG = new CustomRenderTexture(textureWidth, textureHeight, RenderTextureFormat.RInt, RenderTextureReadWrite.Linear) {enableRandomWrite = true};
        colourB = new CustomRenderTexture(textureWidth, textureHeight, RenderTextureFormat.RInt, RenderTextureReadWrite.Linear) {enableRandomWrite = true};
    }
}
