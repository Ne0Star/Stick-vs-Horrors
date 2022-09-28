using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomShadow : MonoBehaviour
{
    [SerializeField] private SpriteRenderer r;
    [SerializeField] private Transform alphaTarget;
    [SerializeField] private bool alphaToDistance, scaleToDistance, blockRotate;
    [SerializeField] private float maxAlphaDistance = 10f, maxScaleDistance = 5f, scaleMultipler, alphaMultipler;
    [SerializeField] private float fixedY;
    [SerializeField] private Vector3 startScale;

    private void Start()
    {
        startScale = transform.localScale;
    }

    public void SetY(float y)
    {
        fixedY = y;
    }

    private void Update()
    {
        transform.position = new Vector3(alphaTarget.transform.position.x, fixedY);
        float distanceY = Vector2.Distance(new Vector2(0, transform.position.y), new Vector2(0, alphaTarget.position.y));
        float distance = Vector2.Distance(transform.position, alphaTarget.position);
        if (alphaToDistance)
        {
            r.color = new Color(r.color.r, r.color.g, r.color.b, Mathf.InverseLerp(0, distance, maxAlphaDistance) * alphaMultipler);
        }
        if (scaleToDistance)
        {
            float scale = Mathf.InverseLerp(0, distanceY, maxScaleDistance * scaleMultipler);
            transform.localScale = new Vector3(startScale.x * scale, startScale.y * scale, startScale.z * scale);
        }
    }
}
