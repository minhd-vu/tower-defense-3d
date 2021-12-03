using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    public int startingCurrency = 1000;
    public int currency;
    public int score = 0;
    public int maxHealth = 100;
    public int health;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        currency = startingCurrency;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
