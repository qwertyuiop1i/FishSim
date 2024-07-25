using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public int am;
    public int[] tracks;

    public float zoomedIn = 3.0f;
    public float zoomedOut = 8.0f;
    public Camera c;

    public Transform target;
    public float smoothSpeed = 0.125f;

    private bool following = false;

    public float timeSpeed = 1f;

    [SerializeField]
    private float foodSpawnSpeed;

    [SerializeField]
    private GameObject fd;

    public float t;
    public void Start()
    {
        c = GetComponent<Camera>();
    }
    public void changeVal(float v)
    {
        timeSpeed = v;
            }

    public void spawn(GameObject spawnee)
    {
        Instantiate(spawnee);
    }

    private void Update()
    {
        Time.timeScale = timeSpeed;
        t += Time.deltaTime;
        if (t > foodSpawnSpeed)
        {
            Instantiate(fd);
            t = 0f;

        }
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("fish"))
            {
                
                target = hit.transform;
                following = !following;
            }
        }

        if (following)
        {
            c.orthographicSize = zoomedIn;
            FollowTarget();
        }
        else
        {
            c.orthographicSize = zoomedOut;
            transform.position = new Vector3(0,0,-10);
        }
    }

    private void FollowTarget()
    {
        Vector3 desiredPosition = new Vector3(target.position.x, target.position.y, -10f);
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
    }

    private Vector3 velocity = Vector3.zero;
}

