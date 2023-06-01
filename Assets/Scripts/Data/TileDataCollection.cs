using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TileDataCollection", menuName = "ScriptableObjects/SpawnTileDataCollection", order = 1)]
public class TileDataCollection : ScriptableObject
{
    [Serializable]
    public struct TileData
    {
        public int tileIndex;
        public int ladderOrSnakeEndPoint;

        public TileData(int inTileIndex, int endPoint)
        {
            tileIndex = inTileIndex;
            ladderOrSnakeEndPoint = endPoint;
        }
    }

    private string toolTipText;

    [Header("List must not contain duplicate values.")]
    [SerializeField] List<TileData> tileDataList = new List<TileData>();


    public Dictionary<int, int> GetTileDataCollection()
    {
        Dictionary<int, int> tileDataCollection = new Dictionary<int, int>(); //Serializing to dictionary since we will be making a lot more read operations than write.
        for (int i = 0; i < tileDataList.Count; i++)
        {
            if (!tileDataCollection.ContainsKey(tileDataList[i].tileIndex))
            {
                tileDataCollection.Add(tileDataList[i].tileIndex, tileDataList[i].ladderOrSnakeEndPoint);
            }
        }

        return tileDataCollection;
    }
}
