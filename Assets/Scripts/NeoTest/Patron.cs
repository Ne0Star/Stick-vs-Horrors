using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[System.Serializable]
public struct TweenScaleData
{
    [SerializeField] private Ease easing;
    [SerializeField] private Vector3 scale;
    [SerializeField] private float duration;

    public Ease Easing { get => easing; set => easing = value; }
    public Vector3 Scale { get => scale; set => scale = value; }
    public float Duration { get => duration; set => duration = value; }
}
[System.Serializable]
public struct TweenStruct
{
    [SerializeField] private Ease easing;
    [SerializeField] private float duration;
    public Ease Easing { get => easing; set => easing = value; }
    public float Duration { get => duration; set => duration = value; }
}
public class Patron : MonoBehaviour
{
    [System.Serializable]
    public class OnPatronKill : UnityEngine.Events.UnityEvent { }


    [SerializeField] private SpriteRenderer patronRenderer;
    public void SetOrder(string name, int order)
    {
        patronRenderer.sortingLayerName = name;
        patronRenderer.sortingOrder = order;
    }

    [SerializeField] private TweenScaleData exploseIn, exploseOut;

    [SerializeField] private float exploseDistance, explosePower;
    [SerializeField] private bool rotateToVelocity;
    [SerializeField] private float rotateOffset;
    [SerializeField] private OnPatronKill onKill;
    [SerializeField] private CustomShadow shadow;
    [SerializeField] private Vector3 startScale;
    public CustomShadow Shadow { get => shadow; }
    public OnPatronKill OnKill { get => onKill; }

    private void Awake()
    {
        startScale = transform.localScale;
    }
    [SerializeField] private float minY;

    public void SetMaxY(float minY)
    {
        this.minY = minY;
    }

    private void Update()
    {
        //if (block) return;
        if (transform.position.y <= minY)
        {
            //block = true;
            Explose();
        }
    }

    private void OnBecameInvisible()
    {
        onKill?.Invoke();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, exploseDistance);
    }

    private void OnEnable()
    {
        shadow.gameObject.SetActive(true);
        transform.localScale = startScale;
    }

    private void OnDisable()
    {
        shadow.gameObject.SetActive(false);
    }

    /// <summary>
    /// »митирует взрыв, не вли€€ и нанос€ никакого урона
    /// </summary>
    public void AnimateExplose()
    {
        transform.DOScale(exploseIn.Scale, exploseIn.Duration).OnComplete(() =>
        {
            transform.DOScale(startScale, exploseOut.Duration).OnKill(() =>
            {

            }).SetEase(exploseOut.Easing);
        }).SetEase(exploseIn.Easing);
    }
    public void AnimateExplose(System.Action onComplete)
    {
        transform.DOScale(exploseIn.Scale, exploseIn.Duration).OnComplete(() =>
        {
            transform.DOScale(startScale, exploseOut.Duration).OnKill(() =>
            {
                onComplete();
            }).SetEase(exploseOut.Easing);

        }).SetEase(exploseIn.Easing);
    }
    public void Explose()
    {
        transform.DOScale(exploseIn.Scale, exploseIn.Duration).OnComplete(() =>
        {
            transform.DOScale(exploseOut.Scale, exploseOut.Duration).OnKill(() =>
            {
                onKill?.Invoke();
            }).SetEase(exploseOut.Easing);

        }).SetEase(exploseIn.Easing);
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (block) return;
    //    block = true;
    //    Explose();
    //}

}
