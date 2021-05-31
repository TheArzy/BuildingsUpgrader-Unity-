using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static WorldBuilderScript;

public class BuildingScript : MonoBehaviour, IPointerClickHandler
{
    public string Name = "Name_None"; // Название здания
    public string Type = "Type_None"; // Тип данного строения
    public string Category = "Cat_None"; // Категория данного строения
    public string Description = "Descr_None"; // Описание строения

    public GameObject myCell; // Хранилище данных о клетке, на которой стоит строение
    public GameObject menu; // Меню, вызываемое при нажатии на коллайдер объекта
    public byte buildinglevel = 1; // Уровень данного строения

    /// <summary>
    /// Открытие соответствующего окна свойств\улучшения при нажатии на коллайдер строения
    /// Имеет защиту от нажатия сквозь открытые окна
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        // Открытие указанного окна в главном канвасе
        Instantiate(menu,
            GameObject.Find("MainCanvas").transform.position,
            GameObject.Find("MainCanvas").transform.rotation,
            parent: GameObject.Find("MainCanvas").transform);
        // Запись выбранного строения в буфер 
        objectBuffer = gameObject;
        // Обновление текста в окне в соответствии с выбранным строением
        TextUpdate(Category);
    }

    /// <summary>
    /// Создание указанного строения с указанными параметрами
    /// </summary>
    /// <param name="index">Номер строения в списке</param>
    /// <param name="name">Имя</param>
    /// <param name="type">Тип</param>
    /// <param name="category">Категория</param>
    /// <param name="description">Описание</param>
    public static void BuildInst(
        byte index, string name, string type, 
        string category, string description)
    {
        // Буфер для созданного нами строения
        GameObject buffer;
        // Буфер для массива доступных строений
        GameObject[] comp = GameObject.Find("WorldBuilder").GetComponent<WorldBuilderScript>().buildings;

        // Создаем и запоминаем строение
        buffer = Instantiate(comp[index], objectBuffer.transform.position, comp[index].transform.rotation);

        // Приписываем созданному строению его имя
        buffer.GetComponent<BuildingScript>().Name =
            name;
        // Приписываем созданному строению его тип
        buffer.GetComponent<BuildingScript>().Type =
            type;
        // Приписываем созданному строению его категорию
        buffer.GetComponent<BuildingScript>().Category =
            category;
        // Приписываем созданному строению соответствующее описание
        buffer.GetComponent<BuildingScript>().Description =
            description;

        // Записываем строению данные о ячейке, на которой оно стоит
        buffer.GetComponent<BuildingScript>().myCell =
            objectBuffer;
    }

    /// <summary>
    /// Создание строения
    /// </summary>
    /// <param name="value">Номер строения в выпадаеющем списке</param>
    public static void CreateBuilding(int value)
    {
        switch (value)
        {
            case 0:
                BuildInst(0, "Tree", "PineTree", "Decor",
                    "Самое обычное дерево\n" +
                    "(Декорация)");
                break;

            case 1:
                BuildInst(1, "House", "House", "Building",
                    "Обычный жилой дом\n");
                break;

            case 2:
                BuildInst(2, "TallBuilding", "TallBuilding", "Building",
                    "Многоэтажный дом\n");
                break;
        }
        // Отключаем коллайдер ячейки чтобы предотвратить повторное открытие окна создания строений
        objectBuffer.GetComponent<Collider>().enabled = false;
    }
}