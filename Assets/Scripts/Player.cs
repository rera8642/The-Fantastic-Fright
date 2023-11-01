using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask teleportLayer;
    [SerializeField] private LayerMask ghostLayer;
    [SerializeField] private LayerMask ringTossLayer;
    [SerializeField] private GameObject teleportVisualPrefab;
    [SerializeField] private GameObject ringVisual;
    [SerializeField] private GameObject ringPrefab;
    [SerializeField] private GameObject ringCount;
    [SerializeField] private GameObject ghostCount;
    [SerializeField] private UnityEvent<int> catchGhost;
    [SerializeField] private UnityEvent<int> throwRing;
    [SerializeField] private Vector3 ringOffset;
    [SerializeField] private int EndGameDelay = 3;
    [SerializeField] private int maxRings = 5;
    [SerializeField] private int forwardForce;
    [SerializeField] private int upwardForce;
    private GameObject teleportVisual;
    private Vector3 finalPosition = Vector3.zero;
    private bool canTeleport = true;
    private bool inRingToss = false;
    public int rings = 0;
    
    void Update()
    {
        if (inRingToss) {
            if (Input.GetMouseButtonDown(0)) {
                rings -= 1;
                GameObject ring = Instantiate(ringPrefab);
                ring.transform.position = transform.position + ringOffset;
                ring.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * forwardForce + transform.up * upwardForce);
                throwRing.Invoke(1);
                if (rings == 0) {
                    inRingToss = false;
                    ringVisual.SetActive(false);
                    canTeleport = false;
                    finalPosition = transform.position;
                }
            }
        } else {
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
                bool startRingToss = Physics.Raycast(
                    transform.position,
                    Camera.main.transform.forward,
                    out hit,
                    Mathf.Infinity,
                    ringTossLayer
                );
                if (startRingToss) {
                    ghostCount.SetActive(false);
                    ringCount.SetActive(true);
                    inRingToss = true;
                    ringVisual.SetActive(true);
                    rings = maxRings;
                } else {
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
    public void End()
    {
        StartCoroutine(LoadLevelAfterDelay(EndGameDelay));
    }

    IEnumerator LoadLevelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Title Screen");
    }
}
