using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MachineGun : Gun
{
    [SerializeField] private bool batchToParent = true;
    [SerializeField] private int batchCount;
    [SerializeField] private LineRenderer[] lines;
    [SerializeField] private LineRenderer prefabLine;
    [SerializeField] private int currentIndex = 0;
    private void Awake()
    {

        lines = new LineRenderer[batchCount];
        for (int i = 0; i < batchCount; i++)
        {
            lines[i] = Instantiate<LineRenderer>(prefabLine);
            lines[i].gameObject.SetActive(false);
            lines[i].gameObject.name = "Patron: " + i;
            if (batchToParent)
                lines[i].gameObject.transform.parent = transform;
        }
    }

    private void Update()
    {
        if (currentIndex >= batchCount)
            currentIndex = 0;

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            LineRenderer line = lines[currentIndex];
            line.gameObject.SetActive(true);
            line.positionCount = 2;
            float distance = Vector2.Distance(transform.position, worldPos);
            float startDistance = Vector2.Distance(transform.position, worldPos);
            line.SetPosition(0, transform.position);
            DOTween.To(() => distance, x => distance = x, 0, 1f / startDistance * attackSpeed).OnUpdate(() =>
            {
                Vector2 dir = ((Vector2)transform.position - worldPos).normalized;
                float total = startDistance - distance;
                line.SetPosition(1, transform.position + GameUtils.ClampMagnitude(dir, total,total));
            });




            currentIndex++;
        }
    }


}
