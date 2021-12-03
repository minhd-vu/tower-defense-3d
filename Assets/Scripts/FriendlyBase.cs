using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyBase : MonoBehaviour
{
    public int startHealth = 10000;
    private int health;
    // Start is called before the first frame update
    void Start()
    {   //  Update health
        health = startHealth;
    }

    // Update is called once per frame
    void Update()
    {   //  TO BE Implemented:
        //  If enemy collides, subtract corresponding health
    }
}
