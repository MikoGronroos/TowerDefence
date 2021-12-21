using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Tweening : MonoBehaviour
{

    public static void Shake(Transform target)
    {
        target.DOShakeRotation(Mathf.Infinity, 45, 5);
    }

    public static void KillTween(Transform target)
    {
        target.DOKill();
    }

}
