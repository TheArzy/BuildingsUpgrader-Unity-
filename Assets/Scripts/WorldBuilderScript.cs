using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static ButtonActionsScript;

public class WorldBuilderScript : MonoBehaviour
{

    #region ��������� ����������

    public GameObject CameraHolder; // ������-��������� ��� ������
    public GameObject Cell; // ������, �� ������� �������� ������� ����
    public GameObject menu; // ���� �����
    public static GameObject objectBuffer; // ���������� ����� ��� ���������� �������

    private byte gridVer; // ������ �������� ���� �� ���������
    private byte gridHor; // ������ �������� ���� �� �����������

    private Transform CamTrans; // ��������� ���������� ��������� ������
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
        // ��������� �������� ���������� ��������� ������
        CamTrans = CameraHolder.transform;
    }

    private void Update()
    {
        // �������� ������� ������� Escape � �������� ���� �����
        if (GameObject.FindGameObjectWithTag("Menu") == null && Input.GetKeyDown(KeyCode.Escape))
        {
            Instantiate(menu,
            Canvas.position,
            Canvas.rotation,
            parent: Canvas);
        }

        #region ������������ ������

        // �������� ������� �� ������ ������� ����
        if (GameObject.Find("MainMenu") == null)
        {
            // �������� ������� ����� �� �������������� ��� � ���� ��� ������ ���� ������ 1
            if (Input.GetAxis("Horizontal") != 0 && gridHor > 1)
            {
                // ����������� ������ �� �������������� ���
                CamTrans.Translate(new Vector3(Input.GetAxis("Horizontal") * 0.3f, 0 , 0));
                // ����������� �������� ������ �� ����������� � ����������� �� �������� ����
                CamTrans.position = new Vector3
                    (
                    Mathf.Clamp(CamTrans.position.x, 0, 10 * (gridHor - 1)),
                    CamTrans.position.y,
                    CamTrans.position.z
                    );
                    
            }
            // �������� ������� ����� �� ������������ ��� � ���� ��� ������ ���� ������ 1
            if (Input.GetAxis("Vertical") != 0 && gridVer > 1)
            {
                // ����������� ������ �� �������������� ���
                CamTrans.Translate(new Vector3(0, 0, 0.3f * Input.GetAxis("Vertical")));
                // ����������� �������� ������ �� ��������� � ����������� �� �������� ����
                CamTrans.position = new Vector3
                    (
                    CamTrans.position.x,
                    CamTrans.position.y,
                    Mathf.Clamp(CamTrans.position.z, 90.4f, 90.4f + 10 * (gridVer - 2))
                    );
            }
        }

        #endregion

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
        gridHor = (byte)GameObject.Find("HorSlider").GetComponent<Slider>().value;
        gridVer = (byte)GameObject.Find("VerSlider").GetComponent<Slider>().value;
        // ��������� ������ �� �� ��� ��������� � ������ ���� (��� ������ ����� 1�1)
        if (start == false && gridHor > 1 || gridVer > 1)
        {
            // ����������� ������ ��������� �������� (� �������� ����� ��� ���������� rotation)
            CellTransBuffer = gameObject.transform;
            // ��������� ������� ����
            CloseMenu();

            for (byte count_1 = 1; count_1 <= gridVer; count_1++)
            {
                // ������ ����������� ��������� �� ������ �������� (�����)
                if (count_1 % 2 == 0) rlPoint = "LeftPoint";

                for (byte count_2 = 1; count_2 <= gridHor; count_2++)
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
