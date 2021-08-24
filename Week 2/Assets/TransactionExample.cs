using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StoreTransaction;

public class TransactionExample : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Transaction.Payment("chips", 1.50f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
