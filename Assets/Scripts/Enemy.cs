using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    private int health;
    public float speed = 1f;
    private Vector3 target;
    private int pathIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        GetNextPath();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 dir = target - position;
        Debug.Log(dir);
        transform.Translate(dir.normalized * speed * Time.deltaTime);

        if (Vector3.Distance(position, target) <= 0.01f)
        {
            GetNextPath();
        }
    }

    void GetNextPath()
    {
        if (++pathIndex >= GenerateMap.path.Count)
        {
            Destroy(gameObject);
        }

        target = new Vector3(GenerateMap.path[pathIndex].x, 0, GenerateMap.path[pathIndex].y);
    }
}
