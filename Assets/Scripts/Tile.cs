using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour
{
    public Color hoverColor;
    private Color startColor;
    private Renderer rend;

    private GameObject weapon;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        // The game object has more than one material, get the surface material
        startColor = rend.materials[1].color;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (weapon != null)
            return;

        var building = BuildManager.instance.GetBuilding();
        var cost = building.GetComponent<Weapon>().cost;

        if (Player.instance.Buy(cost))
            weapon = Instantiate(building, transform.position + building.transform.position, transform.rotation);
    }
    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (weapon != null)
            return;

        rend.materials[1].color = hoverColor;
    }

    void OnMouseExit()
    {
        rend.materials[1].color = startColor;
    }
}
