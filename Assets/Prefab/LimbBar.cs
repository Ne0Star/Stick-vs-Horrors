using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LimbBar : HitBar
{
    [SerializeField] private SpriteRenderer killSprite;
    [SerializeField] private SpriteRenderer[] mainRenderers;

    [SerializeField] private bool findToStart = false;
    [SerializeField] private Transform findTarget;
    [SerializeField] private LimbStrength[] limbs;
    [SerializeField] private List<LimbStrength> test;
    [SerializeField] private int maxLimb;
    [SerializeField] private int currentLimb;
    [SerializeField] private float persent;

    private bool animBlocker = false;

    private IEnumerator CustomAnimate()
    {
        animBlocker = true;
        float time = 0;

        Vector3 startPos = Vector3.zero, startScale = Vector3.zero;
        Color startColor = Color.cyan;

        if (killSprite)
        {
            startPos = killSprite.transform.localPosition;
            startPos = killSprite.transform.localScale;
            startColor = killSprite.color;
        }

        while (time < 1f)
        {
            foreach (SpriteRenderer renderer in mainRenderers)
            {
                renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 1f - time);
            }
            if (killSprite)
            {
                killSprite.color = new Color(killSprite.color.r, killSprite.color.g, killSprite.color.b, 1f - time);
                killSprite.transform.localPosition += new Vector3(0, 0.1f, 0);
                killSprite.transform.localScale = new Vector3(time, time, time);
            }

            time += 0.01F;
            yield return new WaitForFixedUpdate();
        }
        findTarget.gameObject.SetActive(false);

        foreach (SpriteRenderer renderer in mainRenderers)
        {
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 1f);
        }
        if (killSprite)
        {
            killSprite.color = startColor;
            killSprite.transform.localPosition = startPos;
            killSprite.transform.localScale = startScale;
        }
        animBlocker = false;
        yield break;
    }

    private void Awake()
    {
        if (findToStart)
        {
            limbs = findTarget.GetComponentsInChildren<LimbStrength>();
        }
        mainRenderers = findTarget.GetComponentsInChildren<SpriteRenderer>();
        maxLimb = limbs.Length;
        currentLimb = 0;
        //findTarget = null;
        float h = 0f;
        foreach (LimbStrength limb in limbs)
        {
            test.Add(limb);
            //limb.onDamage.AddListener((damage, sources) =>
            //{
            //    health -= damage;
            //    Debug.Log(health / maxHealth * 100);
            //});

            limb.onBreak.AddListener((sources) =>
            {
                test.Remove(sources);
                //health -= sources.MaxStrength;
                TakeDamage(sources.MaxStrength + sources.AddtiveDamage, 0.1f, () => {
                    if (!animBlocker && gameObject.activeInHierarchy)
                        StartCoroutine(CustomAnimate());

                });
                persent = health / maxHealth * 100;
                if (persent <= 0)
                {
                   // if (!animBlocker && gameObject.activeInHierarchy)
                   //     StartCoroutine(CustomAnimate());
                    //Sequence sequence = DOTween.Sequence();
                    //foreach (SpriteRenderer sprite in mainRenderers)
                    //{
                    //    Tween t = sprite.DOColor(new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0f), 0.1f);
                    //    sequence.Insert(0.1f, t);
                    //}


                    //sequence.Play().OnComplete(() =>
                    //{


                    //    //findTarget.gameObject.SetActive(false);
                    //}).OnStart(() =>
                    //{
                    //    float scale = 0.5f;
                    //    killSprite.transform.DOScale(new Vector3(scale, scale, scale), 1f);
                    //    killSprite.DOColor(new Color(killSprite.color.r, killSprite.color.g, killSprite.color.b, 0f), 1f);
                    //    killSprite.transform.DOLocalMove(new Vector3(killSprite.transform.localPosition.x, 5f), 1f);
                    //});

                }
            });

            h += limb.MaxStrength;
        }

        maxHealth = h;
        health = h;
        strength = 0f;
        maxStrength = 0f;
    }

    public override void UpdateData()
    {

    }
}
