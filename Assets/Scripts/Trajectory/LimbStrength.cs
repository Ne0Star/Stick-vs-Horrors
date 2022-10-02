using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbStrength : MonoBehaviour
{
    [SerializeField] private Collider2D colider;
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private float maxStrength;
    [SerializeField] private float currentStrength;
    [SerializeField] private float strength;

    private Vector3 startPosition;
    private Quaternion startRotation;
    private Transform startParent;

    private void Awake()
    {
        startParent = transform.parent;
        startPosition = transform.localPosition;
        startRotation = transform.localRotation;
        body = gameObject.AddComponent<Rigidbody2D>();
        colider = gameObject.GetComponent<Collider2D>();
        body.isKinematic = true;
        //body.simulated = false;
    }

    private void OnBecameVisible()
    {

    }

    private void OnBecameInvisible()
    {
        if(colider)
        colider.enabled = true;
        body.isKinematic = true;
        body.velocity = Vector2.zero;
        body.angularDrag = 0f;
        body.angularVelocity = 0f;
        transform.parent = startParent;
        transform.localPosition = startPosition;
        transform.localRotation = startRotation;
    }

    public void TempDisableCollider()
    {
        colider.enabled = false;
    }

    public void TakeDamage(float damage, float gunPower, Vector3 patronPosition)
    {
        currentStrength = Mathf.Clamp(currentStrength - damage, 0, maxStrength);
        if (currentStrength == 0)
        {
            colider.enabled = false;
            transform.parent = null;
            body.isKinematic = false;
            //body.simulated = true;
            body.AddForce((transform.position - patronPosition) * gunPower, ForceMode2D.Impulse);
        } else
        {
            colider.enabled = true;
        }
    }

    private void Start()
    {
        LevelManager.Instance.LimbManager.RegisterLimb(this);
    }


}
