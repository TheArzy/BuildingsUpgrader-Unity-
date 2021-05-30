using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMe_Script : MonoBehaviour
{
    void Update()
    {
        // Заставляет таблетку с уровнем строения пристально смотреть на тебя
        gameObject.transform.LookAt(GameObject.Find("CameraHolder").transform);
    }
}
