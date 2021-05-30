using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static ButtonActionsScript;

public class WorldBuilderScript : MonoBehaviour
{

    #region ��������� ����������

    public GameObject[] buildings; // ������ �������� ��������� ��� ��������
    public GameObject Cell; // ������, �� ������� �������� ������� ����
    public GameObject menu; // ���� �����
    public static GameObject objectBuffer; // ���������� ����� ��� ���������� �������

    private static byte grid_Ver; // ������ �������� ���� �� ���������
    public static byte GetGrid_Ver()
    {
        return grid_Ver;
    } // ������ � ������ gridVer 
    private static byte grid_Hor; // ������ �������� ���� �� �����������
    public static byte GetGrid_Hor()
    {
        return grid_Hor;
    } // ������ � ������ gridHor 

    private Transform Canvas; // ��������� ��� ���������� ������� �������� ����������
    private Transform CellTransBuffer; // ����� ��� ���������� ��������� ��������� ������

    private bool start = false; // ���� ����������� ��� ��������� ���� ���� ���������
    private string rlPoint = "RightPoint"; // �������� �����-��������� ��� ��������� ����������� ������
    private Vector3 NextCellPos = new Vector3(0, 5, 100); // ���������� �������� ��������� ������

    #endregion

    private void Start()
    {
        // ��������� �������� ���������� ������� �������� ����������
        Canvas = GameObject.Find("MainCanvas").transform;
    }

    private void Update()
    {
        // �������� ������� ������� Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // �������� ���� �����, ���� �� ������� ������ ����
            if (GameObject.FindGameObjectWithTag("Menu") == null)
            {
                Instantiate(menu,
                Canvas.position,
                Canvas.rotation,
                parent: Canvas);
            }
            // �������� ������ ����, ���� ����� ����
            else if (GameObject.FindGameObjectWithTag("Menu") != null)
            {
                CloseMenu();
            }
        }
    }

    /// <summary>
    /// ���������� �������� � ����� � ������
    /// </summary>
    /// <param name="type">��������� ��� �������</param>
    public static void Textupdate(string type)
    {
        // ����� ������ ���������� ������ � ������������ � ����� ��������
        switch (type)
        {
            case "Tree":

                // �������� ������ ������ � ����
                GameObject.Find("BuildingLevel").GetComponent<Text>().text = 
                    "";
                // �������� ������ ���������
                Destroy(GameObject.Find("UpgradeButton"));
                // ����� ���������������� ��������
                GameObject.Find("Description").GetComponent<Text>().text = 
                    "����� ������� ������\n" +
                    "(���������)";

                break;
            case "House":

                // ���������� ������ ������ �������� � ����
                GameObject.Find("BuildingLevel").GetComponent<Text>().text = 
                    $"������� {objectBuffer.GetComponent<BuildingScript>().buildinglevel}";
                // ���������� ���������� ������ ��� ��������� ���������
                objectBuffer.transform.Find("Sphere").Find("Canvas").Find("BuLe").GetComponent<Text>().text = 
                    $"{objectBuffer.GetComponent<BuildingScript>().buildinglevel}";
                // ����� ���������������� ��������
                GameObject.Find("Description").GetComponent<Text>().text = 
                    "������� ����� ���\n" +
                    "(����� ���� �������)";

                break;
        }
    }

    /// <summary>
    /// ��������� �������� ����
    /// </summary>
    public void GenerateTerrain()
    {
        // ��������� �������� �� ��������� � ������� ����
        grid_Hor = (byte)GameObject.Find("HorSlider").GetComponent<Slider>().value;
        grid_Ver = (byte)GameObject.Find("VerSlider").GetComponent<Slider>().value;
        // ��������� ������ �� �� ��� ��������� � ������ ���� (��� ������ ����� 1�1)
        if (start == false && grid_Hor > 1 || grid_Ver > 1)
        {
            // ����������� ������ ��������� �������� (� �������� ����� ��� ���������� rotation)
            CellTransBuffer = gameObject.transform;
            // ��������� ������� ����
            CloseMenu();

            for (byte count_1 = 1; count_1 <= grid_Ver; count_1++)
            {
                // ������ ����������� ��������� �� ������ �������� (�����)
                if (count_1 % 2 == 0) rlPoint = "LeftPoint";

                for (byte count_2 = 1; count_2 <= grid_Hor; count_2++)
                {
                    // ������� ������ � ������ ��� ������� ������������ ��������� (���� �� ��������� �����������)
                    // � ���������� ��������� ��������� ������ � �����
                    CellTransBuffer = Instantiate(Cell, NextCellPos, CellTransBuffer.rotation).transform;
                    // ���������� ���������� ������ ��� ����� ��� ��������� ������
                    NextCellPos = CellTransBuffer.Find(rlPoint).transform.position;
                }
                // ������ ����� ��������� ��������� ������ �� ��������� �������
                NextCellPos = CellTransBuffer.Find("FrontPoint").transform.position;
                // ������ ����������� ��������� (������)
                rlPoint = "RightPoint";
            }
            // ������ ������ ��� �� ������� ���������
            start = true;
        }
        // ���������� ��� ��������� ��� ����� ������
        else if (start == false)
        {
            Instantiate(Cell, NextCellPos, Quaternion.identity);
            // ��������� ������� ����
            CloseMenu();
        }
    }
}