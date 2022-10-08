using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Savage : Enemu
{
    [SerializeField] private Animator animator;

    [SerializeField] private AnimationClip пробитиеНоги;

    [SerializeField] private LimbStrength[] supportLimb;

    [SerializeField] private float speed;
    [SerializeField] private float speedMultipler;


    [SerializeField] private bool isfell = false;

    public void ChangeSpeedMultipler(float multipler)
    {
        speedMultipler = multipler;
    }

    private void FixedUpdate()
    {
        transform.position += new Vector3(speed * speedMultipler, 0);
    }


    private void OnEnable()
    {animator.Play("Бег");
        foreach(LimbStrength limb in limbs)
        {
            if(limb)
            {
                limb.gameObject.SetActive(true);
            }
        }
    }

    private void Awake()
    {
        limbs = GetComponentsInChildren<LimbStrength>(true);

        

        foreach (LimbStrength backLimb in supportLimb)
        {
            backLimb.onBreak.AddListener((v) =>
            {
                if (isfell) return;
                isfell = true;
                animator.Play("Пробитие_задней_ноги");
                animator.speed = Random.Range(1f, 2f);
            });

        }
        
    }

}
