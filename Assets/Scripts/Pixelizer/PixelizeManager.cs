using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PixelizeManager : MonoBehaviour, IDataPersistence
{
    public Texture2D sourceTexture;
    public Texture2D inputTexture;

    public Text text;
    public Slider slider;

    public int sliderValue;
    public int size;
    public byte[] image;

    public DrawingCanvas drawingCanvas;
    private ExportPng exportPng;

    private void Awake()
    {
        UpdateText(slider.value);
        slider.onValueChanged.AddListener(UpdateText);
        exportPng = new ExportPng();     
        if(image != null)
        {
            inputTexture.LoadImage(image);
        }
        PixelizeImage();
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

        Sprite sprite = Sprite.Create(inputTexture, new Rect(0, 0, inputTexture.width, inputTexture.height), new Vector2(0.5f, 0.5f));
        image = inputTexture.EncodeToPNG();
        drawingCanvas.spriteRenderer.sprite = sprite;
        drawingCanvas.UpdateSize();
    }

    public void UpdateText(float _val)
    {
        text.text = slider.value.ToString("00");
        sliderValue =(int)_val;
        Debug.Log(Mathf.Pow(2, _val));
        size = (Mathf.FloorToInt(Mathf.Pow(2, _val)));
        PixelizeImage();
    }

    public void LoadTexture(byte[] _data)
    {
        if (_data != null)
        {
            sourceTexture.LoadImage(_data);
        }
    }

    public void ExportPNG()
    {
        exportPng.SaveImageToFile(inputTexture);
    }

    public void LoadData(ToolData _data)
    {
        image = _data.imgBytes;
        sliderValue = _data.pixelateAmount;
    }

    public void SaveData(ToolData _data)
    {
        _data.imgBytes = image;
        _data.pixelateAmount = sliderValue;
    }

    private void Start()
    {
        //Unity is whack, so I need to do it again? (UI bugged out)
        slider.value = sliderValue;    
        UpdateText(slider.value);
        PixelizeImage();
    }
}
