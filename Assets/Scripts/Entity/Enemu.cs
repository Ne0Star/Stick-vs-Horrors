using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemu : Entity
{
    [SerializeField] private float initialMass;
    [SerializeField] private List<Ball> balls;
    [SerializeField] private Rigidbody2D mainBody;
    [SerializeField] private LimbStrength[] limbs;
    public Rigidbody2D MainBody { get => mainBody; }

    private void Start()
    {

    }

    private void Awake()
    {
        
    }


    protected override void OnTakeDamage(float damage)
    {

    }

    private void OnEnable()
    {
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(2f);
        //float total = 0f;
        //foreach (Ball b in balls)
        //{
        //    total += b.body.mass;
        //}
        //mainBody.useAutoMass = false;
        //mainBody.mass = total;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == LevelManager.Instance.TerrainTag)
        {

        }
    }
}
