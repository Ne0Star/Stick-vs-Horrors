using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ball : MonoBehaviour
{
    public LineRenderer line;
    public SpringJoint2D spring;
    public Rigidbody2D body;

    private void Awake()
    {
        if (!line) line = GetComponent<LineRenderer>();
        if (!spring) spring = GetComponent<SpringJoint2D>();
        if (!body) body = GetComponent<Rigidbody2D>();

        float s = Random.Range(1f, 4f);
        transform.localScale = new Vector3(s, s, s);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {

        //if(Input.GetMouseButtonDown(0))
        //{
        //    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 50f);
        //    if (hit.collider != null)
        //    {
        //        hit.transform.gameObject.SetActive(false    );
        //    }
        //}

        if (line)
        {
            line.positionCount = 2;
            line.SetPosition(0, transform.position);
            line.SetPosition(1, spring.connectedBody.transform.TransformPoint(spring.connectedAnchor));
        }
    }
    private void OnDrawGizmos()
    {
        if (!spring) spring = GetComponent<SpringJoint2D>();
        if (line)
        {
            line.positionCount = 2;
            line.SetPosition(0, transform.position);
            line.SetPosition(1, spring.connectedBody.transform.TransformPoint(spring.connectedAnchor));
        }
    }
}
