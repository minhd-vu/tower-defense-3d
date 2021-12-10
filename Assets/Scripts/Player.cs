using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    public int startingCurrency = 1000;
    public int Currency { get; private set; }
    public int Score { get; private set; }
    public int maxHealth = 100;
    public int Health { get; private set; }

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Score = 0;
        Health = maxHealth;
        Currency = startingCurrency;
    }

    public void Damage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Health = 0;
        }
    }

    public bool Buy(int cost)
    {
        if (Currency < cost)
            return false;
            
        Currency -= cost;
        return true;
    }

    public void Reward(int currency)
    {
        Currency += currency;
        Score += 100;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
