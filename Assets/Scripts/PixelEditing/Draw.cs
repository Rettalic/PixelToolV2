using UnityEngine;

public class Draw : ICommand
{
    private Color[] originalColour;
    private Color[] backupColour;
    private Texture2D texture;

    public Draw(Color[] _original, Color[] _backup, Texture2D _texture)
    {
        this.originalColour = _original;
        this.backupColour   = _backup;
        this.texture        = _texture;
    }
    
    public void Execute()
    {
        texture.SetPixels(originalColour);
        texture.Apply();
    }

    public void Undo()
    {
        if(texture.width * texture.height > backupColour.Length)  return; 
        texture.SetPixels(backupColour);
        texture.Apply();
    }
}