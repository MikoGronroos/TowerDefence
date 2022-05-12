using PathCreation;
using UnityEngine;
using Photon.Pun;

public class FollowPath : MonoBehaviour
{

    [SerializeField] private PathCreator pathCreator;

    [SerializeField] private float speed;

    private const float _rotationSpeed = 5.0f;

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

        Vector3 targetDirection = point - transform.position;

        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * _rotationSpeed);

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
