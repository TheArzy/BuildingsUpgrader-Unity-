using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WorldBuilderScript;

public class CameraMovement : MonoBehaviour
{

    #region Начальные переменные

    public GameObject CameraHolder; // Объект-контейнер для камеры
    private Transform CamTrans; // Хранилище трансформа держателя камеры

    private DateTime dTime = DateTime.Now; // Переменная времени
    public byte CamSpeed = 60; // Скорость движения камеры (клавиши)
    public byte CamWheelSpeed = 7; // Скорость зума камеры (колесико)
    public float CamDragSpeed = 1.2f; // Скорость движения камеры (перетаскивание)
    private sbyte CamRot; // Параметр поворота камеры
    private Vector3 CursorPos; // Буфер координат курсора
    private float CamStartPos_X; // Начальная координата камеры по X
    private float CamStartPos_Y; // Начальная координата камеры по Y
    private float CamStartPos_Z; // Начальная координата камеры по Z
    /// <summary>
    /// Ограничение движения камеры по оси X
    /// </summary>
    /// <returns></returns>
    private float CamClamp_X()
    {
        return Mathf.Clamp(CamTrans.position.x, CamStartPos_X, CamStartPos_X + 10 * (GetGrid_Hor() + 1));
    }
    /// <summary>
    /// Ограничение движения камеры по оси Z
    /// </summary>
    /// <returns></returns>
    private float CamClamp_Z()
    {
        return Mathf.Clamp(CamTrans.position.z, CamStartPos_Z, CamStartPos_Z + 10 * (GetGrid_Ver() + 1));
    }

    #endregion

    void Start()
    {
        // Упрощение названия трансформа держателя камеры
        CamTrans = CameraHolder.transform;
        // Запись стартовой позиции камеры
        CamStartPos_X = CamTrans.position.x - 10;
        CamStartPos_Y = CamTrans.position.y - 10;
        CamStartPos_Z = CamTrans.position.z - 10;
        // Запись начального значения в буфер координат курсора
        CursorPos = Input.mousePosition;
    }

    private void Update()
    {
        // Реализовано не через FixedUpdate из-за чрезмерной нестабильности последнего с данным методом
        #region Zoom камеры колесиком мыши
        
        // Проверка ввода с колесика мыши
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            // Перемещаем камеру по вертикали в соответствии с вводом с колесика
            CamTrans.Translate(0, (-1) * Input.GetAxis("Mouse ScrollWheel") * CamWheelSpeed, 0);
            // Ограничиваем движение камеры по вертикали
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

        #region Передвижение камеры

        // Проверка открыто ли сейчас главное меню
        if (GameObject.Find("MainMenu") == null)
        {

            #region Управление перетаскиванием поля

            // Проверка нажатия правой кнопки мыши
            if (Input.GetButton("Fire2"))
            {
                // Проверка времени, пройденного с последнего вызова этой части кода
                // (Нужно для предотвращения резкого возврата позиции камеры к точке последнего нажатия ПКМ)
                if (DateTime.Now.Subtract(dTime).TotalMilliseconds > 40)
                {
                    // Обновление позиции курсора
                    CursorPos = Input.mousePosition;
                }
                // Перемещение камеры в соответствии с передвижением курсора с зажатой ПКМ
                // Использует разницу в координатах курсора данного момента и координатах при прошлом вызове этой части кода
                CamTrans.Translate
                    (
                    (CursorPos.x - Input.mousePosition.x) * CamDragSpeed * Time.fixedDeltaTime,
                    0,
                    (CursorPos.y - Input.mousePosition.y) * CamDragSpeed * Time.fixedDeltaTime
                    );
                // Установка границ движения камеры
                CamTrans.position = new Vector3
                        (
                        CamClamp_X(),
                        CamTrans.position.y,
                        CamClamp_Z()
                        );
                // Запись текущей позиции курсора
                CursorPos = Input.mousePosition;
                // Запись текущего времени
                dTime = DateTime.Now;
            }

            #endregion

            #region Управление клавишами

            // Проверка наличия ввода с клавиш управления
            else if (GameObject.Find("InputText(Clone)") == null && ( 
                Input.GetAxis("Horizontal") != 0 || 
                Input.GetAxis("Vertical") != 0 ||
                Input.GetKey(KeyCode.Q) || 
                Input.GetKey(KeyCode.E)
                ))
            {
                // Проверка наличия ввода по горизонтальной оси и того что ширина поля больше 1
                if (Input.GetAxis("Horizontal") != 0 && GetGrid_Hor() > 1)
                {
                    // Перемещение камеры по горизонтальной оси
                    // Косинус нужен для возможности управления камерой вне зависимости от ее поворота
                    CamTrans.Translate(new Vector3
                        (
                        Input.GetAxis("Horizontal") * Mathf.Cos(CamTrans.rotation.y) * CamSpeed * Time.fixedDeltaTime,
                        0,
                        0
                        ));
                }
                // Проверка наличия ввода по вертикальной оси и того что длинна поля больше 1
                if (Input.GetAxis("Vertical") != 0 && GetGrid_Ver() > 1)
                {
                    // Перемещение камеры по горизонтальной оси
                    // Косинус нужен для возможности управления камерой вне зависимости от ее поворота
                    CamTrans.Translate(new Vector3
                        (
                        0,
                        0,
                        Input.GetAxis("Vertical") * Mathf.Cos(CamTrans.rotation.y) * CamSpeed * Time.fixedDeltaTime
                        ));
                }
                // Проверка нажатия клавиш Q или E для поворота камеры
                if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E))
                {
                    // Приведение значений Q и E к одной переменной (на подобии GetAxis)
                    if (Input.GetKey(KeyCode.Q)) CamRot = -1;
                    else CamRot = 1;
                    // Поворачиваем камеру
                    CamTrans.Rotate(new Vector3(0, CamRot * 1.5f , 0));
                }

                // Установка границ движения камеры в зависимости от размеров поля
                CamTrans.position = new Vector3
                        (
                        CamClamp_X(),
                        CamTrans.position.y,
                        CamClamp_Z()
                        );
            }

            #endregion

            #region Управление курсором (Not implemented)

            else if (false)
            {
                // Здесь пока пусто
            }

            #endregion

        }

        #endregion

    }
}