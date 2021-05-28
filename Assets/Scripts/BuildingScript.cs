using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static WorldBuilderScript;

public class BuildingScript : MonoBehaviour, IPointerClickHandler
{
    public GameObject menu;
    public byte buildinglevel = 1;
    public string type;
    public void OnPointerClick(PointerEventData eventData)
    {
        Instantiate(menu,
            GameObject.Find("MainCanvas").transform.position,
            GameObject.Find("MainCanvas").transform.rotation,
            parent: GameObject.Find("MainCanvas").transform);

        objectBuffer = gameObject;

        Textupdate(gameObject, type);
    }
}
