using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tiles : MonoBehaviour
{
    [SerializeField] GameObject canonicalTile;

    void Awake() { canonicalTile.SetActive(false); }
    
    [SerializeField] int rows = 100;
    [SerializeField] int columns = 100;
    void Start()
    {
        Vector3 rowOffset = canonicalTile.transform.localPosition;
        for (int r = 0; r < rows; r++)
        {
            Vector3 cellOffset = rowOffset;
            for (int c = 0; c < columns; c++)
            {
                GameObject newTileOffset = UnityEngine.Object.Instantiate(canonicalTile,
                    cellOffset,
                    canonicalTile.transform.localRotation,
                    transform);
                
                newTileOffset.SetActive(true);
                cellOffset += Vector3.right;

                GameObject newTile = newTileOffset.transform.GetChild(0).gameObject;

                Color color = Color.HSVToRGB(GetHeight(r, c), 1f, 0.5f);
                newTile.GetComponent<MeshRenderer>().material.color = color;
            }
            rowOffset += Vector3.forward;
        }
    }

    float GetHeight(int r, int c)
    {
        double flavor = Math.Sin(r / 2d) + Math.Cos(c / 2d) + Math.Sin((r + c) / 4d); // spans -3 to 3
        float packed = (float) (flavor * 0.1665 + 0.5); // spans 0 to 1
        packed = packed * 0.8f; // Looks a little better if highest points stop at purple rather than red
        Debug.Log(packed);
        return packed;
    }

}
