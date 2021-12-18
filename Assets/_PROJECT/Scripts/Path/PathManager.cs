using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviourSingleton<PathManager>
{

    [SerializeField] private List<Path> paths = new List<Path>();

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

}
