using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static WorldBuilderScript;

public class BuildingScript : MonoBehaviour, IPointerClickHandler
{
    public GameObject menu; // ����, ���������� ��� ������� �� ��������� �������
    public byte buildinglevel = 1; // ������� ������� ��������
    private string type = "None"; // ��� ������� ��������

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
        // ���������� ������ � ���� � ������������ � ����� ��������
        Textupdate(type);
    }

    /// <summary>
    /// �������� ��������
    /// </summary>
    /// <param name="value">����� �������� � ����������� ������</param>
    public static void CreateBuilding(int value)
    {
        // ����� ��� ���������� ���� ��������
        GameObject buffer;
        // ����� ��� ������� ��������� ��������
        GameObject[] comp = GameObject.Find("WorldBuilder").GetComponent<WorldBuilderScript>().buildings;
        // ����� �������� ��� �������� � ������������ � ��������� ����������� ������
        switch (value)
        {
            case 0:
                // ���������� ��������� ��������
                buffer = Instantiate(comp[0], objectBuffer.transform.position, comp[0].transform.rotation);
                // ����������� ���������� �������� ��� ���
                buffer.GetComponent<BuildingScript>().type = "Tree";
                break;
            case 1:
                // ���������� ��������� ��������
                buffer = Instantiate(comp[1], objectBuffer.transform.position, comp[1].transform.rotation);
                // ����������� ���������� �������� ��� ���
                buffer.GetComponent<BuildingScript>().type = "House";
                break;
        }
        // ��������� ��������� ������ ����� ������������� ��������� �������� ���� �������� ��������
        objectBuffer.GetComponent<Collider>().enabled = false;
    }
}