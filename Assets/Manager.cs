using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public void spawn(GameObject spawnee)
    {
        Instantiate(spawnee);
    }
}
