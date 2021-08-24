using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGasReceiver
{
    void ReceiveGas(IDispenser dispenser);

    float HowMuch();
}
