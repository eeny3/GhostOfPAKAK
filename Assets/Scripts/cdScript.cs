using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cdScript : MonoBehaviour
{
    public static cdScript instance { get; private set; }

    public Image maskQ;
    public Image maskE;
    public Image maskC;
    float originalSizeQ;
    float originalSizeE;
    float originalSizeC;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        originalSizeQ = maskQ.rectTransform.rect.width;
        originalSizeE = maskE.rectTransform.rect.width;
        originalSizeC = maskC.rectTransform.rect.width;
    }

    public void SetValue(float value, byte spell)
    {
        switch(spell)
        {
            case 1:
                maskQ.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, originalSizeQ * value);
                break;
            case 2:
                maskE.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, originalSizeE * value);
                break;
            case 3:
                maskC.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, originalSizeC * value);
                break;
        }
        //mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, originalSize * value);
    }
}
