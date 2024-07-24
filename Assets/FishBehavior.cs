using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBehavior : MonoBehaviour
{
    public float speed = 3f;
    public float rotationSpeed = 4f;
    public float boidRadius = 2.5f;
    public float separationWeight = 1.5f;
    public float alignmentWeight = 1f;
    public float cohesionWeight = 1f;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {

        float angle = Random.Range(0, 360) * Mathf.Deg2Rad;
        rb.velocity = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * speed;

       
    }


}
