using Finark.Events;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviourSingleton<PathManager>
{

    [SerializeField] private List<Path> paths = new List<Path>();

    [SerializeField] private PathEventChannel pathEventChannel;

    private void Start()
    {
        pathEventChannel?.SetupPaths(null, SetupNewPath);
    }

    public Path GetPathWithPlayerID(int id)
    {
        foreach (var path in paths)
        {
            if (path.EnemyPlayerID == id)
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
