using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchGhost : MonoBehaviour
{
    AudioSource audioSFX;

    void Start()
    {
        audioSFX = GetComponent<AudioSource>();
    }

    public void Catch()
    {
        audioSFX.Play();
        transform.position = new Vector3(0, -100, 0);
        Destroy(gameObject, 2f);
    }
}
