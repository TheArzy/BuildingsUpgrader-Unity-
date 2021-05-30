using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WorldBuilderScript;

public class CameraMovement : MonoBehaviour
{

    #region ��������� ����������

    public GameObject CameraHolder; // ������-��������� ��� ������
    private Transform CamTrans; // ��������� ���������� ��������� ������

    private DateTime dTime = DateTime.Now; // ���������� �������
    public byte CamSpeed = 60; // �������� �������� ������ (�������)
    private readonly float CamDragSpeed = 0.03f; // �������� �������� ������ (��������������)
    private Vector3 CursorPos; // ����� ��������� �������
    private float CamStartPos_X; // ��������� ���������� ������ �� X
    private float CamStartPos_Z; // ��������� ���������� ������ �� Z
    /// <summary>
    /// ����������� �������� ������ �� ��� X
    /// </summary>
    /// <returns></returns>
    private float CamClamp_X()
    {
        return Mathf.Clamp(CamTrans.position.x, CamStartPos_X, 10 * (GetGrid_Hor() - 1));
    }
    /// <summary>
    /// ����������� �������� ������ �� ��� Z
    /// </summary>
    /// <returns></returns>
    private float CamClamp_Z()
    {
        return Mathf.Clamp(CamTrans.position.z, CamStartPos_Z, CamStartPos_Z + 10 * (GetGrid_Ver() - 2));
    }

    #endregion

    void Start()
    {
        // ��������� �������� ���������� ��������� ������
        CamTrans = CameraHolder.transform;
        // ������ ��������� ������� ������
        CamStartPos_X = CamTrans.position.x;
        CamStartPos_Z = CamTrans.position.z;
        // ������ ���������� �������� � ����� ��������� �������
        CursorPos = Input.mousePosition;
    }

    void FixedUpdate()
    {

        #region ������������ ������

        // �������� ������� �� ������ ������� ����
        if (GameObject.Find("MainMenu") == null)
        {
            #region ���������� ��������������� ����

            // �������� ������� ������ ������ ����
            if (Input.GetButton("Fire2"))
            {
                // �������� �������, ����������� � ���������� ������ ���� ����� ����
                // (����� ��� �������������� ������� �������� ������� ������ � ����� ���������� ������� ���)
                if (DateTime.Now.Subtract(dTime).TotalMilliseconds > 40)
                {
                    // ���������� ������� �������
                    CursorPos = Input.mousePosition;
                }
                // ����������� ������ � ������������ � ������������� ������� � ������� ���
                // ���������� ������� � ����������� ������� ������� ������� � ����������� ��� ������� ������ ���� ����� ����
                CamTrans.Translate
                    (
                    (CursorPos.x - Input.mousePosition.x) * CamDragSpeed,
                    0,
                    (CursorPos.y - Input.mousePosition.y) * CamDragSpeed
                    );
                // ��������� ������ �������� ������
                CamTrans.position = new Vector3
                        (
                        CamClamp_X(),
                        CamTrans.position.y,
                        CamClamp_Z()
                        );
                // ������ ������� ������� �������
                CursorPos = Input.mousePosition;
                // ������ �������� �������
                dTime = DateTime.Now;
            }

            #endregion

            #region ���������� ���������

            // �������� ������� ����� � ������ ����������
            else if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                // �������� ������� ����� �� �������������� ��� � ���� ��� ������ ���� ������ 1
                if (Input.GetAxis("Horizontal") != 0 && GetGrid_Hor() > 1)
                {
                    // ����������� ������ �� �������������� ���
                    CamTrans.Translate(new Vector3
                        (
                        Input.GetAxis("Horizontal") * CamSpeed * Time.fixedDeltaTime,
                        0,
                        0
                        ));
                    // ��������� ������ �������� ������ �� ����������� � ����������� �� �������� ����
                    CamTrans.position = new Vector3
                        (
                        CamClamp_X(),
                        CamTrans.position.y,
                        CamTrans.position.z
                        );
                }
                // �������� ������� ����� �� ������������ ��� � ���� ��� ������ ���� ������ 1
                if (Input.GetAxis("Vertical") != 0 && GetGrid_Ver() > 1)
                {
                    // ����������� ������ �� �������������� ���
                    CamTrans.Translate(new Vector3
                        (
                        0,
                        0,
                        Input.GetAxis("Vertical") * CamSpeed * Time.fixedDeltaTime
                        ));
                    // ��������� ������ �������� ������ �� ��������� � ����������� �� �������� ����
                    CamTrans.position = new Vector3
                        (
                        CamTrans.position.x,
                        CamTrans.position.y,
                        CamClamp_Z()
                        );
                }
            }

            #endregion

            #region ���������� ��������

            else if (false)
            {

            }

            #endregion

        }

        #endregion

    }
}