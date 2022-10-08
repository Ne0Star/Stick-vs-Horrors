using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : OneSingleton<LevelManager>
{
    [SerializeField] private ShadowManager shadowManager;
    [SerializeField] private LimbManager limbManager;
    [SerializeField] private PatronPool patronPool;
    [SerializeField] private List<GameObject> allEnemiesAndBuilds;
    [SerializeField] private List<Enemu> allEnemu;

    [SerializeField] private string terrainTag;
    [SerializeField] private float maxY;


    public float MaxY { get => maxY; }
    public string TerrainTag { get => terrainTag; }
    public PatronPool PatronPool { get => patronPool; }
    public ShadowManager ShadowManager { get => shadowManager; }
    public LimbManager LimbManager { get => limbManager; set => limbManager = value; }

    private void Awake()
    {
        LevelManager.Instance = this;
        if(!shadowManager)
        shadowManager = FindObjectOfType<ShadowManager>();
        if(!patronPool)
        patronPool = FindObjectOfType<PatronPool>();
        allEnemu.AddRange(FindObjectsOfType<Enemu>());
        if (!limbManager)
            limbManager = FindObjectOfType<LimbManager>();
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
        //allEnemiesAndBuilds.AddRange(GameObject.FindGameObjectsWithTag("enemu"));
    }

}
