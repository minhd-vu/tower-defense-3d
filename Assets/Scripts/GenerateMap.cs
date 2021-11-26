using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

public class GenerateMap : MonoBehaviour
{
    public GameObject grass, dirt, tower;
    public GameObject[] weapons;
    public GameObject[] obstacles;
    public GameObject[] enemies;
    Vector2Int mapSize = new Vector2Int(10, 10);
    int[,] map;
    Vector2Int start;
    public static List<Vector2Int> path;

    // Instantiate GameObject as a child
    public void InstantiateChild(GameObject gameObject, Vector3 position)
    {
        GameObject child = Instantiate(gameObject, position, Quaternion.identity);
        child.transform.parent = this.transform;
    }

    // Get the neighbors of a node in an adjacency matrix
    List<Vector2Int> GetNeighbors(int x, int y)
    {
        return new List<Vector2Int>(4)
        {
            new Vector2Int(x + 1, y),
            new Vector2Int(x, y + 1),
            new Vector2Int(x, y - 1),
            new Vector2Int(x - 1, y),
        }.FindAll(e => InMapBounds(e.x, e.y));
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

    // Ensure that the point is within the bounds of the map
    bool InMapBounds(int x, int y)
    {
        return x >= 0 && x < mapSize.x && y >= 0 && y < mapSize.y;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Generate a int map which will determine what tiles are what
        map = new int[mapSize.x, mapSize.y];
        bool[,] visited = new bool[mapSize.x, mapSize.y];
        path = new List<Vector2Int>();

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

        // Generate waypoints for nodal pathfinding
        List<Vector2Int> waypoints = new List<Vector2Int>();
        waypoints.Add(start);
        for (int x = 3; x < 7; x += 2)
        {
            var pos = new Vector2Int(x, Random.Range(1, mapSize.y - 1));
            waypoints.Add(pos);
            map[pos.x, pos.y] = 3;
        }
        waypoints.Add(end);

        for (int i = 0; i < waypoints.Count - 1; i++)
        {
            FindPath(waypoints[i], waypoints[i + 1]);
        }

        // Set the tower position
        map[end.x, end.y] = 2;

        // Remove duplicates from the path
        path = path.Distinct().ToList();

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

        InvokeRepeating("SpawnEnemy", 5, 5);
    }

    // Update is called once per frame
    void Update()
    {
    }

    // Spawn an enemy at the start position
    void SpawnEnemy()
    {
        GameObject enemy = enemies[Random.Range(0, enemies.Length)];
        InstantiateChild(enemy, new Vector3(start.x, 0, start.y) + enemy.transform.position);
    }

    // A* pathfinding from start to end position
    private bool FindPath(Vector2Int start, Vector2Int end)
    {
        List<Node> open = new List<Node>();
        List<Node> closed = new List<Node>();

        Node node = new Node(start.x, start.y);
        node.SetDistance(end);

        open.Add(node);
        // Debug.Log(node + " " + start + " " + end);

        while (open.Any())
        {
            Node curr = open.OrderBy(x => x.total).First();
            // Debug.Log(curr);
            if (curr.x == end.x && curr.y == end.y)
            {
                // Set the map tiles and create the enemy path
                int count = path.Count;
                while (curr != null)
                {
                    map[curr.x, curr.y] = 1;
                    path.Insert(count, new Vector2Int(curr.x, curr.y));
                    curr = curr.parent;
                }

                return true;
            }

            closed.Add(curr);
            open.Remove(curr);

            foreach (Vector2Int pos in Shuffle(GetNeighbors(curr.x, curr.y)))
            {
                // Prevent traversing an already existing path
                if (map[pos.x, pos.y] == 1)
                    continue;

                // The neighbors to the current neighbor, prevent paths from being adjacent to one another
                var adjacent = GetNeighbors(pos.x, pos.y);
                if (adjacent.Any(e => e.x != curr.x && e.y != curr.y && map[e.x, e.y] == 1))
                    continue;

                Node neighbor = new Node(pos.x, pos.y);
                neighbor.parent = curr;
                neighbor.SetDistance(end);

                if (closed.Any(x => x.x == neighbor.x && x.y == neighbor.y))
                    continue;

                if (open.Any(x => x.x == neighbor.x && x.y == neighbor.y))
                {
                    var existing = open.First(x => x.x == neighbor.x && x.y == neighbor.y);
                    if (existing.total > curr.total)
                    {
                        open.Remove(existing);
                        open.Add(neighbor);
                    }
                }
                else open.Add(neighbor);
            }
        }

        return false;
    }

    // Node for a* pathfinding
    private class Node
    {
        public int x { get; set; }
        public int y { get; set; }
        public int cost { get; set; }
        public int distance { get; set; }
        public int total => cost + distance;
        public Node parent { get; set; }
        public Node(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public void SetDistance(Vector2Int target)
        {
            this.distance = Mathf.Abs(target.x - x) + Mathf.Abs(target.y - y);
        }

        public override string ToString()
        {
            return (new Vector2Int(x, y)).ToString();
        }
    }
}
