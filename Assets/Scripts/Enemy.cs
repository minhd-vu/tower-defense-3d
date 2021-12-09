using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Player plr;
    public int maxHealth = 100;
    public int Health { get; private set; }
    public float speed = 1f;
    private Vector3 target;
    private int pathIndex = 0;
    public int currencyDrop = 10;
    public int damage = 1;

    // Start is called before the first frame update
    void Start()
    {
        Health = maxHealth;

        // Get the first path the enemy should follow
        GetNextPath();
    }

    // Update is called once per frame
    void Update()
    {
        // Move the enemy towards the path node
        Vector3 position = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 dir = target - position;
        // Debug.Log(dir);
        transform.Translate(dir.normalized * speed * Time.deltaTime);

        // Give a new path node if the enemy has reach it
        if (Vector3.Distance(position, target) <= 0.01f)
        {
            GetNextPath();
        }
    }

    void GetNextPath()
    {
        var path = GenerateMap.instance.GetPath();
        // Destroy the enemy if it has reached the tower
        if (++pathIndex >= path.Count)
        {
            Player.instance.Damage(damage);
            Destroy(gameObject);
            return;
        }

        // Get the next path for the enemy to follow
        target = new Vector3(path[pathIndex].x, 0, path[pathIndex].y);
    }

    public void Damage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Health = 0;
            Player.instance.Reward(currencyDrop);
            Destroy(gameObject);
        }
    }
}
