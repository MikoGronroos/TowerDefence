using PathCreation;
using UnityEngine;

public class FollowPath : MonoBehaviour
{

    [SerializeField] private PathCreator pathCreator;

    [SerializeField] private float speed;

    private float _distanceTraveled;

    private void Update()
    {
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
