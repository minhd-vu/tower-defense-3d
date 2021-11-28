using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemies;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", 5, 5);

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Spawn an enemy at the start position
    private void SpawnEnemy()
    {
        GameObject enemy = enemies[Random.Range(0, enemies.Length)];
        var start = GenerateMap.instance.GetStart();
        var child = Instantiate(enemy, new Vector3(start.x, 0, start.y) + enemy.transform.position, Quaternion.identity);
        child.transform.parent = this.transform;
    }

}
