#define DEBUG_DEFINE

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTilesBuilder : Singleton<GridTilesBuilder>
{
    [SerializeField] GameObject tileTemplate;
    [SerializeField] float positionOffset;
    [SerializeField] int gridHeight, gridLength;

    private Dictionary<int, Vector3> gridPositionCollection = new Dictionary<int, Vector3>();

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
        SetupTiles();
    }

    private void SetupTiles()
    {
        int count = GetTilesCount();

        for (int i = 0; i < gridHeight; i++)
        {
            if (i % 2 != 0)
            {
                for (int j = 0; j < gridLength; j++)
                {
                    SetupTile(j, i, count);
                    count--;
                }
            }
            else
            {
                for (int j = gridLength - 1; j >= 0; j--)
                {
                    SetupTile(j, i, count);
                    count--;
                }
            }
        }
    }

    private void SetupTile(int row, int column, int count)
    {
        Vector3 position = Vector3.zero;
        position.x = row + positionOffset;
        position.z = column + positionOffset;
        gridPositionCollection.Add(count, position);

#if DEBUG_DEFINE
        GameObject currentTile;
        currentTile = Instantiate(tileTemplate, transform);
        currentTile.transform.localPosition = position;
        currentTile.name = "Tile_" + count;
#endif
    }
    #endregion

    #region Public
    public int GetTilesCount()
    {
        return gridHeight * gridLength;
    }

    public Vector3 GetPositionForTile(int tileNum)
    {
        if(gridPositionCollection.ContainsKey(tileNum))
        {
            return gridPositionCollection[tileNum];
        }

        return Vector3.zero;
    }
    #endregion
}
