using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static WorldBuilderScript;

public class CellScript : MonoBehaviour, IPointerClickHandler
{
    public GameObject menu; // Меню, вызываемое при нажатии на коллайдер объекта

    /// <summary>
    /// Открытие окна создания строений при нажатии на коллайдер клетки
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
            // Запись выбранной клетки в буфер 
            objectBuffer = gameObject;
    }
}