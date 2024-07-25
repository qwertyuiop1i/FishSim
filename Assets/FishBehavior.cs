using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBehavior : MonoBehaviour
{
    public float boidRadius = 2.5f;
    public float separationWeight = 1.5f;
    public float alignmentWeight = 1f;
    public float cohesionWeight = 1f;
    public float otherSpeciesSeperationWeight = 0f;


    public float maxSpeed = 4;
    public float steerStrength = 2;
    public float wanderStrength = 1;

    public float foodWanderStrength = 3f;

    [SerializeField]
    private int neighborCount;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    public string species;

    private Vector2 desiredDirection;

    [SerializeField]
    public float foodBar;
    [SerializeField]
    private float startFood;


    [SerializeField]
    private Vector2 separationForce = Vector2.zero;
    [SerializeField]
    private Vector2 alignmentForce = Vector2.zero;
    [SerializeField]
    private Vector2 cohesionForce = Vector2.zero;
    [SerializeField]
    private Vector2 foodForce = Vector2.zero;

    void Start()
    {
        foodBar = startFood;

        desiredDirection = Vector2.zero;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        transform.position = new Vector2(Random.Range(-7.0f, 7.0f), Random.Range(-5.0f, 5.0f));

        neighborCount = 0;
    }

    void FixedUpdate()
    {
        foodBar -= Time.deltaTime;
        if (foodBar <= 0)
        {
            Debug.Log("A fish of species "+species+" died.");
            Destroy(gameObject);
            
        }


        neighborCount = 0;

        separationForce = Vector2.zero;
        alignmentForce = Vector2.zero;
        cohesionForce = Vector2.zero;
        foodForce = Vector2.zero;
        Collider2D[] neighbors = Physics2D.OverlapCircleAll(transform.position, boidRadius);

        foreach (Collider2D neighbor in neighbors)
        {
            Vector2 neighborOffset = (neighbor.transform.position - transform.position).normalized;

            if (neighbor.tag == "food")
            {
                foodForce += neighborOffset;
            }

            if (neighbor.gameObject.layer == LayerMask.NameToLayer("fish") && neighbor.gameObject != gameObject)
            {

                
                if ((!neighbor.CompareTag(species)))
                {
                    separationForce += neighborOffset * -1 * otherSpeciesSeperationWeight;
                }
                if (neighbor.CompareTag(species))
                {
                    neighborCount += 1;

                    // Separation
                    separationForce += neighborOffset * -1;

                    // Alignment
                    alignmentForce += neighbor.GetComponent<Rigidbody2D>().velocity.normalized * alignmentWeight;

                    // Cohesion
                    cohesionForce += (Vector2)neighbor.transform.position * cohesionWeight;



                }
            }
            


        }
        alignmentForce /= neighborCount;
        alignmentForce.Normalize();
        if (neighborCount != 0)
        {
            cohesionForce /= neighborCount;
            cohesionForce = cohesionForce - (Vector2)transform.position;
        }
        else
        {
            cohesionForce = Vector2.zero;
        }



        desiredDirection = (foodForce*foodWanderStrength+separationForce*separationWeight + alignmentForce*alignmentWeight + cohesionForce + new Vector2(Mathf.Cos(Random.Range(0, Mathf.PI * 2)), Mathf.Sin(Random.Range(0, Mathf.PI * 2))) * wanderStrength).normalized;

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
        desiredDirection *= -1;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, boidRadius);
    }
}
