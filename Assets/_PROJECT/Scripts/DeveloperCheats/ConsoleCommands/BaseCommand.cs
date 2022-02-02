using UnityEngine;

public abstract class BaseCommand : ScriptableObject
{

    [SerializeField] private string commandWord;

    [SerializeField] private string commandDescription;

    public string CommandWord { get { return commandWord; } private set { } }

    public string CommandDescription { get { return commandDescription; } private set { } }

    public abstract bool Process(string[] args);

}
