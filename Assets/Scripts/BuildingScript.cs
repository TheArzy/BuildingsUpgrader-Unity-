using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static WorldBuilderScript;

public class BuildingScript : MonoBehaviour, IPointerClickHandler
{
    public string Name = "Name_None"; // �������� ������
    public string Type = "Type_None"; // ��� ������� ��������
    public string Category = "Cat_None"; // ��������� ������� ��������
    public string Description = "Descr_None"; // �������� ��������

    public GameObject myCell; // ��������� ������ � ������, �� ������� ����� ��������
    public GameObject menu; // ����, ���������� ��� ������� �� ��������� �������
    public byte buildinglevel = 1; // ������� ������� ��������

    /// <summary>
    /// �������� ���������������� ���� �������\��������� ��� ������� �� ��������� ��������
    /// ����� ������ �� ������� ������ �������� ����
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        // �������� ���������� ���� � ������� �������
        Instantiate(menu,
            GameObject.Find("MainCanvas").transform.position,
            GameObject.Find("MainCanvas").transform.rotation,
            parent: GameObject.Find("MainCanvas").transform);
        // ������ ���������� �������� � ����� 
        objectBuffer = gameObject;
        // ���������� ������ � ���� � ������������ � ��������� ���������
        TextUpdate(Category);
    }

    /// <summary>
    /// �������� ���������� �������� � ���������� �����������
    /// </summary>
    /// <param name="index">����� �������� � ������</param>
    /// <param name="name">���</param>
    /// <param name="type">���</param>
    /// <param name="category">���������</param>
    /// <param name="description">��������</param>
    public static void BuildInst(
        byte index, string name, string type, 
        string category, string description)
    {
        // ����� ��� ���������� ���� ��������
        GameObject buffer;
        // ����� ��� ������� ��������� ��������
        GameObject[] comp = GameObject.Find("WorldBuilder").GetComponent<WorldBuilderScript>().buildings;

        // ������� � ���������� ��������
        buffer = Instantiate(comp[index], objectBuffer.transform.position, comp[index].transform.rotation);

        // ����������� ���������� �������� ��� ���
        buffer.GetComponent<BuildingScript>().Name =
            name;
        // ����������� ���������� �������� ��� ���
        buffer.GetComponent<BuildingScript>().Type =
            type;
        // ����������� ���������� �������� ��� ���������
        buffer.GetComponent<BuildingScript>().Category =
            category;
        // ����������� ���������� �������� ��������������� ��������
        buffer.GetComponent<BuildingScript>().Description =
            description;

        // ���������� �������� ������ � ������, �� ������� ��� �����
        buffer.GetComponent<BuildingScript>().myCell =
            objectBuffer;
    }

    /// <summary>
    /// �������� ��������
    /// </summary>
    /// <param name="value">����� �������� � ����������� ������</param>
    public static void CreateBuilding(int value)
    {
        switch (value)
        {
            case 0:
                BuildInst(0, "Tree", "PineTree", "Decor",
                    "����� ������� ������\n" +
                    "(���������)");
                break;

            case 1:
                BuildInst(1, "House", "House", "Building",
                    "������� ����� ���\n");
                break;

            case 2:
                BuildInst(2, "TallBuilding", "TallBuilding", "Building",
                    "������������ ���\n");
                break;
        }
        // ��������� ��������� ������ ����� ������������� ��������� �������� ���� �������� ��������
        objectBuffer.GetComponent<Collider>().enabled = false;
    }
}