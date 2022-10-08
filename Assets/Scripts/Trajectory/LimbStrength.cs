using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LimbStrength : MonoBehaviour
{
    public UnityEvent<LimbStrength> onBreak;
    public UnityEvent<float, LimbStrength> onDamage;

    [SerializeField] private LimbStrength childrenLimb;

    [SerializeField] private float addtiveDamage;

    [SerializeField] private Transform particle;
    [SerializeField] private Collider2D colider;
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private float maxStrength;
    [SerializeField] private float currentStrength;

    [SerializeField] private float currentTime;
    [SerializeField] private float particleDuration;
    [SerializeField] private bool waitParticle = false;

    private Vector3 startPosition;
    private Quaternion startRotation;
    private Transform startParent;

    public float MaxStrength { get => maxStrength; }
    public LimbStrength ChildrenLimb { get => childrenLimb; }
    public float CurrentStrength { get => currentStrength; }
    public float AddtiveDamage { get => addtiveDamage; }

    private void Awake()
    {
        body = gameObject.AddComponent<Rigidbody2D>();
        colider = gameObject.GetComponent<Collider2D>();
        body.isKinematic = true;

        onBreak.AddListener((s) =>
        {
            if (childrenLimb)

                childrenLimb.TakeDamage(9999f, 5, transform.position);
        });

        //body.simulated = false;
    }

    public void Tick()
    {
        if (!gameObject.activeInHierarchy) return;
        currentTime += 0.1f;
        if (currentTime >= particleDuration)
        {
            if (waitParticle)
            {
                if (particle)
                    particle.gameObject.SetActive(false);
            }
            currentTime = 0;
        }
    }
    private void OnEnable()
    {
        currentStrength = maxStrength;
        if (colider)
            colider.enabled = true;
        body.isKinematic = true;
        if (particle)
        {
            particle.gameObject.SetActive(false);
            waitParticle = false;
        }

        body.velocity = Vector2.zero;
        body.angularDrag = 0f;
        body.angularVelocity = 0f;
    }
    private void OnDisable()
    {
        if (childrenLimb)
            childrenLimb.TakeDamage(9999f, 5, transform.position);
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
        transform.parent = startParent;
        transform.localPosition = startPosition;
        transform.localRotation = startRotation;
    }
    //private void OnBecameInvisible()
    //{
    //    currentStrength = maxStrength;
    //    if (colider)
    //        colider.enabled = true;
    //    body.isKinematic = true;
    //    if (particle)
    //    {
    //        particle.gameObject.SetActive(false);
    //        waitParticle = false;
    //    }

    //    body.velocity = Vector2.zero;
    //    body.angularDrag = 0f;
    //    body.angularVelocity = 0f;
    //    transform.parent = startParent;
    //    transform.localPosition = startPosition;
    //    transform.localRotation = startRotation;
    //}



    public void TempDisableCollider()
    {
        colider.enabled = false;
    }

    public void TakeDamage(float damage, float gunPower, Vector3 patronPosition)
    {

        currentStrength = Mathf.Clamp(currentStrength - damage, 0, maxStrength);
        if (currentStrength == 0)
        {

            if (particle)
            {
                particle.gameObject.SetActive(true);
                waitParticle = false;
            }

            colider.enabled = false;

            if (transform.parent && transform.parent.gameObject.activeInHierarchy)
                transform.parent = null;

            body.isKinematic = false;
            //body.simulated = true;
            body.AddForce((transform.position - patronPosition) * gunPower, ForceMode2D.Impulse);
            onBreak?.Invoke(this);
        }
        else
        {
            if (particle)
            {
                particle.gameObject.SetActive(true);
                waitParticle = true;
            }

            currentTime = 0f;
            colider.enabled = true;
        }
        onDamage?.Invoke(damage, this);
    }

    private void Start()
    {
        LevelManager.Instance.LimbManager.RegisterLimb(this);
        startParent = transform.parent;
        startPosition = transform.localPosition;
        startRotation = transform.localRotation;
    }


}
