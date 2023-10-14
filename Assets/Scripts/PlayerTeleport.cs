using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    [SerializeField] private LayerMask teleportLayer;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            bool hitFound = Physics.Raycast(
                transform.position,
                Camera.main.transform.forward,
                out hit,
                Mathf.Infinity,
                teleportLayer);
            Debug.Log("Click");
            if (hitFound) {
                Debug.Log("hit");
                transform.position = new Vector3(
                    hit.point.x,
                    transform.position.y,
                    hit.point.z
                );
            }
        }
    }
}
