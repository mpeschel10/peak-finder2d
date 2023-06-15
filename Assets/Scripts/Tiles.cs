using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiles : MonoBehaviour
{
    [SerializeField] GameObject canonicalTile;

    void Awake() { canonicalTile.SetActive(false); }
    
    [SerializeField] int rows = 10;
    [SerializeField] int columns = 10;
    void Start()
    {
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < columns; c++)
            {
                // Object.Instantiate(canonicalTile, );

            }
        }
    }

}
