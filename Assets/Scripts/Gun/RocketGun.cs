using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketGun : Gun
{
    [SerializeField] private Transform heightTarget;
    [SerializeField] private TrajectorySystem trajectory;
    [SerializeField] private PatronPool pool;
    [SerializeField] private PatronCreator patronTrajectory;
    [SerializeField] private Quaternion patronStartRotation;
    [SerializeField] private Transform target;
    [SerializeField] private GunController gunController;
    [SerializeField] private float distance;
    [SerializeField] private float splashRadius;
    [SerializeField] private SpriteRenderer render;
    private void Start()
    {

        if(!trajectory)
            trajectory = FindObjectOfType<TrajectorySystem>();

        if (!pool)
            pool = LevelManager.Instance.PatronPool;

        StartCoroutine(WaitAttack());

    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position + (transform.position - target.position) * distance, 2f);
        Debug.DrawRay(transform.position, (transform.position - target.position) * distance, Color.red);
    }
    public int created, reached, animated, canceled;

    Vector2 dir;
    [SerializeField] private bool attack = false;
    private IEnumerator WaitAttack()
    {
        yield return new WaitUntil(() => attack == true);
        while (attack)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dir = transform.position + (transform.position - target.position) * distance;
            Patron patron = pool.GetFreePatron(transform.position, GameUtils.LookAt2D(transform, dir, 0f));
            if (patron)
            {
                created++;
                //patron.Shadow.SetY(heightTarget.position.y);
                patron.SetMaxY(heightTarget.position.y + 0.5f);
                patron.SetOrder("Player", render.sortingOrder);
                DynamicData dd = patronTrajectory.GetMoveData(patron.transform, dir, transform);


                LimbStrength limb = null;

                RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.position - target.position, distance);
                if (hit.collider && hit.transform)
                {
                    limb = LevelManager.Instance.LimbManager.GetLimbById(hit.transform.GetInstanceID());
                    //Debug.Log(limb);
                    dd.pursue = true;
                    //dd.currentSpeed = Vector2.Distance(new Vector2(hit.point.x, hit.point.y), transform.position);
                    dd.endPosition = new Vector2(hit.point.x, hit.point.y);
                    dd.target = hit.transform;
                }

                if (limb)
                    limb.TempDisableCollider();






                //dd.pursue = true;
                //dd.target = 
                gunController.CreateDrag();


                    trajectory.CreatePatron(dd, (p, t) =>
                    {
                        reached++;
                        //Debug.Log(limb);
                        if (limb)
                        {
                            LevelManager.Instance.LimbManager.TakeSplashDamage(attackDamage, splashRadius, p.position);

                            //limb.TakeDamage(attackDamage, attackDamage / 4, p.position);
                            //Debug.Log("Taks");
                        }

                        patron.OnKill?.Invoke();
                    }, (e) =>
                    {
                        Debug.Log("Не удалось создать патрон: " + e);
                        canceled++;
                        reached++;
                        patron.OnKill?.Invoke();
                    });
                patron.AnimateExplose(() =>
                {
                    animated++;



                });
            }
            else
            {
                Debug.Log("НЕТ ПАТРОНОВ ??");
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
}
