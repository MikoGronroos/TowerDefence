using UnityEngine;

public class GraphicFinder : MonoBehaviour
{

    [SerializeField] private string graphicKey;

    private SpriteRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _renderer.sprite = GraphicsManager.Instance.GetSprite(graphicKey);
    }
}
