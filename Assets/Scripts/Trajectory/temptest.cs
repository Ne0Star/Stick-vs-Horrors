using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class temptest : MonoBehaviour
{
    //[SerializeField] private float offsetMultipler;
    //[Range(0f, 1f)]
    //[SerializeField]
    //private float currentTime;
    //[SerializeField] private AnimationCurve yOffset, moveCurva;
    //[SerializeField] private Vector3 offset;
    //[SerializeField] private float speed;

    //[SerializeField] private Transform movable, target;
    //[SerializeField] private Vector3 startPosition;

    //private void Awake()
    //{
    //    startPosition = movable.position;
    //}

    //private void Update()
    //{

    //    offset = new Vector3(0, yOffset.Evaluate(currentTime), 0);
    //    movable.position = Vector3.Lerp(startPosition, target.position + (offset * offsetMultipler), moveCurva.Evaluate(currentTime));


    //}

    [SerializeField] private PatronCreator patron;




    //[SerializeField] private TrajectoryData data;

    //[SerializeField] private DynamicData dd;
    [SerializeField] private Transform movable;
    [SerializeField] private Transform target;
    [SerializeField] private TrajectorySystem tj;

    [SerializeField] private DynamicData preview;


    private void OnDrawGizmos()
    {
        if (preview != patron.GetMoveData(movable, target, transform)) preview = patron.GetMoveData(movable, target, transform);
    }

    // Start is called before the first frame update
    void Start()
    {

    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 lastPos = movable.position;
            //DynamicData dt = new DynamicData();
            //dt = new DynamicData()
            //{
            //    source = transform,
            //    startPosition = lastPos,
            //    rotateOffset = dd.rotateOffset,
            //    distanceToCompleted = dd.distanceToCompleted,
            //    allCurvesPower = dd.allCurvesPower,
            //    currentSpeed = dd.currentSpeed,
            //    currentTime = dd.currentTime,
            //    endPosition = Vector3.zero,
            //    missilesType = dd.missilesType,
            //    pursue = dd.pursue,
            //    rotateToTarget = dd.rotateToTarget
            //};


            tj.CreatePatron(patron.GetMoveData(movable, target, transform), movable, target, (p, t) =>
            {

                movable.position = lastPos;

            });
        }

    }

}
