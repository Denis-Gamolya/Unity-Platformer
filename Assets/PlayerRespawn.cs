using System;
using UnityEngine;
using System.Collections;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField]
    private Transform respawnPoint;
    [SerializeField]
    private float respawnDelay = 0.1f;
    private Rigidbody2D rb;
    private Collider2D col;
    private bool isDead;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    // Update is called once per frame
 private void OnTriggerEnter2D(Collider2D other)
    {
        if (isDead) return;

        // Option A: tag check (recommended)
        if (other.CompareTag("Hazards"))
            StartCoroutine(DieAndRespawn());
    }
    private IEnumerator DieAndRespawn()
    {
        isDead = true;

        rb.linearVelocity = Vector2.zero;
         rb.angularVelocity = 0f;
         rb.simulated = false;   // <- stops falling/physics

    yield return new WaitForSeconds(respawnDelay);

    transform.position = respawnPoint.position;

    rb.simulated = true;    // <- physics back on
    rb.linearVelocity = Vector2.zero;
    rb.angularVelocity = 0f;
        isDead = false;
    }
}
