using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform heightTarget;
    [SerializeField] private TrajectorySystem trajectory;
    [SerializeField] private PatronPool pool;
    [SerializeField] private PatronCreator patronTrajectory;
    [SerializeField] private Quaternion patronStartRotation;
    [SerializeField] private Transform target;
    [SerializeField] private GunController gunController;
    [SerializeField] private float distance;
    [SerializeField] private SpriteRenderer render;
    private void Start()
    {
        if (!pool)
            pool = LevelManager.Instance.PatronPool;

        StartCoroutine(WaitAttack());

    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position + (transform.position - target.position) * distance, 2f);
    }
    public int created, reached, animated, canceled;


    [SerializeField] private bool attack = false;
    [SerializeField] private float attackSpeed;
    private IEnumerator WaitAttack()
    {
        yield return new WaitUntil(() => attack == true);
        while (attack)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
Vector2 dir = transform.position + (transform.position - target.position) * distance;
            Patron patron = pool.GetFreePatron(transform.position, GameUtils.LookAt2D(transform, dir, 0f));
            if (patron)
            {
                created++;
                patron.Shadow.SetY(heightTarget.position.y);
                patron.SetMaxY(heightTarget.position.y + 0.5f);
                patron.SetOrder("Player", render.sortingOrder);
                DynamicData dd = patronTrajectory.GetMoveData(patron.transform, dir, transform);
                gunController.CreateDrag();
                patron.AnimateExplose(() =>
                {
                    animated++;

                    trajectory.CreatePatron(dd, (p, t) =>
                    {
                        reached++;
                        patron.OnKill?.Invoke();
                    }, () =>
                    {
                        canceled++;
                        reached++;
                        patron.OnKill?.Invoke();
                    });

                });
            }

            yield return new WaitForSeconds(attackSpeed + Random.Range(0, attackSpeed));
        }
        StartCoroutine(WaitAttack());
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            attack = false;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            attack = true;
        }
    }

    //    //[SerializeField] private float maxY;
    //    [SerializeField] private GunController gunController;
    //    [SerializeField] private Transform heightTarget;
    //    [SerializeField] private bool inverseDir;
    //    [SerializeField] private Transform dirTarget;
    //    [SerializeField] private float patronPower;
    //    [SerializeField] private Patron patronPrefab;
    //    private float facingMaxY;
    //    private void FixedUpdate()
    //    {
    //        if (Input.GetMouseButton(0))
    //        {
    //            Patron patron = LevelManager.Instance.PatronPool.GetFreePatron(transform.position);
    //            patron.Shadow.SetY(heightTarget.position.y);
    //            patron.SetMaxY(heightTarget.position.y + 0.5f);
    //            Rigidbody2D body = patron.Body;
    //            Vector2 dir = inverseDir ? (dirTarget.position - transform.position).normalized : (transform.position - dirTarget.position).normalized;
    //gunController.CreateDrag();
    //            patron.AnimateExplose(() =>
    //            {
    //                body.AddForce(dir * patronPower, ForceMode2D.Impulse);
    //            });

    //        }
    //    }
}
