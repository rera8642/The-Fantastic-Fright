using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class UpdateScore : MonoBehaviour
{
    [SerializeField] private TextMeshPro textMesh;
    [SerializeField] private int ghosts = 0;
    [SerializeField] private int totalGhosts = 11;
    [SerializeField] private UnityEvent endGame;
    void Start() {
        UpdateUI();
    }
    public void IncreaseScore(int scoreInc = 1) {
        ghosts += scoreInc;
        if (ghosts == totalGhosts) {
            endGame.Invoke();
        }
        UpdateUI();
    }

    private void UpdateUI() {
        textMesh.text = "Ghosts: " + ghosts + "/" + totalGhosts;
    }
}
