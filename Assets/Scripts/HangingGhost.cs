using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class HangingGhost : MonoBehaviour
{
    [SerializeField] private LayerMask ringLayer;
    [SerializeField] private UnityEvent<int> catchGhost;
    private bool hasCollected = false;

    void OnCollisionEnter(Collision c) {
        if ((ringLayer.value & 1<<c.gameObject.layer) == 1<<c.gameObject.layer)
        {
            if (!hasCollected) {
                hasCollected = true;
                catchGhost.Invoke(1);
                Destroy(gameObject);
            }
        }
    }
}
