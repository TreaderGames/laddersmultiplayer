using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTiles : MonoBehaviour
{
    [SerializeField] GameObject tileTemplate;
    [SerializeField] float positionOffset;
    [SerializeField] int gridHeight, gridLength;

    #region Unity
    // Start is called before the first frame update
    void Start()
    {
        InitTiles();
    }
    #endregion

    #region private
    private void InitTiles()
    {
        InstantiateTiles();
    }

    private void InstantiateTiles()
    {
        int count = gridHeight * gridLength;

        for (int i = 0; i < gridHeight; i++)
        {
            if (i % 2 != 0)
            {
                for (int j = 0; j < gridLength; j++)
                {
                    InstantiateTile(j, i, count);
                    count--;
                }
            }
            else
            {
                for (int j = gridLength - 1; j >= 0; j--)
                {
                    InstantiateTile(j, i, count);
                    count--;
                }
            }
        }
    }

    private void InstantiateTile(int row, int column, int count)
    {
        GameObject currentTile;
        Vector3 position = Vector3.zero;
        currentTile = Instantiate(tileTemplate, transform);
        position.x = row + positionOffset;
        position.z = column + positionOffset;

        currentTile.transform.localPosition = position;
        currentTile.name = "Tile_" + count;
    }
    #endregion
}
