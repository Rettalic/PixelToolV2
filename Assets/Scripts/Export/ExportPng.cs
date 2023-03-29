using UnityEngine;
using System.IO;

public class ExportPng 
{
    public void SaveImageToFile(Texture2D _texture)
    {
        string filePath = Application.dataPath + "/Results";
        byte[] bytes = _texture.EncodeToPNG();
        if (!Directory.Exists(filePath)) Directory.CreateDirectory(filePath);
        filePath = filePath + "/Results" + ".png";
        File.WriteAllBytes(filePath, bytes);
        // Open File in saved location
        filePath = filePath.Replace(@"/", @"\");
        System.Diagnostics.Process.Start("explorer.exe", "/select," + filePath);
    }
}
