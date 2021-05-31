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
    public byte CamWheelSpeed = 7; // �������� ���� ������ (��������)
    public float CamDragSpeed = 1.2f; // �������� �������� ������ (��������������)
    private sbyte CamRot; // �������� �������� ������
    private Vector3 CursorPos; // ����� ��������� �������
    private float CamStartPos_X; // ��������� ���������� ������ �� X
    private float CamStartPos_Y; // ��������� ���������� ������ �� Y
    private float CamStartPos_Z; // ��������� ���������� ������ �� Z
    /// <summary>
    /// ����������� �������� ������ �� ��� X
    /// </summary>
    /// <returns></returns>
    private float CamClamp_X()
    {
        return Mathf.Clamp(CamTrans.position.x, CamStartPos_X, CamStartPos_X + 10 * (GetGrid_Hor() + 1));
    }
    /// <summary>
    /// ����������� �������� ������ �� ��� Z
    /// </summary>
    /// <returns></returns>
    private float CamClamp_Z()
    {
        return Mathf.Clamp(CamTrans.position.z, CamStartPos_Z, CamStartPos_Z + 10 * (GetGrid_Ver() + 1));
    }

    #endregion

    void Start()
    {
        // ��������� �������� ���������� ��������� ������
        CamTrans = CameraHolder.transform;
        // ������ ��������� ������� ������
        CamStartPos_X = CamTrans.position.x - 10;
        CamStartPos_Y = CamTrans.position.y - 10;
        CamStartPos_Z = CamTrans.position.z - 10;
        // ������ ���������� �������� � ����� ��������� �������
        CursorPos = Input.mousePosition;
    }

    private void Update()
    {
        // ����������� �� ����� FixedUpdate ��-�� ���������� �������������� ���������� � ������ �������
        #region Zoom ������ ��������� ����
        
        // �������� ����� � �������� ����
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            // ���������� ������ �� ��������� � ������������ � ������ � ��������
            CamTrans.Translate(0, (-1) * Input.GetAxis("Mouse ScrollWheel") * CamWheelSpeed, 0);
            // ������������ �������� ������ �� ���������
            CamTrans.position = new Vector3
                    (
                    CamTrans.position.x,
                    Mathf.Clamp(CamTrans.position.y, CamStartPos_Y, CamStartPos_Y + 40),
                    CamTrans.position.z
                    );
        }

        #endregion

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
                    (CursorPos.x - Input.mousePosition.x) * CamDragSpeed * Time.fixedDeltaTime,
                    0,
                    (CursorPos.y - Input.mousePosition.y) * CamDragSpeed * Time.fixedDeltaTime
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
            else if (GameObject.Find("InputText(Clone)") == null && ( 
                Input.GetAxis("Horizontal") != 0 || 
                Input.GetAxis("Vertical") != 0 ||
                Input.GetKey(KeyCode.Q) || 
                Input.GetKey(KeyCode.E)
                ))
            {
                // �������� ������� ����� �� �������������� ��� � ���� ��� ������ ���� ������ 1
                if (Input.GetAxis("Horizontal") != 0 && GetGrid_Hor() > 1)
                {
                    // ����������� ������ �� �������������� ���
                    // ������� ����� ��� ����������� ���������� ������� ��� ����������� �� �� ��������
                    CamTrans.Translate(new Vector3
                        (
                        Input.GetAxis("Horizontal") * Mathf.Cos(CamTrans.rotation.y) * CamSpeed * Time.fixedDeltaTime,
                        0,
                        0
                        ));
                }
                // �������� ������� ����� �� ������������ ��� � ���� ��� ������ ���� ������ 1
                if (Input.GetAxis("Vertical") != 0 && GetGrid_Ver() > 1)
                {
                    // ����������� ������ �� �������������� ���
                    // ������� ����� ��� ����������� ���������� ������� ��� ����������� �� �� ��������
                    CamTrans.Translate(new Vector3
                        (
                        0,
                        0,
                        Input.GetAxis("Vertical") * Mathf.Cos(CamTrans.rotation.y) * CamSpeed * Time.fixedDeltaTime
                        ));
                }
                // �������� ������� ������ Q ��� E ��� �������� ������
                if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E))
                {
                    // ���������� �������� Q � E � ����� ���������� (�� ������� GetAxis)
                    if (Input.GetKey(KeyCode.Q)) CamRot = -1;
                    else CamRot = 1;
                    // ������������ ������
                    CamTrans.Rotate(new Vector3(0, CamRot * 1.5f , 0));
                }

                // ��������� ������ �������� ������ � ����������� �� �������� ����
                CamTrans.position = new Vector3
                        (
                        CamClamp_X(),
                        CamTrans.position.y,
                        CamClamp_Z()
                        );
            }

            #endregion

            #region ���������� �������� (Not implemented)

            else if (false)
            {
                // ����� ���� �����
            }

            #endregion

        }

        #endregion

    }
}