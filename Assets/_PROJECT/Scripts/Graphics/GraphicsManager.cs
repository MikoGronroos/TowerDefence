using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GraphicsManager : MonoBehaviourSingletonDontDestroyOnLoad<GraphicsManager>
{

    [SerializeField] private string graphicsPath;

    private Dictionary<string, Sprite> _graphics = new Dictionary<string, Sprite>();

    private void Start()
    {
        var sprites = Resources.LoadAll(graphicsPath, typeof(Sprite)).Cast<Sprite>().ToArray();

        foreach (var sprite in sprites)
        {
            _graphics.Add(sprite.name, sprite);
        }
    }

    public Sprite GetSprite(string key)
    {
        Debug.Log(key);
        foreach (var graphic in _graphics)
        {
            Debug.Log(graphic.Key);
            if (graphic.Key.Contains(key))
            {
                return graphic.Value;
            }
        }
        return null;
    }

}