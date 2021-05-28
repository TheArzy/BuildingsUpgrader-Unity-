using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static WorldBuilderScript;

public class BuildingScript : MonoBehaviour, IPointerClickHandler
{
    public GameObject menu; // Меню, вызываемое при нажатии на коллайдер объекта
    public byte buildinglevel = 1; // Уровень данного строения
    public string type; // Тип данного строения

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
}
