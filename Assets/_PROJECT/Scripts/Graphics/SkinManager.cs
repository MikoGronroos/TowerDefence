using UnityEngine;

public class SkinManager : MonoBehaviourSingletonDontDestroyOnLoad<SkinManager>
{

    [SerializeField] private Graphic[] graphics;

    public string GetGraphicKeyWithMainKey(string mainKey)
    {
        foreach (var graphic in graphics)
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
        foreach (var graphic in graphics)
        {
            if (graphic.MainKey == mainKey)
            {
                graphic.GraphicKey = graphicKey;
            }
        }
    }

}


[System.Serializable]
public class Graphic
{
    public string MainKey;
    public string GraphicKey;
}