using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBehavior : MonoBehaviour
{
    public float boidRadius = 2.5f;
    public float separationWeight = 1.5f;
    public float alignmentWeight = 1f;
    public float cohesionWeight = 1f;

    private List<GameObject> neighbors;
    public float maxSpeed = 4;
    public float steerStrength = 2;
    public float wanderStrength = 1;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private Vector2 desiredDirection;
    void Start()
    {
        desiredDirection = Vector2.zero;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        desiredDirection = (desiredDirection + Random.insideUnitCircle * wanderStrength).normalized;

        Vector2 desiredVelocity = desiredDirection * maxSpeed;
        Vector2 desiredSteeringForce = (desiredVelocity - rb.velocity) * steerStrength;

        Vector2 acceleration = Vector2.ClampMagnitude(desiredSteeringForce, steerStrength) / 1;
        rb.AddForce(acceleration, ForceMode2D.Force);

        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);

        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        if (rb.velocity.x > 0)
        {
            sr.flipX = false;
        }
        else if (rb.velocity.x < 0)
        {
            sr.flipX = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb.velocity = -rb.velocity;
    }
}
