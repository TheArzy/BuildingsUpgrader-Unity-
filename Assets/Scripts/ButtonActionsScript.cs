using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static WorldBuilderScript;

public class ButtonActionsScript : MonoBehaviour
{
    public GameObject[] buildings; // Список строений доступных для создания

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
        Textupdate("House");
    }

    #region Постройка зданий

    /// <summary>
    /// Промежуточный метод для привязки к кнопке
    /// </summary>
    public void SubCreate()
    {
        // Вызов метода создания строений с номером объекта из выпадающего списка
        CreateBuilding(GameObject.Find("Dropdown").GetComponent<Dropdown>().value);
        // Закрытие окна
        CloseMenu();
    }

    /// <summary>
    /// Создание строения
    /// </summary>
    /// <param name="value">Номер строения в выпадаеющем списке</param>
    public void CreateBuilding(int value)
    {
        // Буфер для созданного нами строения
        GameObject buffer;
        // Выбор строения для создания в соответствии с значением выпадающего списка
        switch (value)
        {
            case 0:
                // Запоминаем созданное строение
                buffer = Instantiate(buildings[1], objectBuffer.transform.position, buildings[1].transform.rotation);
                // Приписываем созданному строению его тип
                buffer.GetComponent<BuildingScript>().type = "House";
                break;
            case 1:
                // Запоминаем созданное строение
                buffer = Instantiate(buildings[0], objectBuffer.transform.position, buildings[0].transform.rotation);
                // Приписываем созданному строению его тип
                buffer.GetComponent<BuildingScript>().type = "Tree";
                break;
        }
        // Отключаем коллайдер ячейки чтобы предотвратить повторное открытие окна создания строений
        objectBuffer.GetComponent<Collider>().enabled = false;
    }

    #endregion

}
