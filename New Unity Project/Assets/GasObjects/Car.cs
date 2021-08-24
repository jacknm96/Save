using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : GasContainerBase, IGasReceiver
{
    public float gasWanted;

    [ContextMenu("pump gas")]
    public void ReceiveGas(IDispenser dispenser)
    {
        if (availableGas + gasWanted > maxGas)
        {
            gasWanted = maxGas - availableGas;
        }
        availableGas += dispenser.DispensingGas(gasWanted, this);
    }

}
