using UnityEngine;
using UnityEngine.UI;

public class BarrackUI : MonoBehaviour
{

    [SerializeField] private Image _timerCircleImage;

    public void UpdateTimerBar(float currentTime, float maxTime)
    {

        _timerCircleImage.fillAmount = currentTime / maxTime;

    }

}