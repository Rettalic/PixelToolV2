﻿
using AnotherFileBrowser.Windows;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.IO;

public class FileBrowserUpdate : MonoBehaviour, IDataPersistence
{
    public byte[] imgData;

    public DrawingCanvas drawCanvas;

    public void OpenFileBrowser()
    {
        var bp = new BrowserProperties();
        bp.filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
        bp.filterIndex = 0;

        new FileBrowser().OpenFileBrowser(bp, path =>
        {
            //Load image from local path with UWR
            StartCoroutine(LoadImage1(path));
        });
    }

    private IEnumerator LoadImage1(string _path)
    {
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(_path))
        {
            yield return uwr.SendWebRequest();

        #pragma warning disable CS0618 // Type or member is obsolete
            if (uwr.isNetworkError || uwr.isHttpError)
            {
                Debug.Log(uwr.error);
            }
            else
            {
                Texture2D uwrTexture = DownloadHandlerTexture.GetContent(uwr);
                imgData = uwrTexture.EncodeToPNG();
                drawCanvas.LoadTexture(imgData);
            }
        #pragma warning restore CS0618 // Type or member is obsolete
        }
    }

    private void Start()
    {
        LoadImage();    
    }

    public void LoadImage()
    {
        Texture2D tex = new Texture2D(1, 1);
        if (imgData != null)
        {
            tex.LoadImage(imgData);
          //  PixelizerManager.inputTexture = tex;
          //  PixelizerManager.Pixelate();
            
        }
    }

    public void LoadData(ToolData _data)
    {
        imgData = _data.imgBytes;
    }

    public void SaveData(ToolData _data)
    {
        _data.imgBytes = imgData;
    }
}
