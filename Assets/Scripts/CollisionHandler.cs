using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float loadDelay = 1f; 
    [SerializeField] private ParticleSystem explosion;


    private void OnCollisionEnter(Collision other)
    {
        StartCrashSequence();
    }

    private void StartCrashSequence()
    {
        explosion.Play();
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<PlayerControlls>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        Invoke("ReloadLevel", loadDelay);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
