
using UnityEngine;
[System.Serializable]
public struct MissilesType
{
    [SerializeField] private bool useRandomCurve;
    [SerializeField] private float randomMinX, randomMaxX;
    [SerializeField] private float randomMinY, randomMaxY;
    public float RandomMinX { get => randomMinX; set => randomMinX = value; }
    public float RandomMaxX { get => randomMaxX; set => randomMaxX = value; }
    public float RandomMinY { get => randomMinY; set => randomMinY = value; }
    public float RandomMaxY { get => randomMaxY; set => randomMaxY = value; }
    public bool UseRandomCurve { get => useRandomCurve; set => useRandomCurve = value; }



    public void SetRandomKeys()
    {
        SetRandom(ref yOffset, randomMinY, randomMaxY);
        SetRandom(ref xOffset, randomMinX, randomMaxX);
    }

    private void SetRandom(ref AnimationCurve curve, float min, float max)
    {
        AnimationCurve result = new AnimationCurve();
        Keyframe[] keys = curve.keys;
        for (int I = 0; I < keys.Length; I++)
        {
            if (I > 0 && I < keys.Length - 1)
            {
                Keyframe key = keys[I];
                key.value = Random.Range(key.value - min, key.value + max) * -1f;
                keys[I] = key;
            }
        }
        curve.keys = keys;
    }

    public AnimationCurve SetRandomKeys(AnimationCurve origin, float minValue, float maxValue)
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

    [SerializeField] private AnimationCurve yOffset, xOffset, speedInterpolator;
    public AnimationCurve YOffset { get => yOffset; set => yOffset = value; }
    public AnimationCurve XOffset { get => xOffset; set => xOffset = value; }
    public AnimationCurve SpeedInterpolator { get => speedInterpolator; set => speedInterpolator = value; }
}