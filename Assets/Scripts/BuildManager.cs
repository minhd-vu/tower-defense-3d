using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    public List<GameObject> buildings;
    private int buildingIndex = 0;

    public GameObject GetBuilding()
    {
        return buildings[buildingIndex];
    }

    public void SelectBuilding(int index)
    {
        Debug.Log("Selected Buidling");
        buildingIndex = index;
    }

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
}
