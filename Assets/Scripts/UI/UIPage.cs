using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[System.Serializable]
public struct Vector3Animation
{
    public bool use;
    public Ease easing;
    public float duration;
    public Vector3 value;
}

[System.Serializable]
public struct Vector1Animation
{
    public Ease easing;
    public float duration;
    public float value;
}

[System.Serializable]
public struct UIAnimationData
{
    public Vector3Animation position, scale, rotate;
}

public abstract class UIPage : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool setCloseStats = true;
    public UIAnimator animator;

    [System.Serializable]
    public class OnClosePage : UnityEngine.Events.UnityEvent { }
    [System.Serializable]
    public class OnOpenPage : UnityEngine.Events.UnityEvent { }
    [System.Serializable]
    public class OnUpPage : UnityEngine.Events.UnityEvent { }
    [SerializeField]
    private OnUpPage onUppage;
    [SerializeField]
    private OnOpenPage onOpenpage;
    [SerializeField]
    private OnClosePage onClosepage;

    private RectTransform rect;
    public RectTransform Rect => rect;

    public void OnPointerUp(PointerEventData data)
    {
        onUppage?.Invoke();
    }
    public void OnPointerDown(PointerEventData data)
    {

    }
    private void OnEnable()
    {
        onOpenpage?.Invoke();
    }
    private void OnDisable()
    {
        onClosepage?.Invoke();
    }


    [SerializeField] protected UIManager manager;
    [SerializeField] protected int id;
    private void Awake()
    {
        if (setCloseStats)
        {
            animator.SetCloseStats(transform);
        }
        rect = gameObject.GetComponent<RectTransform>();
        manager = FindObjectOfType<UIManager>();
        id = manager.RegisterPage(this);
    }

}
