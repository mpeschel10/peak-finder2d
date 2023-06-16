using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tiles : MonoBehaviour
{
    [SerializeField] GameObject canonicalTile;
    [SerializeField] Material winMaterial;

    void Awake() { canonicalTile.SetActive(false); }
    
    [SerializeField] int rowCount = 20;
    [SerializeField] int columnCount = 20;
    int _rowCount, _columnCount;
    GameObject[][] tiles;
    float[][] heights;

    void Start()
    {
        _rowCount = rowCount + 2;
        _columnCount = columnCount + 2;
        heights = new float[_rowCount][];
        for (int r = 0; r < heights.Length; r++)
        {
            heights[r] = new float[_columnCount];
            for (int c = 0; c < heights[r].Length; c++)
            {
                heights[r][c] = float.NaN;
            }
            heights[r][0] = 0;
            heights[r][heights[r].Length - 1] = 0;
        }
        for (int c = 0; c < heights[0].Length; c++)
        {
            heights[0][c] = 0;
            heights[heights.Length - 1][c] = 0;
        }
        
        tiles = new GameObject[_rowCount][];
        Vector3 rowOffset = canonicalTile.transform.localPosition;
        for (int r = 0; r < _rowCount; r++)
        {
            if (r == 0 || r == _rowCount - 1)
                continue;
            Vector3 cellOffset = rowOffset;
            tiles[r] = new GameObject[_columnCount];
            for (int c = 0; c < _columnCount; c++)
            {
                if (c == 0 || c == _columnCount - 1)
                    continue;
                GameObject newTileOffset = UnityEngine.Object.Instantiate(canonicalTile,
                    cellOffset,
                    canonicalTile.transform.localRotation,
                    transform);
                
                newTileOffset.SetActive(true);
                cellOffset += Vector3.right;

                GameObject tileObject = newTileOffset.transform.GetChild(0).gameObject;
                tiles[r][c] = tileObject;

                Tile tile = tileObject.GetComponent<Tile>();
                tile.arguments = (r, c);
                tile.callback = this.Click;
            }
            rowOffset += Vector3.forward;
        }
    }

    void Collapse(int r, int c)
    {
        float height = GetHeight(r, c);
        heights[r][c] = height;
        Color color = Color.HSVToRGB(height, 1f, 0.5f);
        tiles[r][c].GetComponent<MeshRenderer>().material.color = color;
    }

    float GetHeight(int r, int c)
    {
        double flavor = Math.Sin(r / 2d) + Math.Cos(c / 2d) + Math.Sin((r + c) / 4d); // spans -3 to 3
        float packed = (float) (flavor * 0.1664 + 0.50001); // spans (0, 1)
        packed = packed * 0.8f; // Looks better if peaks are purple rather than red
        return packed;
    }

    (int, int)[] offsets = {
        (-1, -1), ( 0, -1), ( 1, -1),
        (-1,  0),           ( 1,  0),
        (-1,  1), ( 0,  1), ( 1,  1),
    };
    
    bool IsPeak(int r, int c)
    {
        float center = heights[r][c];
        if (float.IsNaN(center)) return false;
        foreach ((int rowOffset, int columnOffset) in offsets)
        {
            if (! (heights[r + rowOffset][c + columnOffset] < center))
            {
                return false;
            }
        }
        return true;
    }
    
    void CheckWin()
    {
        for (int r = 1; r < rowCount - 1; r++)
        {
            for (int c = 1; c < columnCount - 1; c++)
            {
                if (IsPeak(r, c))
                {
                    tiles[r][c].GetComponent<MeshRenderer>().material = winMaterial;
                }
            }
        }
    }

    public void Click(System.Object arguments)
    {
        (int r, int c) = ( (int, int) ) arguments;
        Collapse(r, c);
        CheckWin();
    }
}
