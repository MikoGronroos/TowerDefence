using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Projectile : MonoBehaviour, IPunInstantiateMagicCallback
{

    private Vector3 _shootDir;
    private float _damage;
    private float _speed;
    private int _currentProjectilePenetration;
    private IEnumerable<ProjectileType> _types;
    private bool _hitObject;

    private PhotonView _photonView;
    private SpriteRenderer _spriteRenderer;

    public int InstanceId { get; private set; }

    public string PrefabName { get; set; }

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Setup(Vector3 direction, TurretExecutable exec, string mainKey)
    {
        _shootDir = direction;
        _damage = exec.Damage.Value;
        _types = exec.ProjectileTypes;
        _speed = exec.ProjectileSpeed;
        _currentProjectilePenetration = exec.ProjectilePenetration;
        _hitObject = false;

        _photonView.RPC("RPCSetup", RpcTarget.All, mainKey);

    }

    [PunRPC]
    private void RPCSetup(string mainKey)
    {
        _spriteRenderer.sprite = GraphicsManager.Instance.GetSprite(SkinManager.Instance.GetGraphicKeyWithMainKey(mainKey));
    }

    public void Update()
    {
        transform.position += _speed * _shootDir * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (!_photonView.IsMine) return;

        if (collision.TryGetComponent(out Unit unit))
        {
            if (_hitObject) return;

            unit.RemoveCurrentHealth(_damage, _types);

            if (!unit.CheckIfProjectilePenetrates(_currentProjectilePenetration))
            {
                ProjectileSpawner.Instance.DespawnUnit(gameObject);
                _hitObject = true;
            }

            _currentProjectilePenetration -= unit.GetHardness();
            unit.RemoveHardness(_currentProjectilePenetration);
        }
    }

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        var photonView = gameObject.GetPhotonView();
        object[] data = photonView.InstantiationData;
        if (data != null && data.Length == 1)
        {
            InstanceId = (int)data[0];

            ProjectileSpawner.Instance.AddProjectileToList(gameObject, InstanceId);
        }
    }
}
