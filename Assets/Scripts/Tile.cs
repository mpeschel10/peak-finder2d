using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour, FreeMovement.Hoverable, FreeMovement.Clickable
{
   LayeredOutline layers;
    public System.Object arguments;
    public System.Action<System.Object> callback;

    void Awake()
    {
        layers = GetComponent<LayeredOutline>();
    }
    public void Hover() { layers.AddLayer("can-click"); }
    public void Unhover() { layers.SubtractLayer("can-click"); }

    public GameObject GetGameObject() { return gameObject; }

    public void Click()
    {
        callback(arguments);
    }
}
