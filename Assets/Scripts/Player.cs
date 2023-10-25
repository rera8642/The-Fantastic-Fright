using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask teleportLayer;
    [SerializeField] private LayerMask ghostLayer;
    [SerializeField] private GameObject teleportVisualPrefab;
    [SerializeField] private UnityEvent<int> catchGhost;
    private GameObject teleportVisual;
    private Vector3 finalPosition = Vector3.zero;
    private bool canTeleport = true;
    
    void Update()
    {
        if (Input.GetMouseButtonUp(0)) {
            transform.position = finalPosition;
            if (teleportVisual) {
                Destroy(teleportVisual);
            }
            finalPosition = transform.position;
            canTeleport = true;
        }
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            bool hitFound = Physics.Raycast(
                transform.position,
                Camera.main.transform.forward,
                out hit,
                Mathf.Infinity,
                ghostLayer
            );
            if (hitFound) {
                canTeleport = false;
                if (catchGhost != null) {
                    catchGhost.Invoke(1);
                    hit.transform.gameObject.GetComponent<CatchGhost>().Catch();
                }
            }
        }
        if (Input.GetMouseButton(0) && canTeleport) {
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
