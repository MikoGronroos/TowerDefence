using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HealthManagerUI : MonoBehaviour
{

    public void UpdateHealthText(PlayerHealth health)
    {
        health.HealthText.text = $"Health: {health.Health}";
    }


}
