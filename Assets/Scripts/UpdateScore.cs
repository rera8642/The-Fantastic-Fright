using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UpdateScore : MonoBehaviour
{
    [SerializeField] private TextMeshPro textMesh;
    [SerializeField] private int ghosts = 0;
    [SerializeField] private int totalGhosts = 20;
    void Start() {
        UpdateUI();
    }
    public void IncreaseScore(int scoreInc = 1) {
        ghosts += scoreInc;
        UpdateUI();
    }

    private void UpdateUI() {
        textMesh.text = "Ghosts: " + ghosts + "/" + totalGhosts;
    }
}
