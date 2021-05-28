using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static WorldBuilderScript;

public class ButtonActionsScript : MonoBehaviour
{
    public GameObject[] buildings; // ������ �������� ��������� ��� ��������

    /// <summary>
    /// ����� �� ���������
    /// </summary>
    public void CloseGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// �������� ��������� ����
    /// </summary>
    public static void CloseMenu()
    {
        Destroy(GameObject.FindGameObjectWithTag("Menu"));
    }

    /// <summary>
    /// ��������� ������
    /// </summary>
    public void Upgrade()
    {
        // ��������� ����� �� ��� ��������� ������� ������
        if (objectBuffer.GetComponent<BuildingScript>().buildinglevel < 5)
        {
            // ����������� ������� ������
            objectBuffer.GetComponent<BuildingScript>().buildinglevel++;
        }
        // ��������� ����� � ���� � �� �������
        Textupdate("House");
    }

    #region ��������� ������

    /// <summary>
    /// ������������� ����� ��� �������� � ������
    /// </summary>
    public void SubCreate()
    {
        // ����� ������ �������� �������� � ������� ������� �� ����������� ������
        CreateBuilding(GameObject.Find("Dropdown").GetComponent<Dropdown>().value);
        // �������� ����
        CloseMenu();
    }

    /// <summary>
    /// �������� ��������
    /// </summary>
    /// <param name="value">����� �������� � ����������� ������</param>
    public void CreateBuilding(int value)
    {
        // ����� ��� ���������� ���� ��������
        GameObject buffer;
        // ����� �������� ��� �������� � ������������ � ��������� ����������� ������
        switch (value)
        {
            case 0:
                // ���������� ��������� ��������
                buffer = Instantiate(buildings[1], objectBuffer.transform.position, buildings[1].transform.rotation);
                // ����������� ���������� �������� ��� ���
                buffer.GetComponent<BuildingScript>().type = "House";
                break;
            case 1:
                // ���������� ��������� ��������
                buffer = Instantiate(buildings[0], objectBuffer.transform.position, buildings[0].transform.rotation);
                // ����������� ���������� �������� ��� ���
                buffer.GetComponent<BuildingScript>().type = "Tree";
                break;
        }
        // ��������� ��������� ������ ����� ������������� ��������� �������� ���� �������� ��������
        objectBuffer.GetComponent<Collider>().enabled = false;
    }

    #endregion

}
