using UnityEngine;

public class CreateTexture : MonoBehaviour
{
    public Color color;
    int res = 4;

    void Start()
    {
        Color[] pixels = new Color[res * res];

        for (int i = 0; i < pixels.Length; i++)
        {
            pixels[i] = color;
        }

        Texture2D texture = new Texture2D(res, res);
        texture.SetPixels(pixels);
        texture.Apply();

        byte[] bytes = texture.EncodeToPNG();
        string path = Application.dataPath;
        System.IO.File.WriteAllBytes(path + "texture.png", bytes);

        UnityEditor.AssetDatabase.Refresh();
    }
}
