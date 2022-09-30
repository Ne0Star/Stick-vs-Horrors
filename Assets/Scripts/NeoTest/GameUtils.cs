using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;

public static class GameUtils
{
    public static AnimationCurve SetRandomKeys(AnimationCurve origin, float minValue, float maxValue)
    {
        AnimationCurve result = new AnimationCurve();
        Keyframe[] keys = origin.keys;
        for (int I = 0; I < keys.Length; I++)
        {
            if (I > 0 && I < keys.Length - 1)
            {
                Keyframe key = keys[I];
                key.value = Random.Range(key.value - minValue, key.value + maxValue) * -1f;
                keys[I] = key;
            }
        }
        result.keys = keys;
        return result;
    }
    public static float CalculateScaleExtents(SpriteRenderer renderer)
    {
        float ScaleX = renderer.bounds.extents.x;
        float ScaleY = renderer.bounds.extents.y;
        float result = ScaleX > ScaleY ? ScaleX : ScaleY;
        return result;
    }
    public static float RoundToValue(float round, float value)
    {
        float result = 0f;
        if (round % value > (value / 2f))
            result = (int)(round / value) * value + value;
        else
            result = (int)(round / value) * value;
        return result;
    }

    /// <summary>
    /// Возвращает true если layer содержится в маске в включённом состоянии
    /// </summary>
    /// <returns></returns>
    public static bool LayerEquals(LayerMask mask, int layer)
    {
        return mask == (mask | (1 << layer));
    }

    /// <summary>
    /// Аналогичен одноимённому методу, имеет минимальный радиус сжатия
    /// </summary>
    /// <param name="v"></param>
    /// <param name="max"></param>
    /// <param name="min"></param>
    /// <returns></returns>
    public static Vector3 ClampMagnitude(Vector3 v, float max, float min)
    {
        double sm = v.sqrMagnitude;
        if (sm > (double)max * (double)max) return v.normalized * max;
        else if (sm < (double)min * (double)min) return v.normalized * min;
        return v;
    }

    public static bool LookAt2DSmooth(Transform whom, Vector3 where, float offset, float time, float distanceToComplete)
    {
        bool result = false;
        float angle = GameUtils.RoundToValue(Mathf.Atan2(where.y - whom.transform.position.y, where.x - whom.transform.position.x) * Mathf.Rad2Deg - offset, 0.05f);
        Quaternion total = Quaternion.Euler(whom.transform.rotation.eulerAngles.x, whom.transform.rotation.eulerAngles.y, angle);
        whom.transform.rotation = Quaternion.Lerp(whom.transform.rotation, total, time);
        if (GameUtils.RoundToValue(Vector2.Distance(new Vector2(total.z, 0), new Vector2(whom.transform.rotation.z, 0)), 0.05f) <= distanceToComplete) result = true;
        return result;
    }
    public static void LookAt2DSmooth(TransformAccess whom, Vector3 where, float offset, float time, float distanceToComplete, System.Action onComplete)
    {
        float angle = GameUtils.RoundToValue(Mathf.Atan2(where.y - whom.position.y, where.x - whom.position.x) * Mathf.Rad2Deg - offset, 0.05f);
        Quaternion total = Quaternion.Euler(whom.rotation.eulerAngles.x, whom.rotation.eulerAngles.y, angle);
        whom.rotation = Quaternion.Lerp(whom.rotation, total, time);
        if (GameUtils.RoundToValue(Vector2.Distance(new Vector2(total.z, 0), new Vector2(whom.rotation.z, 0)), 0.05f) <= distanceToComplete) onComplete();
    }
    public static void LookAt2DSmooth(Transform whom, Vector3 where, float offset, float time, float distanceToComplete, System.Action onComplete)
    {
        float angle = GameUtils.RoundToValue(Mathf.Atan2(where.y - whom.position.y, where.x - whom.position.x) * Mathf.Rad2Deg - offset, 0.05f);
        Quaternion total = Quaternion.Euler(whom.rotation.eulerAngles.x, whom.rotation.eulerAngles.y, angle);
        whom.rotation = Quaternion.Lerp(whom.rotation, total, time);
        if (GameUtils.RoundToValue(Vector2.Distance(new Vector2(total.z, 0), new Vector2(whom.rotation.z, 0)), 0.05f) <= distanceToComplete) onComplete();
    }

    /// <summary>
    /// whom поворачивается в сторону where, с оффестом offset
    /// </summary>
    /// <param name="whom"></param>
    /// <param name="where"></param>
    /// <param name="offset"></param>
    public static Quaternion LookAt2DValue(Transform whom, Transform where, float offset)
    {
        float angle = Mathf.Atan2(where.transform.position.y - whom.transform.position.y, where.transform.position.x - whom.transform.position.x) * Mathf.Rad2Deg - offset;
        Quaternion total = Quaternion.Euler(whom.transform.rotation.eulerAngles.x, whom.transform.rotation.eulerAngles.y, angle);
        return total;
    }

    /// <summary>
    /// whom поворачивается в сторону where, с оффестом offset
    /// </summary>
    /// <param name="whom"></param>
    /// <param name="where"></param>
    /// <param name="offset"></param>
    public static void LookAt2D(Transform whom, Transform where, float offset, float duration)
    {
        float angle = Mathf.Atan2(where.transform.position.y - whom.transform.position.y, where.transform.position.x - whom.transform.position.x) * Mathf.Rad2Deg - offset;
        Quaternion total = Quaternion.Euler(whom.transform.rotation.eulerAngles.x, whom.transform.rotation.eulerAngles.y, angle);
        whom.transform.DORotateQuaternion(total, duration);
        //whom.transform.rotation = total;
    }
    /// <summary>
    /// whom поворачивается в сторону where с оффестом offset за duration секунд, по оконачанию отправляет событие
    /// </summary>
    /// <param name="whom"></param>
    /// <param name="where"></param>
    /// <param name="offset"></param>
    public static void LookAt2D(Transform whom, Transform where, float offset, float duration, System.Action complete)
    {
        float angle = Mathf.Atan2(where.transform.position.y - whom.transform.position.y, where.transform.position.x - whom.transform.position.x) * Mathf.Rad2Deg - offset;
        Quaternion total = Quaternion.Euler(whom.transform.rotation.eulerAngles.x, whom.transform.rotation.eulerAngles.y, angle);
        whom.transform.DORotateQuaternion(total, duration).OnKill(() => complete());
        //whom.transform.rotation = total;
    }

    /// <summary>
    /// whom поворачивается в сторону where, с оффестом offset
    /// </summary>
    /// <param name="whom"></param>
    /// <param name="where"></param>
    /// <param name="offset"></param>
    public static void LookAt2D(Rigidbody2D whom, Rigidbody2D where, float offset)
    {
        float angle = Mathf.Atan2(where.transform.position.y - whom.transform.position.y, where.transform.position.x - whom.transform.position.x) * Mathf.Rad2Deg - offset;
        whom.rotation = angle;
    }
    /// <summary>
    /// whom поворачивается в сторону where, с оффестом offset
    /// </summary>
    /// <param name="whom"></param>
    /// <param name="where"></param>
    /// <param name="offset"></param>
    public static void LookAt2D(TransformAccess whom, Vector2 where, float offset)
    {
        float angle = Mathf.Atan2(where.y - whom.position.y, where.x - whom.position.x) * Mathf.Rad2Deg;
        Quaternion total = Quaternion.Euler(whom.rotation.eulerAngles.x, whom.rotation.eulerAngles.y, angle + offset);

        whom.rotation = total;
    }
    ///// <summary>
    ///// whom поворачивается в сторону where, с оффестом offset
    ///// </summary>
    ///// <param name="whom"></param>
    ///// <param name="where"></param>
    ///// <param name="offset"></param>
    //public static void LookAt2D(Transform whom, Vector2 where, float offset)
    //{
    //    float angle = Mathf.Atan2(where.y - whom.position.y, where.x - whom.position.x) * Mathf.Rad2Deg;
    //    Quaternion total = Quaternion.Euler(whom.rotation.eulerAngles.x, whom.rotation.eulerAngles.y, angle + offset);

    //    whom.rotation = total;
    //}

    /// <summary>
    /// whom поворачивается в сторону where, с оффестом offset
    /// </summary>
    /// <param name="whom"></param>
    /// <param name="where"></param>
    /// <param name="offset"></param>
    public static Quaternion LookAt2D(Transform whom, Vector2 where, float offset)
    {
        float angle = Mathf.Atan2(where.y - whom.position.y, where.x - whom.position.x) * Mathf.Rad2Deg;
        Quaternion total = Quaternion.Euler(whom.rotation.eulerAngles.x, whom.rotation.eulerAngles.y, angle + offset);

        return total;
    }

    /// <summary>
    /// whom поворачивается в сторону where, с оффестом offset
    /// </summary>
    /// <param name="whom"></param>
    /// <param name="where"></param>
    /// <param name="offset"></param>
    public static Quaternion LookAt2D(Transform whom, Transform where, float offset)
    {
        float angle = Mathf.Atan2(where.transform.position.y - whom.transform.position.y, where.transform.position.x - whom.transform.position.x) * Mathf.Rad2Deg - offset;
        Quaternion total = Quaternion.Euler(whom.transform.rotation.eulerAngles.x, whom.transform.rotation.eulerAngles.y, angle);
        return total;
    }

    /// <summary>
    /// whom поворачивается в сторону where, с оффестом offset
    /// </summary>
    /// <param name="whom"></param>
    /// <param name="where"></param>
    /// <param name="offset"></param>
    public static void LookAt2D(Transform whom, Vector2 where, float offset, float duration)
    {
        float angle = Mathf.Atan2(where.y - whom.position.y, where.x - whom.position.x) * Mathf.Rad2Deg;
        Quaternion total = Quaternion.Euler(whom.rotation.eulerAngles.x, whom.rotation.eulerAngles.y, angle + offset);
        whom.transform.DORotateQuaternion(total, duration);
    }
}