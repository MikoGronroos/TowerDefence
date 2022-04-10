using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Projectile : MonoBehaviour
{

    [SerializeField] private int projectilePenetration;

    private Vector3 _shootDir;
    private float _damage;
    private float _speed;
    private IEnumerable<ProjectileType> _types;
    private bool _hitObject;

    public void Setup(Vector3 direction, TurretExecutable exec)
    {
        _shootDir = direction;
        _damage = exec.Damage.Value;
        _types = exec.ProjectileTypes;
        _speed = exec.ProjectileSpeed;
    }

    public void Update()
    {
        transform.position += _speed * _shootDir * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Unit unit))
        {
            if (_hitObject) return;

            unit.RemoveCurrentHealth(_damage, _types);

            if (!unit.CheckIfProjectilePenetrates(projectilePenetration))
            {

                PhotonNetwork.Destroy(gameObject.GetPhotonView());
                _hitObject = true;
            }

            projectilePenetration -= unit.GetHardness();
            unit.RemoveHardness(projectilePenetration);
            

        }
    }

}
