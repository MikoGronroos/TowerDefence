using UnityEngine;

public class SkinManager : MonoBehaviourSingletonDontDestroyOnLoad<SkinManager>
{

    [SerializeField] private PlayerSkins skins;

    public string GetGraphicKeyWithMainKey(string mainKey)
    {
        foreach (var graphic in skins.graphics)
        {
            if (graphic.MainKey == mainKey)
            {
                if(graphic.GraphicKey != "") return graphic.GraphicKey;
            }
        }
        return $"{mainKey}_Default";
    }

    public void SetGraphicKeyWithMainKey(string mainKey, string graphicKey)
    {

        foreach (var graphic in skins.graphics)
        {
            if (graphic.MainKey == mainKey)
            {
                graphic.GraphicKey = graphicKey;
            }
        }

        SaveData.SaveTheSkinManagerData(skins);

    }

    public void ReplaceGraphicsDataSet(PlayerSkins data)
    {
        skins = data;
    }

    public PlayerSkins GetGraphicsData()
    {
        return skins;
    }

}


[System.Serializable]
public class Graphic
{
    public string MainKey;
    public string GraphicKey;
}

[System.Serializable]
public class PlayerSkins
{
    public Graphic[] graphics;
}