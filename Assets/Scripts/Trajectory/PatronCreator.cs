using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "MoveData", menuName = "Trajectory/MoveData", order = 1)]
public class PatronCreator : ScriptableObject
{
    [SerializeField] public UnityEvent<Transform, Transform> reach;
    public MissilesType missilesType;
    public float allCurvesPower;
    public float moveSpeed;
    public float reachRange;
    public float rotateOffset;
    public bool useCurves = false;
    public bool usePursue = false;
    public bool useZRotation = false;
    public Transform target;
    public Vector3 endPosition;


    public DynamicData GetMoveData(Transform patron, Vector3 targetPosition, Transform source)
    {
        return new DynamicData()
        {
            patron = patron,
            target = null,
            distanceToCompleted = reachRange,
            allCurvesPower = allCurvesPower,
            currentSpeed = moveSpeed,
            currentTime = 0f,
            startPosition = patron.position,
            endPosition = targetPosition,
            missilesType = missilesType,
            source = source,
            pursue = false,
            rotateOffset = rotateOffset,
            rotateToTarget = useZRotation
        };
    }

    public DynamicData GetMoveData(Transform patron, Transform moveTarget, Transform source)
    {
        Transform totalTarget = (moveTarget == null) ? target : moveTarget;
        return new DynamicData()
        {
            patron = patron,
            target = totalTarget,
            distanceToCompleted = reachRange,
            allCurvesPower = allCurvesPower,
            currentSpeed = moveSpeed,
            currentTime = 0f,
            startPosition = patron.position,
            endPosition = (totalTarget == null) ? endPosition : totalTarget.position,
            missilesType = missilesType,
            source = source,
            pursue = usePursue,
            rotateOffset = rotateOffset,
            rotateToTarget = useZRotation
        };
    }

}
