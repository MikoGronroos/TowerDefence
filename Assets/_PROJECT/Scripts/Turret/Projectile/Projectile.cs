using UnityEngine;

public class Projectile : MonoBehaviour
{

    private Vector3 shootDir;

    public void Setup(Vector3 direction)
    {
        shootDir = direction;
    }

    public void Update()
    {
        float moveSpeed = 10.0f;
        transform.position += moveSpeed * shootDir * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

}
