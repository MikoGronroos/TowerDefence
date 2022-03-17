using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GraphicsManager : MonoBehaviourSingleton<GraphicsManager>
{

    [SerializeField] private string graphicsPath;

    [SerializeField]private List<GraphicsItem> _graphicsItems = new List<GraphicsItem>();

    private void Start()
    {
        var sprites = Resources.LoadAll(graphicsPath, typeof(Sprite)).Cast<Sprite>().ToArray();

        foreach (var sprite in sprites)
        {
            GraphicsItem item = new GraphicsItem(sprite.name, sprite);
            _graphicsItems.Add(item);
        }

    }

    public Sprite GetSprite(string key)
    {

        foreach (var graphic in _graphicsItems)
        {
            if (graphic.Key == key)
            {
                return graphic.Graphic;
            }
        }
        return null;
    }

}

[System.Serializable]
public class GraphicsItem
{

    public GraphicsItem(string Key, Sprite Graphic)
    {
        this.Key = Key;
        this.Graphic = Graphic;
    }

    public string Key;
    public Sprite Graphic;
}