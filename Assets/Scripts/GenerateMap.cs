using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GenerateMap : MonoBehaviour
{
    public GameObject grass, dirt, tower;
    public GameObject[] weapons;
    public Vector2Int mapSize;

    // Start is called before the first frame update
    void Start()
    {
        for (int y = 0; y < mapSize.y; y++)
        {
            for (int x = 0; x < mapSize.x; x++)
            {
                Vector3 position = new Vector3(x, 0, y);
                GameObject tile = Instantiate(grass, position, Quaternion.identity);
                tile.transform.parent = this.transform;
            }
        }

        Camera.main.transform.position = new Vector3(mapSize.x / 2f, 10f, 1f);
        Camera.main.transform.eulerAngles = new Vector3(75f, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
