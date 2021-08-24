using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDispenser
{
    float DispensingGas(float amount, IGasReceiver receiver);
}
