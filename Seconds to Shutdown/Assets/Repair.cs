using System;
using System.Collections;
using System.Collections.Generic;
using SUPERCharacter;
using UnityEngine;

public class Repair : MonoBehaviour, IInteractable
{
    public static event Action<float> PerformedRepair;

    [SerializeField]
    private float timeGain = 10.0f;

    private bool collected = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AttemptRepair();
        }
    }

    public bool Interact()
    {
        return AttemptRepair();
    }

    private bool AttemptRepair()
    {
        if (collected) return false;

        collected = true;
        PerformedRepair?.Invoke(timeGain);
        Destroy(gameObject);

        return true;
    }
}
