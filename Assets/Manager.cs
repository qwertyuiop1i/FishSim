using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
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
        
    }
    public void changeVal(float v)
    {
        timeSpeed = v;
            }
    public void Update()
    {
        Time.timeScale = timeSpeed;
        t += Time.deltaTime;
        if (t > foodSpawnSpeed)
        {
            Instantiate(fd);
            t = 0f;

        }
    }
    public void spawn(GameObject spawnee)
    {
        Instantiate(spawnee);
    }

    
}
