using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GunController : MonoBehaviour
{
    [SerializeField] private float dragPower;
    [SerializeField] private TweenStruct dragAnimation;
    public bool left;
    public Transform header;
    private Vector3 mousePos, startPos;
    public float angle, minAngle, maxAngle, current, radius, headerOffset;
    [SerializeField] private bool blocker = false;

    [SerializeField] private float maxDrag = 120f;
    [Range(0.0001f, 1f)]
    [SerializeField] private float dragRestoreSpeed;
    [SerializeField] private float currentDrag = 0f;
    void Start()
    {
        startPos = transform.localPosition;
    }


    public void CreateDrag()
    {
        currentDrag += Mathf.Abs(dragPower);

        //blocker = true;
        //Vector3 last = transform.rotation.eulerAngles;
        //transform.DORotate(new Vector3(0, 0, last.z + dragPower), dragAnimation.Duration).OnComplete(() =>
        //{

        //    transform.DORotate(last, dragAnimation.Duration).OnComplete(() =>
        //    {
        //        blocker = false;
        //    });


        //    //blocker = false; 
        //});
    }

    void FixedUpdate()
    {

        if (blocker) return;

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 point = mousePos;
        current = Mathf.Atan2(point.y - transform.position.y, point.x - transform.position.x) * Mathf.Rad2Deg - angle;
        if (left)
        {
            angle = 180f;
            header.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            header.localScale = new Vector3(-1, -1, 1);
            angle = 0f;
        }
        Vector3 pos = mousePos - transform.position;

        transform.localPosition = startPos + (left ? (pos.normalized * radius) : (pos.normalized * -radius));

        float a = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;

        if (mousePos.x > transform.position.x)
            transform.eulerAngles = new Vector3(0, 0, Mathf.Clamp(a - currentDrag, minAngle, maxAngle));

        float tempSpeed = Mathf.Clamp((currentDrag * dragRestoreSpeed), 0, float.MaxValue);


        currentDrag = Mathf.Clamp(currentDrag - tempSpeed, 0, maxDrag);
        //currentDrag -= (currentDrag * 0.1f);


        //header.transform.rotation = Quaternion.Euler(
        //        0,
        //        0,
        //        current + headerOffset);
        //facingg = WrapAngle(current);


    }
}
