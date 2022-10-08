using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class ToggleAnimator : MonoBehaviour
{

    [System.Serializable]
    public struct ToggleAnimationData
    {
        public bool useDisable;
        public MaskableGraphic[] images;
        public Color enableColor, disableColor;
    }

    [SerializeField] private List<ToggleAnimationData> animationDatas;
    [SerializeField] private Toggle toggle;

    private void Awake()
    {
        toggle = GetComponent<Toggle>();
        foreach (ToggleAnimationData ad in animationDatas)
        {
            foreach (MaskableGraphic img in ad.images)
            {
                img.color = toggle.isOn ? ad.enableColor : (ad.useDisable ? ad.disableColor : img.color);
            }
        }

        toggle.onValueChanged.AddListener((v) =>
        {
            foreach (ToggleAnimationData ad in animationDatas)
            {
                foreach (MaskableGraphic img in ad.images)
                {
                    img.color = v ? ad.enableColor : (ad.useDisable ? ad.disableColor : img.color);
                }
            }
        });
    }

}
