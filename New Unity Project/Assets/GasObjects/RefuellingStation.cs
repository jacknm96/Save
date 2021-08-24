using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefuellingStation : MonoBehaviour, IDispenser
{
    [SerializeField] float availableGas = 16f;

    public float DispensingGas(float amount, IGasReceiver receiver)
    {
        if (amount > availableGas)
        {
            amount = availableGas;
        }
        if (amount > receiver.HowMuch())
        {
            amount = receiver.HowMuch();
            availableGas -= amount;
            return amount;
        }
        else
        {
            availableGas -= amount;
            return amount;
        }
    }
    
    public float HowMuch()
    {
        return 0f;
    }
    [ContextMenu("Refuel")]
    public void ReceiveGas()
    {
        availableGas += 300;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IGasReceiver>() != null)
        {
            other.GetComponent<IGasReceiver>().ReceiveGas(this);
        }
    }
}