using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static WorldBuilderScript;

public class ButtonActionsScript : MonoBehaviour
{

    /// <summary>
    /// Выход из программы
    /// </summary>
    public void CloseGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// Закрытие активного окна
    /// </summary>
    public static void CloseMenu()
    {
        Destroy(GameObject.FindGameObjectWithTag("Menu"));
    }

    /// <summary>
    /// 
    /// </summary>
    public static void CloseTextRedactor()
    {
        Destroy(GameObject.Find("InputText(Clone)"));
    }

    /// <summary>
    /// 
    /// </summary>
    public void SubTextUpdate()
    {
        Transform parent = GameObject.Find("MainCanvas").transform;
        Instantiate(GameObject.Find("WorldBuilder").GetComponent<WorldBuilderScript>().RedactMenu,
            parent.position,
            parent.rotation,
            parent: parent
            );
    }

    /// <summary>
    /// Улучшение здания
    /// </summary>
    public void Upgrade()
    {
        // Проверяем можно ли еще поднимать уровень здания
        if (objectBuffer.GetComponent<BuildingScript>().buildinglevel < 5)
        {
            // Увеличиваем уровень здания
            objectBuffer.GetComponent<BuildingScript>().buildinglevel++;
        }
        // Обновляем текст в окне и на значках
        TextUpdate("Building");
    }

    /// <summary>
    /// Промежуточный метод для привязки к кнопке
    /// </summary>
    public void SubCreate()
    {
        // Вызов метода создания строений с номером объекта из выпадающего списка
        BuildingScript.CreateBuilding(GameObject.Find("Dropdown").GetComponent<Dropdown>().value);
        // Закрытие окна
        CloseMenu();
    }

    /// <summary>
    /// 
    /// </summary>
    public void DeleteBuilding()
    {
        // Активация коллайдера клетки
        objectBuffer.GetComponent<BuildingScript>().myCell.GetComponent<Collider>().enabled = true;
        // Уничтожение выбранного строения
        Destroy(objectBuffer);
        // Закрытие меню
        CloseMenu();
    }
}