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




    public DynamicData GetMoveData(Transform patron, Transform target, Transform source)
    {
        return new DynamicData()
        {
            distanceToCompleted = reachRange,
            allCurvesPower = allCurvesPower,
            currentSpeed = moveSpeed,
            currentTime = 0f,
            startPosition = patron.position,
            endPosition = target != null ? target.position : endPosition,
            missilesType = missilesType,
            source = source,
            pursue = usePursue,
            rotateOffset = rotateOffset,
            rotateToTarget = useZRotation
        };
    }

}
