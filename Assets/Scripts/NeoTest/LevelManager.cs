using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : OneSingleton<LevelManager>
{
    [SerializeField] private ShadowManager shadowManager;
    [SerializeField] private PatronPool patronPool;
    [SerializeField] private List<GameObject> allEnemiesAndBuilds;
    [SerializeField] private List<Enemu> allEnemu;

    [SerializeField] private string terrainTag;
    [SerializeField] private float maxY;


    public float MaxY { get => maxY; }
    public string TerrainTag { get => terrainTag; }
    public PatronPool PatronPool { get => patronPool; }
    public ShadowManager ShadowManager { get => shadowManager; }

    private void Awake()
    {
        LevelManager.Instance = this;
        shadowManager = FindObjectOfType<ShadowManager>();
        patronPool = FindObjectOfType<PatronPool>();
        allEnemu.AddRange(FindObjectsOfType<Enemu>());

    }

    private void Start()
    {
        Application.targetFrameRate = 60;
        //allEnemiesAndBuilds.AddRange(GameObject.FindGameObjectsWithTag("enemu"));
    }
    public void DealSplah(float exploseDistance, float explosePower)
    {
        foreach (Enemu e in allEnemu)
        {
            float distance = Vector2.Distance(transform.position, e.transform.position);
            float percent = Mathf.InverseLerp(exploseDistance, 0, distance);
            e.MainBody.AddForce((e.transform.position - transform.position).normalized * (percent * explosePower), ForceMode2D.Impulse);
        }


    }

    public void Test(Patron patron, System.Action patronComplete)
    {
        StartCoroutine(Wait(patron, patronComplete));
    }

    private IEnumerator Wait(Patron patron, System.Action patronComplete)
    {
        yield return new WaitForSeconds(1f);

        patronComplete?.Invoke();
    }
}
