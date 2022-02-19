using UnityEngine;
using PathCreation;

[System.Serializable]
public class Path
{

    public string PathOwner;

    public PathCreator ThisPath;

    public Transform PathStartPos;

    public int EnemyPlayerID;

}
