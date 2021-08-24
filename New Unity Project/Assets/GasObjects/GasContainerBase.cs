using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasContainerBase : MonoBehaviour
{
    public float availableGas = 10f;
    public float maxGas = 20f;

    public float HowMuch()
    {
        return maxGas - availableGas;
    }
}
