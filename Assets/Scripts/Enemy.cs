using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject deathVFX;
    [SerializeField] private int scorePerHit = 15;

    private ScoreBoard scoreBoard;
    GameObject parent;

    private void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
        parent = GameObject.FindWithTag("SpawnAtRuntime");
        AddRigidBody();
    }

    private void AddRigidBody()
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        KillEnemy();
    }
    private void ProcessHit()
    {
        scoreBoard.IncreaseScore(scorePerHit);
    }
    private void KillEnemy()
    {
        GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parent.transform;
        Destroy(this.gameObject);
    }

    
}
