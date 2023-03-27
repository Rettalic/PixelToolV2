using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ExportPng : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Sprite itemBGSprite = Resources.Load<Sprite>("_Defaults/Item Images/_Background");
        Texture2D itemBGTex = itemBGSprite.texture;
        byte[] itemBGBytes = itemBGTex.EncodeToPNG();
        File.WriteAllBytes(formattedCampaignPath + "/Item Images/Background.png", itemBGBytes);
    }
}
