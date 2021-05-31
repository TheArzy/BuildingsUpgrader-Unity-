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
    /// 
    /// </summary>
    public static void CloseTextRedactor()
    {
        Destroy(GameObject.Find("InputText(Clone)"));
    }

    /// <summary>
    /// 
    /// </summary>
    public void SubTextUpdate()
    {
        Transform parent = GameObject.Find("MainCanvas").transform;
        Instantiate(GameObject.Find("WorldBuilder").GetComponent<WorldBuilderScript>().RedactMenu,
            parent.position,
            parent.rotation,
            parent: parent
            );
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
        TextUpdate("Building");
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

    /// <summary>
    /// 
    /// </summary>
    public void DeleteBuilding()
    {
        // ��������� ���������� ������
        objectBuffer.GetComponent<BuildingScript>().myCell.GetComponent<Collider>().enabled = true;
        // ����������� ���������� ��������
        Destroy(objectBuffer);
        // �������� ����
        CloseMenu();
    }
}