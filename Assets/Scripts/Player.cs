using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    public int startingCurrency = 1000;
    private int currency;
    private int score = 0;
    public int maxHealth;
    private int health;

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
