using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Curve Reference", menuName = "DataReference/CurveReference")]
public class CurveReference : ScriptableObject
{
    public AnimationCurve curve;
}
