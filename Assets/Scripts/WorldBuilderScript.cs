using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldBuilderScript : MonoBehaviour
{
    public static GameObject objectBuffer;
    public GameObject CameraHolder;
    public GameObject Cell;
    public GameObject menu;
    public static byte gridVer;
    public static byte gridHor;
    private GameObject CellBuffer;
    private bool start = false;
    private string rlPoint = "RightPoint";
    private Vector3 CellPosition = new Vector3(0, 5, 100);

    private void Update()
    {
        if (GameObject.FindGameObjectWithTag("Menu") == null && Input.GetKeyDown(KeyCode.Escape))
        {
            Instantiate(menu,
            GameObject.Find("MainCanvas").transform.position,
            GameObject.Find("MainCanvas").transform.rotation,
            parent: GameObject.Find("MainCanvas").transform);
        }

        if (GameObject.Find("MainMenu") == null)
        {
            if (Input.GetAxis("Horizontal") != 0 && gridHor > 1)
            {
                CameraHolder.transform.Translate(new Vector3(Input.GetAxis("Horizontal") * 0.3f, 0 , 0));
                CameraHolder.transform.position = new Vector3
                    (
                    Mathf.Clamp(CameraHolder.transform.position.x, 0, 10 * (gridHor - 1)),
                    CameraHolder.transform.position.y,
                    CameraHolder.transform.position.z
                    );
                    
            }
            if (Input.GetAxis("Vertical") != 0 && gridVer > 1)
            {
                CameraHolder.transform.Translate(new Vector3(0, 0, 0.3f * Input.GetAxis("Vertical")));
                CameraHolder.transform.position = new Vector3
                    (
                    CameraHolder.transform.position.x,
                    CameraHolder.transform.position.y,
                    Mathf.Clamp(CameraHolder.transform.position.z, 90.4f, 90.4f + 10 * (gridVer - 2))
                    );
            }
        }
    }

    public static void Textupdate(GameObject gaob, string type)
    {
        Text text;
        switch (type)
        {
            case "Tree":
                text = GameObject.Find("BuildingLevel").GetComponent<Text>();
                text.text = "";
                text = GameObject.Find("Description").GetComponent<Text>();
                text.text = "Самое обычное дерево\n" +
                    "(Декорация)";
                Destroy(GameObject.Find("UpgradeButton"));
                break;
            case "House":
                text = GameObject.Find("BuildingLevel").GetComponent<Text>();
                text.text = $"Уровень {gaob.GetComponent<BuildingScript>().buildinglevel}";
                text = gaob.transform.Find("Sphere").Find("Canvas").Find("BuLe").GetComponent<Text>();
                text.text = $"{gaob.GetComponent<BuildingScript>().buildinglevel}";
                text = GameObject.Find("Description").GetComponent<Text>();
                text.text = "Обычный жилой дом\n" +
                    "(Может быть улучшен)";
                break;
                
        }
    }

    public void GenerateTerrain()
    {
        if (start == false && gridHor > 1 || gridVer > 1)
        {
            CellBuffer = gameObject;
            Debug.Log("Instance");
            for (byte count_1 = 1; count_1 <= gridVer; count_1++)
            {
                if (count_1 % 2 == 0) rlPoint = "LeftPoint";
                for (byte count_2 = 1; count_2 <= gridHor; count_2++)
                {
                    CellBuffer = Instantiate(Cell, CellPosition, CellBuffer.transform.rotation);
                    CellPosition = CellBuffer.transform.Find(rlPoint).transform.position;
                }
                CellPosition = CellBuffer.transform.Find("FrontPoint").transform.position;
                rlPoint = "RightPoint";
            }

            start = true;
        }
        else if (start == false) Instantiate(Cell, CellPosition, Quaternion.identity);
    }
}
