using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static WorldBuilderScript;

public class BuildingScript : MonoBehaviour, IPointerClickHandler
{
    public GameObject menu; // Меню, вызываемое при нажатии на коллайдер объекта
    public byte buildinglevel = 1; // Уровень данного строения
    private string type = "None"; // Тип данного строения

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
        // Обновление текста в окне в соответствии с типом строения
        Textupdate(type);
    }

    /// <summary>
    /// Создание строения
    /// </summary>
    /// <param name="value">Номер строения в выпадаеющем списке</param>
    public static void CreateBuilding(int value)
    {
        // Буфер для созданного нами строения
        GameObject buffer;
        // Буфер для массива доступных строений
        GameObject[] comp = GameObject.Find("WorldBuilder").GetComponent<WorldBuilderScript>().buildings;
        // Выбор строения для создания в соответствии с значением выпадающего списка
        switch (value)
        {
            case 0:
                // Запоминаем созданное строение
                buffer = Instantiate(comp[0], objectBuffer.transform.position, comp[0].transform.rotation);
                // Приписываем созданному строению его тип
                buffer.GetComponent<BuildingScript>().type = "Tree";
                break;
            case 1:
                // Запоминаем созданное строение
                buffer = Instantiate(comp[1], objectBuffer.transform.position, comp[1].transform.rotation);
                // Приписываем созданному строению его тип
                buffer.GetComponent<BuildingScript>().type = "House";
                break;
        }
        // Отключаем коллайдер ячейки чтобы предотвратить повторное открытие окна создания строений
        objectBuffer.GetComponent<Collider>().enabled = false;
    }
}