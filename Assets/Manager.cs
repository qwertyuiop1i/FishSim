using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public float timeSpeed = 1f;
    public void Start()
    {
        
    }
    public void Update()
    {
        Time.timeScale = timeSpeed;
    }
    public void spawn(GameObject spawnee)
    {
        Instantiate(spawnee);
    }
}
