using PathCreation;
using UnityEngine;
using Photon.Pun;

public class FollowPath : MonoBehaviour
{

    [SerializeField] private PathCreator pathCreator;

    [SerializeField] private float speed;

    private PhotonView _view;

    private float _distanceTraveled;

    private void Awake()
    {
        _view = GetComponent<PhotonView>();
    }

    private void Update()
    {

        if (!_view.IsMine) return;

        if (pathCreator == null) return;

        _distanceTraveled += speed * Time.deltaTime;
        Vector3 point = pathCreator.path.GetPointAtDistance(_distanceTraveled);

        transform.position = point;
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public void SetPath(PathCreator pathCreator)
    {
        this.pathCreator = pathCreator;
    }

}
