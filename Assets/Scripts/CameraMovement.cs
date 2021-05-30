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
    private readonly float CamDragSpeed = 0.03f; // Скорость движения камеры (перетаскивание)
    private Vector3 CursorPos; // Буфер координат курсора
    private float CamStartPos_X; // Начальная координата камеры по X
    private float CamStartPos_Z; // Начальная координата камеры по Z
    /// <summary>
    /// Ограничение движения камеры по оси X
    /// </summary>
    /// <returns></returns>
    private float CamClamp_X()
    {
        return Mathf.Clamp(CamTrans.position.x, CamStartPos_X, 10 * (GetGrid_Hor() - 1));
    }
    /// <summary>
    /// Ограничение движения камеры по оси Z
    /// </summary>
    /// <returns></returns>
    private float CamClamp_Z()
    {
        return Mathf.Clamp(CamTrans.position.z, CamStartPos_Z, CamStartPos_Z + 10 * (GetGrid_Ver() - 2));
    }

    #endregion

    void Start()
    {
        // Упрощение названия трансформа держателя камеры
        CamTrans = CameraHolder.transform;
        // Запись стартовой позиции камеры
        CamStartPos_X = CamTrans.position.x;
        CamStartPos_Z = CamTrans.position.z;
        // Запись начального значения в буфер координат курсора
        CursorPos = Input.mousePosition;
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
                    (CursorPos.x - Input.mousePosition.x) * CamDragSpeed,
                    0,
                    (CursorPos.y - Input.mousePosition.y) * CamDragSpeed
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
            else if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                // Проверка наличия ввода по горизонтальной оси и того что ширина поля больше 1
                if (Input.GetAxis("Horizontal") != 0 && GetGrid_Hor() > 1)
                {
                    // Перемещение камеры по горизонтальной оси
                    CamTrans.Translate(new Vector3
                        (
                        Input.GetAxis("Horizontal") * CamSpeed * Time.fixedDeltaTime,
                        0,
                        0
                        ));
                    // Установка границ движения камеры по горизонтали в зависимости от размеров поля
                    CamTrans.position = new Vector3
                        (
                        CamClamp_X(),
                        CamTrans.position.y,
                        CamTrans.position.z
                        );
                }
                // Проверка наличия ввода по вертикальной оси и того что длинна поля больше 1
                if (Input.GetAxis("Vertical") != 0 && GetGrid_Ver() > 1)
                {
                    // Перемещение камеры по горизонтальной оси
                    CamTrans.Translate(new Vector3
                        (
                        0,
                        0,
                        Input.GetAxis("Vertical") * CamSpeed * Time.fixedDeltaTime
                        ));
                    // Установка границ движения камеры по вертикали в зависимости от размеров поля
                    CamTrans.position = new Vector3
                        (
                        CamTrans.position.x,
                        CamTrans.position.y,
                        CamClamp_Z()
                        );
                }
            }

            #endregion

            #region Управление курсором

            else if (false)
            {

            }

            #endregion

        }

        #endregion

    }
}