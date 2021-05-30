using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static ButtonActionsScript;

public class WorldBuilderScript : MonoBehaviour
{

    #region Начальные переменные

    public GameObject[] buildings; // Список строений доступных для создания
    public GameObject Cell; // Ячейка, из которых строится игровое поле
    public GameObject menu; // Меню паузы
    public static GameObject objectBuffer; // Глобальный буфер для выбранного объекта

    private static byte grid_Ver; // Размер игрового поля по вертикали
    public static byte GetGrid_Ver()
    {
        return grid_Ver;
    } // Доступ к чтению gridVer 
    private static byte grid_Hor; // Размер игрового поля по горизонтали
    public static byte GetGrid_Hor()
    {
        return grid_Hor;
    } // Доступ к чтению gridHor 

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
    }

    private void Update()
    {
        // Проверка нажатия клавиши Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Открытие меню паузы, если не открыты другие окна
            if (GameObject.FindGameObjectWithTag("Menu") == null)
            {
                Instantiate(menu,
                Canvas.position,
                Canvas.rotation,
                parent: Canvas);
            }
            // Закрытие других окон, если такие есть
            else if (GameObject.FindGameObjectWithTag("Menu") != null)
            {
                CloseMenu();
            }
        }
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
        grid_Hor = (byte)GameObject.Find("HorSlider").GetComponent<Slider>().value;
        grid_Ver = (byte)GameObject.Find("VerSlider").GetComponent<Slider>().value;
        // Проверяем делали ли мы уже генерацию и размер поля (все случаи кроме 1х1)
        if (start == false && grid_Hor > 1 || grid_Ver > 1)
        {
            // Приписываем буферу начальное значение (в основном нужно для начального rotation)
            CellTransBuffer = gameObject.transform;
            // Закрываем главное меню
            CloseMenu();

            for (byte count_1 = 1; count_1 <= grid_Ver; count_1++)
            {
                // Меняем направление генерации на четных строчках (Влево)
                if (count_1 % 2 == 0) rlPoint = "LeftPoint";

                for (byte count_2 = 1; count_2 <= grid_Hor; count_2++)
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