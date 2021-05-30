using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static WorldBuilderScript;

public class ButtonActionsScript : MonoBehaviour
{

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

    /// <summary>
    /// ������������� ����� ��� �������� � ������
    /// </summary>
    public void SubCreate()
    {
        // ����� ������ �������� �������� � ������� ������� �� ����������� ������
        BuildingScript.CreateBuilding(GameObject.Find("Dropdown").GetComponent<Dropdown>().value);
        // �������� ����
        CloseMenu();
    }
}