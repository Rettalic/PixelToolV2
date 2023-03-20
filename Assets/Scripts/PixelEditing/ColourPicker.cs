using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ColourPicker : MonoBehaviour, IPointerClickHandler
{
    public Color colourOutput;

    public Image imgTest;
    
    public BrushManager brushManager;


    public void OnPointerClick(PointerEventData eventData)
    {
        colourOutput = Pick(Camera.main.WorldToScreenPoint(eventData.position), GetComponent<Image>());

        if(colourOutput.a == 0)
        {
            return;
        }

        imgTest.color = colourOutput;
        brushManager.drawColour = colourOutput;
    }

    Color Pick(Vector2 screenPoint, Image imageToPick)
    {
        Vector2 point;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(imageToPick.rectTransform, screenPoint, Camera.main, out point);
        point += imageToPick.rectTransform.sizeDelta / 2;
        Texture2D tex = GetComponent<Image>().sprite.texture;
        Vector2Int m_point = new Vector2Int((int)((tex.width * point.x) / imageToPick.rectTransform.sizeDelta.x), (int)((tex.height * point.y) / imageToPick.rectTransform.sizeDelta.y));

        return  tex.GetPixel(m_point.x, m_point.y);
    }
}
