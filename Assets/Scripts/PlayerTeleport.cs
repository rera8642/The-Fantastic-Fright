using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    [SerializeField] private LayerMask teleportLayer;
    [SerializeField] private GameObject teleportVisualPrefab;
    private GameObject teleportVisual;
    private Vector3 finalPosition = Vector3.zero;
    
    void Update()
    {
        if (Input.GetMouseButtonUp(0)) {
            transform.position = finalPosition;
            if (teleportVisual) {
                Destroy(teleportVisual);
            }
            finalPosition = transform.position;
        } if (Input.GetMouseButton(0)) {
            RaycastHit hit;
            bool hitFound = Physics.Raycast(
                transform.position,
                Camera.main.transform.forward,
                out hit,
                Mathf.Infinity,
                teleportLayer
            );
            if (hitFound) {
                if (!teleportVisual) {
                    teleportVisual = Instantiate(teleportVisualPrefab);
                }
                finalPosition = new Vector3(
                    hit.point.x,
                    transform.position.y,
                    hit.point.z
                );
                teleportVisual.transform.position = new Vector3(
                    finalPosition.x,
                    hit.point.y,
                    finalPosition.z
                );
            }
        }
    }
}
