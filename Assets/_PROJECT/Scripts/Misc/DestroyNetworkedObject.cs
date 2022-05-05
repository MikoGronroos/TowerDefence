using UnityEngine;
using Photon.Pun;

public class DestroyNetworkedObject : MonoBehaviour
{

    [Header("Collision and Trigger")]
    [SerializeField] private bool destroyOnTriggerEnter;
    [SerializeField] private bool destroyOnCollisionEnter;
    [SerializeField] private string neededTag;

    [Header("Destroy After Time")]
    [SerializeField] private bool destroyAfterTime;
    [SerializeField] private float destroyTime;

    private PhotonView _photonView;

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
    }

    private void Start()
    {
        if (destroyAfterTime) Invoke("Destroy", destroyTime);
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
            Destroy();
        }
    }

    private void Destroy()
    {
        if (_photonView.IsMine)
        {
            PhotonNetwork.Destroy(_photonView);
        }
    }
}