using TMPro;
using UnityEngine;

public class GameStartUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI amountOfPlayersReadyText;
    
    public void UpdateAmountOfPlayersText(int currentlyReady, int playersInTheGame) => amountOfPlayersReadyText.text = $"{currentlyReady}/{playersInTheGame}";

}
