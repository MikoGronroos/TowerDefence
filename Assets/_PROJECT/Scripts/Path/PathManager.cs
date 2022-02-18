using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviourSingleton<PathManager>
{

    [SerializeField] private List<Path> paths = new List<Path>();

    //[SerializeField] private EventChannel pathManagerLoadedChannel;

    private void Start()
    {
        //pathManagerLoadedChannel.RaiseEvent(null);
    }

    public Path GetPathWithPlayerID(int id)
    {
        foreach (var path in paths)
        {
            if (path.PlayedId == id)
            {
                return path;
            }
        }
        return null;
    }

    public void SetupNewPath(Dictionary<string, object> args)
    {
        var path = (Path)args["Path"];

        paths.Add(path);

    }

}
