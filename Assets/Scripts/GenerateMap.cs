using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GenerateMap : MonoBehaviour
{
    public GameObject grass, dirt, tower;
    public GameObject[] weapons;
    public GameObject[] obstacles;
    Vector2Int mapSize = new Vector2Int(10, 10);
    int[,] map;
    Vector2Int start;

    // Instantiate GameObject as a child
    void InstantiateChild(GameObject gameObject, Vector3 position)
    {
        GameObject child = Instantiate(gameObject, position, Quaternion.identity);
        child.transform.parent = this.transform;
    }

    // Get the neighbors of a node in an adjacency matrix
    List<Vector2Int> GetNeighbors(int x, int y)
    {
        return new List<Vector2Int>(4)
        {
            new Vector2Int(x, y + 1),
            new Vector2Int(x, y - 1),
            new Vector2Int(x + 1, y),
            new Vector2Int(x - 1, y)
        };
    }

    // Shuffle a list
    public static IList<T> Shuffle<T>(IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }

        return list;
    }

    // Determine a random path from start to the tower
    bool RecursiveDFS(int x, int y, bool[,] visited)
    {
        if (map[x, y] == 2)
        {
            return true;
        }

        visited[x, y] = true;

        foreach (Vector2Int n in Shuffle(GetNeighbors(x, y)))
        {
            // Ensure not out of the map or a border tile (to make it interesting)
            if (n.x >= 0 &&
                n.x < mapSize.x &&
                n.y >= 0 &&
                n.y < mapSize.y &&
                !visited[n.x, n.y])
            {
                if (RecursiveDFS(n.x, n.y, visited))
                {
                    map[x, y] = 1;
                    return true;
                }
            }
        }

        return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Generate a int map which will determine what tiles are what
        map = new int[mapSize.x, mapSize.y];
        for (int y = 0; y < mapSize.y; y++)
        {
            for (int x = 0; x < mapSize.x; x++)
            {
                map[x, y] = 0;
            }
        }

        // Determine the starting position, this is where the enemies will spawn
        start = new Vector2Int(0, Random.Range(0, mapSize.y));
        map[start.x, start.y] = 1;

        // The end will be where the tower is, the location the enemies will have to reach
        Vector2Int end = new Vector2Int(mapSize.x - 1, Random.Range(0, mapSize.y));
        map[end.x, end.y] = 2;

        bool[,] visited = new bool[mapSize.x, mapSize.y];

        // Add code here to make the initial visited tiles, so that the path is more interesting

        RecursiveDFS(start.x, start.y, visited);

        // Spawn in random obstacles
        for (int i = 0; i < 10; i++)
        {
            int x = Random.Range(0, mapSize.x);
            int y = Random.Range(0, mapSize.y);
            if (map[x, y] == 0)
            {
                map[x, y] = 4;
            }
        }

        // Map the integers to actual prefabs
        for (int y = 0; y < mapSize.y; y++)
        {
            for (int x = 0; x < mapSize.x; x++)
            {
                Vector3 position = new Vector3(x, 0, y);

                switch (map[x, y])
                {
                    case 0: // Grass
                        InstantiateChild(grass, position);
                        break;
                    case 1: // Dirt
                        InstantiateChild(dirt, position);
                        break;
                    case 2: // Tower
                        InstantiateChild(grass, position);
                        InstantiateChild(tower, position + tower.transform.position);
                        break;
                    case 3: // Weapons
                        InstantiateChild(grass, position);
                        GameObject weapon = weapons[Random.Range(0, weapons.Length)];
                        InstantiateChild(weapon, position + weapon.transform.position);
                        break;
                    case 4: // Obstacles
                        InstantiateChild(grass, position);
                        InstantiateChild(obstacles[Random.Range(0, obstacles.Length)], position);
                        break;
                    default:
                        break;
                }
            }
        }

        // Set the camera to the correct location 
        Camera.main.transform.position = new Vector3(mapSize.x / 2f, 10f, 1f);
        Camera.main.transform.eulerAngles = new Vector3(75f, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
