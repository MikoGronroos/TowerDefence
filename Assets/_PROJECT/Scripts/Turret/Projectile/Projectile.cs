using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    private Vector3 _shootDir;
    private float _damage;
    private IEnumerable<ProjectileType> _types;

    public void Setup(Vector3 direction, TurretExecutable exec)
    {
        _shootDir = direction;
        _damage = exec.Damage.Value;
        _types = exec.ProjectileTypes;
    }

    public void Update()
    {
        float moveSpeed = 15.0f;
        transform.position += moveSpeed * _shootDir * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Unit unit))
        {

            unit.RemoveCurrentHealth(_damage, _types);

            Destroy(gameObject);
        }
    }

}
