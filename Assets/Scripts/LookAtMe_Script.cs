using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMe_Script : MonoBehaviour
{
    void Update()
    {
        // ���������� �������� � ������� �������� ���������� �������� �� ����
        gameObject.transform.LookAt(GameObject.Find("CameraHolder").transform);
    }
}
