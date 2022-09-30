using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;


[CreateAssetMenu(fileName = "UI Animations", menuName = "UI Animations/Animation", order = 1)]
public class UIAnimator : ScriptableObject
{
    public UIAnimationData openStats, closeStats;

    public void SetCloseStats(Transform target)
    {
        target.localPosition = closeStats.position.value;
        target.localRotation = Quaternion.Euler(closeStats.rotate.value);
        target.localScale = closeStats.scale.value;
    }

}
