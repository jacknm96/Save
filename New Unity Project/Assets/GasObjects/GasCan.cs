using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasCan : GasContainerBase, IDispenser, IGasReceiver
{
    public RefuellingStation gasStation = new RefuellingStation();
    public float availableGas;
    public float gasWanted;
    public float AvailableGas
    {
        get
        {
            return availableGas;
        }
        set
        {
            if (availableGas + value > maxGas)
            {
                availableGas = maxGas;
            }
        }
    }

    public void ReceiveGas(IDispenser receiver)
    {
        availableGas += gasStation.DispensingGas(gasWanted, this);
    }

    public float HowMuch()
    {
        return maxGas - availableGas;
    }

    public float DispensingGas(float amount, IGasReceiver other)
    {
        return 0;
    }
}
