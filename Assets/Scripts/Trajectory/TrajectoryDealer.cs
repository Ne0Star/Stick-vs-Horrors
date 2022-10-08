using System.Collections;
using System.Linq;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Jobs;


[System.Serializable]
public class TrajectoryData
{
    public bool useCurves;
}

/// <summary>
/// Эта структура будет хранить и передавать данные из главного потока
/// </summary>
[System.Serializable]
public class DynamicData
{


    public Transform target, patron;

    /// <summary>
    /// Если патрон вложенный, то источником будет его родитель
    /// </summary>
    public Transform source;

    public MissilesType missilesType;

    /// <summary>
    /// Время которое пройцдено для снаряда
    /// </summary>
    public float currentTime;
    /// <summary>
    /// Скорость с которым снаряд проходит время
    /// </summary>
    public float currentSpeed;
    /// <summary>
    /// Множитель смещения траектории
    /// </summary>
    public float allCurvesPower;
    /// <summary>
    /// Вращение будет происходить с этим отклонением
    /// </summary>
    public float rotateOffset;
    /// <summary>
    /// Когда дистанция будет меньше или равна этой патрон прилетит к цели я
    /// </summary>
    public float distanceToCompleted;
    public Vector3 endPosition;
    public Vector3 startPosition;
    public bool pursue, rotateToTarget;
}
public class TrajectoryDealer : MonoBehaviour
{
    [Header("Количество трансформов на одного ребёнка")]
    public int oneDealerPatrons;
    [SerializeField]
    private int maxPatrons;
    [SerializeField] private int currentPatrons;

    public bool UseJob;
    public Allocator allocator;
    public JobHandle handles;
    public Mover moveJob;
    public TransformAccessArray accessArray;

    public bool AllowBuild()
    {
        if (tempBlock || currentPatrons >= maxPatrons) return false; else return true;
    }
    private void OnEnable()
    {

    }
    private void OnDisable()
    {

    }
    private void OnDestroy()
    {
        DisposeAllTemp();
    }

    /// <summary>
    /// Эта структура отправляется в задачу
    /// </summary>
    [System.Serializable]
    public struct Data
    {
        public bool rotateToTarget;
        public float time;
        public float speed;
        public Vector3 offset;
        public float offsetMultipler;
        public float rotateOffset;
        public Vector3 endPosition;
        public Vector3 startPosition;

    }

    void Awake()
    {
        //maxPatrons = transform.childCount * oneDealerPatrons;
        m_Targets = new Transform[maxPatrons];
        m_Patrons = new Transform[maxPatrons];
        m_OnReached = new UnityEvent<Transform, Transform>[maxPatrons];
        m_DynamicData = new DynamicData[maxPatrons];
    }

    public Transform[] m_Targets;
    public Transform[] m_Patrons;
    /// <summary>
    /// Патрон, цель, источник
    /// </summary>
    public UnityEvent<Transform, Transform>[] m_OnReached;
    public DynamicData[] m_DynamicData;
    public NativeArray<Data> tempData;

    public void DisposeAllTemp()
    {
        if (accessArray.isCreated)
            accessArray.Dispose();
        if (tempData.IsCreated)
            tempData.Dispose();
        isCreated = false;
    }
    public void CreateAllTemp()
    {
        accessArray = new TransformAccessArray(m_Patrons);
        tempData = new NativeArray<Data>(m_Patrons.Length, allocator);
        isCreated = true;
    }
    public int created = 0;
    public int reached = 0;
    public void RemoveAllList(int i, bool andDispose)
    {
        //Debug.Log(m_Patrons[i] + " " + i);
        currentPatrons--;

        reached++;
        m_OnReached[i]?.Invoke(m_Patrons[i], m_Targets[i]);

        m_OnReached[i] = null;
        m_DynamicData[i] = new DynamicData();
        m_Targets[i] = null;
        m_Patrons[i] = null;
        DisposeAllTemp();
        //
        //currentPatrons--;

    }

    private void CreateAllList(DynamicData d, Transform patron, Transform target, UnityEvent<Transform, Transform> onComplete)
    {
        //if (!AllowBuild()) return;
        for (int i = 0; i < m_Patrons.Length; i++)
        {
            if (!m_Patrons[i])
            {
                //StartCoroutine(PatronLife(i));
                m_OnReached[i] = onComplete;
                m_DynamicData[i] = d;
                m_Patrons[i] = patron;
                m_Targets[i] = target;
                //currentPatrons++;
                return;
            }
        }
        //
    }

    //private IEnumerator PatronLife(int i)
    //{
    //    yield return new WaitUntil(() => (!m_Patrons[i] || !m_Patrons[i].gameObject.activeInHierarchy));
    //    currentPatrons--;
    //}

    private void LateUpdate()
    {
        //if(currentPatrons == maxPatrons) currentPatrons = 0;

        if (!isCreated)
        {
            return;
        }
        if (UseJob)
        {
            for (int i = 0; i < m_DynamicData.Length; i++)
            {
                DynamicData dd = m_DynamicData[i];
                if (m_Patrons[i] != null)
                {
                    Data td = tempData[i];
                    // check last data
                    if (!m_Patrons[i].gameObject.activeInHierarchy || Vector2.Distance(m_Patrons[i].position, dd.endPosition) <= dd.distanceToCompleted)
                    {
                        RemoveAllList(i, true);
                        CreateAllTemp();
                        return;
                    }
                    // set/update dynamic
                    dd.currentTime += 1f / Vector2.Distance(dd.endPosition, dd.startPosition) * dd.currentSpeed;
                    // set temp
                    td.rotateOffset = dd.rotateOffset;
                    td.rotateToTarget = dd.rotateToTarget;
                    if (dd.pursue)
                    {
                        if (m_Targets[i] && m_Patrons[i])
                        {
                            dd.endPosition = m_Targets[i].position;
                        }
                        if (!m_Targets[i] && m_Patrons[i])
                        {
                            dd.endPosition = m_Patrons[i].position;
                        }
                    }
                    td.startPosition = dd.startPosition;
                    td.endPosition = dd.endPosition;
                    td.offset = new Vector3(dd.missilesType.XOffset.Evaluate(dd.currentTime), dd.missilesType.YOffset.Evaluate(dd.currentTime), 0);
                    td.speed = dd.missilesType.SpeedInterpolator.Evaluate(dd.currentTime);
                    td.time = dd.currentTime;
                    td.offsetMultipler = dd.allCurvesPower;

                    // apply
                    tempData[i] = td; // Данные которые отправятся
                    m_DynamicData[i] = dd;
                }
            }

            moveJob = new Mover
            {
                data = tempData
            };

            handles = moveJob.Schedule(accessArray);
handles.Complete();
        }
    }

    public bool ContainsPatron(Transform patron)
    {
        if (m_Patrons.Contains(patron))
        {
            return true;
        }
        else return false;
    }

    public void CreatePatron(DynamicData data, Transform patron, Transform target, UnityEvent<Transform, Transform> onComplete, System.Action<string> cansel)
    {

        if (!AllowBuild()) { cansel("Запрещенно"); return; };
        created++;
        StartCoroutine(WaitTemp());
        if (data.missilesType.UseRandomCurve)
        {
            data.missilesType.XOffset = data.missilesType.SetRandomKeys(data.missilesType.XOffset, data.missilesType.RandomMinX, data.missilesType.RandomMaxX);
            data.missilesType.YOffset = data.missilesType.SetRandomKeys(data.missilesType.YOffset, data.missilesType.RandomMinY, data.missilesType.RandomMaxY);
        }
        currentPatrons++;
        if (UseJob)
        {
            CreateAllList(data, patron, target, onComplete);
            ConfigureArray();
        }
        else
        {
            CreateAllList(data, patron, target, onComplete);
        }
    }
    private IEnumerator WaitTemp()
    {
        tempBlock = true;
        yield return new WaitForSeconds(0.0000001f);
        tempBlock = false;
    }
    [SerializeField]
    private bool tempBlock = false;

    [SerializeField]
    private bool isCreated = false;
    private void ConfigureArray()
    {
        if (isCreated)
        {
            DisposeAllTemp();
        }
        CreateAllTemp();
        //yield return new WaitUntil(() => accessArray.isCreated);
    }
    [BurstCompile]
    public struct Mover : IJobParallelForTransform
    {
        public NativeArray<Data> data;
        public void Execute(int i, TransformAccess transform)
        {
            Vector2 target = (data[i].endPosition + (data[i].offset * data[i].offsetMultipler));
            //float tempSpeed = 0f;

            //if (Vector2.Distance(transform.position, data[i].endPosition) < 1f) tempSpeed += 0.01f;
            Vector3 nextPosition = (Vector3.LerpUnclamped(data[i].startPosition, target, data[i].speed));
            if (data[i].rotateToTarget)
            {
                GameUtils.LookAt2D(transform, nextPosition, data[i].rotateOffset);
            }
            transform.position = nextPosition;
        }
    }
}