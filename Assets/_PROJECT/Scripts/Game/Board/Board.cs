using UnityEngine;

public class Board : MonoBehaviour
{

    [SerializeField] private int playerID;

    public int GetID()
    {
        return playerID;
    }

}
