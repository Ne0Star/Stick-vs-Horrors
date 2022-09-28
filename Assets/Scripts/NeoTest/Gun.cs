using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    //[SerializeField] private float maxY;
    [SerializeField] private GunController gunController;
    [SerializeField] private Transform heightTarget;
    [SerializeField] private bool inverseDir;
    [SerializeField] private Transform dirTarget;
    [SerializeField] private float patronPower;
    [SerializeField] private Patron patronPrefab;
    private float facingMaxY;
    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            Patron patron = LevelManager.Instance.PatronPool.GetFreePatron(transform.position);
            patron.Shadow.SetY(heightTarget.position.y);
            patron.SetMaxY(heightTarget.position.y + 0.5f);
            Rigidbody2D body = patron.Body;
            Vector2 dir = inverseDir ? (dirTarget.position - transform.position).normalized : (transform.position - dirTarget.position).normalized;
gunController.CreateDrag();
            patron.AnimateExplose(() =>
            {
                body.AddForce(dir * patronPower, ForceMode2D.Impulse);
            });

        }
    }
}
