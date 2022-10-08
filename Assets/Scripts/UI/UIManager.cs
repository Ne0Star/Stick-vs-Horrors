using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UIManager : MonoBehaviour
{
    private Dictionary<int, UIPage> allPages = new Dictionary<int, UIPage>();

    [SerializeField] private int maxid = 0;

    private void Awake()
    {

    }

    public int RegisterPage(UIPage page)
    {
        allPages[maxid] = page;
        maxid++;
        return maxid - 1;
    }

    private IEnumerator Animate(Transform target, UIAnimationData data, System.Action onComplete)
    {
        bool one = false, two = false, three = false;
        if (data.scale.use)
        {
            target.DOScale(data.scale.value, data.scale.duration).SetEase(data.scale.easing).OnComplete(() =>
            {
                one = true;
            });
        } else
        {
            one = true;
        }
        if (data.position.use)
        {
            target.DOLocalMove(data.position.value, data.position.duration).SetEase(data.position.easing).OnComplete(() =>
            {
                two = true;
            });
        } else
        {
            two = true;
        }
        if (data.rotate.use)
        {
            target.DOLocalRotate(data.rotate.value, data.rotate.duration).SetEase(data.rotate.easing).OnComplete(() =>
            {
                three = true;
            });
        } else
        {
            three = true;
        }
        yield return new WaitUntil(() => one && two && three);
        onComplete();
    }



    public void OpenPage(UIPage page)
    {
        if (!page) return;
        page.gameObject.SetActive(true);
        StartCoroutine(Animate(page.transform, page.animator.openStats, () =>
        {

        }));
    }

    public void ClosePage(UIPage page)
    {
        if (!page) return;
        StartCoroutine(Animate(page.transform, page.animator.closeStats, () =>
        {
            if (page.gameObject)
            {
                Debug.Log("Close");
                page.gameObject.SetActive(false);
            }
        }));
    }
    public void OpenPage(UIPage page, System.Action onComplete)
    {
        if (!page) onComplete();
        page.gameObject.SetActive(true);

        StartCoroutine(Animate(page.transform, page.animator.openStats, () =>
        {
            if (page.gameObject)
                onComplete();
        }));
    }

    public void ClosePage(UIPage page, System.Action onComplete)
    {
        if (!page) onComplete();
        StartCoroutine(Animate(page.transform, page.animator.closeStats, () =>
        {
            if (page.gameObject)
            {
                page.gameObject.SetActive(false);
                onComplete();
            }
        }));
    }

}
