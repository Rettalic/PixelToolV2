using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToolSettings : MonoBehaviour, ICommand, IDataPersistence
{ 
    public int pixelateAmount;
    public TMP_Text text;

    public ToolSettings(int _pixelateAmount)
    {
        pixelateAmount = _pixelateAmount;
    }

    #region Undo/Redo
    public void Execute()
    {
        pixelateAmount++;
    }

    public void Undo()
    {
        pixelateAmount--;
    }
    #endregion Undo/Redo

    #region Save/Load
    public void LoadData(ToolData _data)
    {
        pixelateAmount = _data.pixelateAmount;
    }

    public void SaveData(ToolData _data)
    {
        _data.pixelateAmount = pixelateAmount;
    }
    #endregion Save/Load


    private void Update()
    {
        text.text = pixelateAmount.ToString();
    }


}
