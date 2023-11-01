using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UpdateRings : MonoBehaviour
{
    [SerializeField] private TextMeshPro textMesh;
    [SerializeField] private GameObject ghostCounter;
    [SerializeField] private int rings = 5;
    [SerializeField] private int maxRings = 5;
    void Start() {
        UpdateUI();
    }
    public void DecreaseRings(int scoreInc = 1) {
        rings -= scoreInc;
        if (rings == 0) {
            gameObject.SetActive(false);
            // ghostCounter.SetActive(true);
            rings = maxRings;
        }
        UpdateUI();
    }

    private void UpdateUI() {
        textMesh.text = "Rings Left: " + rings;
    }
}
