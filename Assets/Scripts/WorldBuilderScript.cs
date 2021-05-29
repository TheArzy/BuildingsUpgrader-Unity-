using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static ButtonActionsScript;

public class WorldBuilderScript : MonoBehaviour
{

    #region Начальные переменные

    public GameObject CameraHolder; // Объект-контейнер для камеры
    public GameObject Cell; // Ячейка, из которых строится игровое поле
    public GameObject menu; // Меню паузы
    public static GameObject objectBuffer; // Глобальный буфер для выбранного объекта

    private byte gridVer; // Размер игрового поля по вертикали
    private byte gridHor; // Размер игрового поля по горизонтали

    private Transform CamTrans; // Хранилище трансформа держателя камеры
    private Transform Canvas; // Хранилище для трансформа канваса игрового интерфейса
    private Transform CellTransBuffer; // Буфер для трансформа последней созданной клетки

    private bool start = false; // Флаг указывающий что генерация поля была проведена
    private string rlPoint = "RightPoint"; // Название метки-ориентира для следующей создаваемой клетки
    private Vector3 NextCellPos = new Vector3(0, 5, 100); // Координаты создания следующей клетки

    #endregion

    private void Start()
    {
        // Упрощение названия трансформа канваса игрового интерфейса
        Canvas = GameObject.Find("MainCanvas").transform;
        // Упрощение названия трансформа держателя камеры
        CamTrans = CameraHolder.transform;
    }

    private void Update()
    {
        // Проверка нажатия клавиши Escape и открытие меню паузы
        if (GameObject.FindGameObjectWithTag("Menu") == null && Input.GetKeyDown(KeyCode.Escape))
        {
            Instantiate(menu,
            Canvas.position,
            Canvas.rotation,
            parent: Canvas);
        }

        #region Передвижение камеры

        // Проверка открыто ли сейчас главное меню
        if (GameObject.Find("MainMenu") == null)
        {
            // Проверка наличия ввода по горизонтальной оси и того что ширина поля больше 1
            if (Input.GetAxis("Horizontal") != 0 && gridHor > 1)
            {
                // Перемещение камеры по горизонтальной оси
                CamTrans.Translate(new Vector3(Input.GetAxis("Horizontal") * 0.3f, 0 , 0));
                // Ограничение движения камеры по горизонтали в зависимости от размеров поля
                CamTrans.position = new Vector3
                    (
                    Mathf.Clamp(CamTrans.position.x, 0, 10 * (gridHor - 1)),
                    CamTrans.position.y,
                    CamTrans.position.z
                    );
                    
            }
            // Проверка наличия ввода по вертикальной оси и того что длинна поля больше 1
            if (Input.GetAxis("Vertical") != 0 && gridVer > 1)
            {
                // Перемещение камеры по горизонтальной оси
                CamTrans.Translate(new Vector3(0, 0, 0.3f * Input.GetAxis("Vertical")));
                // Ограничение движения камеры по вертикали в зависимости от размеров поля
                CamTrans.position = new Vector3
                    (
                    CamTrans.position.x,
                    CamTrans.position.y,
                    Mathf.Clamp(CamTrans.position.z, 90.4f, 90.4f + 10 * (gridVer - 2))
                    );
            }
        }

        #endregion

    }

    /// <summary>
    /// Обновление надписей в окнах и метках
    /// </summary>
    /// <param name="type">Принимает тип зданиня</param>
    public static void Textupdate(string type)
    {
        // Выбор режима обновления текста в соответствии с типом строения
        switch (type)
        {
            case "Tree":

                // Удаление строки уровня в окне
                GameObject.Find("BuildingLevel").GetComponent<Text>().text = 
                    "";
                // Удаление кнопки улучшения
                Destroy(GameObject.Find("UpgradeButton"));
                // Вывод соответствующего описания
                GameObject.Find("Description").GetComponent<Text>().text = 
                    "Самое обычное дерево\n" +
                    "(Декорация)";

                break;
            case "House":

                // Обновление строки уровня строения в окне
                GameObject.Find("BuildingLevel").GetComponent<Text>().text = 
                    $"Уровень {objectBuffer.GetComponent<BuildingScript>().buildinglevel}";
                // Обновление индикатора уровня над выбранным строением
                objectBuffer.transform.Find("Sphere").Find("Canvas").Find("BuLe").GetComponent<Text>().text = 
                    $"{objectBuffer.GetComponent<BuildingScript>().buildinglevel}";
                // Вывод соответствующего описания
                GameObject.Find("Description").GetComponent<Text>().text = 
                    "Обычный жилой дом\n" +
                    "(Может быть улучшен)";

                break;
        }
    }

    /// <summary>
    /// Генератор игрового поля
    /// </summary>
    public void GenerateTerrain()
    {
        // Считываем значения со слайдеров в главном меню
        gridHor = (byte)GameObject.Find("HorSlider").GetComponent<Slider>().value;
        gridVer = (byte)GameObject.Find("VerSlider").GetComponent<Slider>().value;
        // Проверяем делали ли мы уже генерацию и размер поля (все случаи кроме 1х1)
        if (start == false && gridHor > 1 || gridVer > 1)
        {
            // Приписываем буферу начальное значение (в основном нужно для начального rotation)
            CellTransBuffer = gameObject.transform;
            // Закрываем главное меню
            CloseMenu();

            for (byte count_1 = 1; count_1 <= gridVer; count_1++)
            {
                // Меняем направление генерации на четных строчках (Влево)
                if (count_1 % 2 == 0) rlPoint = "LeftPoint";

                for (byte count_2 = 1; count_2 <= gridHor; count_2++)
                {
                    // Создаем ячейку в нужной нам позиции относительно последней (Либо по дефолтным координатам)
                    // И записываем трансформ созданной ячейки в буфер
                    CellTransBuffer = Instantiate(Cell, NextCellPos, CellTransBuffer.rotation).transform;
                    // Записываем координаты нужной нам метки для следующей ячейки
                    NextCellPos = CellTransBuffer.Find(rlPoint).transform.position;
                }
                // Стацим точку генерации следующей ячейки на следующей строчке
                NextCellPos = CellTransBuffer.Find("FrontPoint").transform.position;
                // Меняем направление генерации (Вправо)
                rlPoint = "RightPoint";
            }
            // Ставим флажок что мы провели генерацию
            start = true;
        }
        // Упрощенный тип генерации для одной клетки
        else if (start == false)
        {
            Instantiate(Cell, NextCellPos, Quaternion.identity);
            // Закрываем главное меню
            CloseMenu();
        }
    }
}
