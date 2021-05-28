using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static WorldBuilderScript;

public class ButtonActionsScript : MonoBehaviour
{
    public GameObject[] buildings;

    public void ButtonStart()
    {
        gridHor = (byte)GameObject.Find("HorSlider").GetComponent<Slider>().value;
        gridVer = (byte)GameObject.Find("VerSlider").GetComponent<Slider>().value;

        GameObject.Find("WorldBuilder").GetComponent<WorldBuilderScript>().GenerateTerrain();

        CloseMenu();
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public void CloseMenu()
    {
        Destroy(GameObject.FindGameObjectWithTag("Menu"));
    }

    public void SubCreate()
    {
        CreateBuilding(GameObject.Find("Dropdown").GetComponent<Dropdown>().value);
        CloseMenu();
    }

    public void CreateBuilding(int value)
    {
        GameObject buffer;
        switch (value)
        {
            case 0:
                buffer = Instantiate(buildings[1], objectBuffer.transform.position, buildings[1].transform.rotation);
                buffer.GetComponent<BuildingScript>().type = "House";
                break;
            case 1:
                buffer = Instantiate(buildings[0], objectBuffer.transform.position, buildings[0].transform.rotation);
                buffer.GetComponent<BuildingScript>().type = "Tree";
                break;
        }
        objectBuffer.GetComponent<Collider>().enabled = false;
    }

    public void Upgrade()
    {
        byte blevel = objectBuffer.GetComponent<BuildingScript>().buildinglevel;
        if (blevel < 5) objectBuffer.GetComponent<BuildingScript>().buildinglevel++;

        Textupdate(objectBuffer, "House");
    }

}
