using UnityEngine;

public class DestroyObject : MonoBehaviour
{

    [Header("Collision and Trigger")]
    [SerializeField] private bool destroyOnTriggerEnter;
    [SerializeField] private bool destroyOnCollisionEnter;
    [SerializeField] private string neededTag;

    [Header("Destroy After Time")]
    [SerializeField] private bool destroyAfterTime;
    [SerializeField] private float destroyTime;

    private void Start()
    {
        if (destroyAfterTime) Destroy(gameObject, destroyTime);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (destroyOnCollisionEnter) ProcessCollision(collision.gameObject);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (destroyOnTriggerEnter) ProcessCollision(collision.gameObject);
    }

    private void ProcessCollision(GameObject collision)
    {
        if (collision.tag == neededTag)
        {
            Destroy(gameObject);
        }
    }

}
