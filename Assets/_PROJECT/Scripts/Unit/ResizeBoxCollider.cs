using UnityEngine;

public class ResizeBoxCollider : MonoBehaviour
{

    private BoxCollider2D _boxCollider;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _boxCollider.size
            = new Vector2(_spriteRenderer.size.x, _spriteRenderer.size.y);
    }
}
