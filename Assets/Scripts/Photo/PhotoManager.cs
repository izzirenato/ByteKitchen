using Consts;

using UnityEngine;
using UnityEngine.UI;

public class PhotoManager : MonoBehaviour
{
    public RawImage topRawImage;
    public RawImage bottomRawImage;

    public Texture[] topTextures;
    public Texture[] bottomTextures;

    public void LoadCharacterImages(Characters characters)
    {
        int index = (int) characters;

        topRawImage.texture = topTextures[index];
        bottomRawImage.texture = bottomTextures[index];
    }

    private void Awake()
    {
        LoadCharacterImages(GameData.characterSelected);
    }
}