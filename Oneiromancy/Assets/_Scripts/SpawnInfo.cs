using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnInfo
{
    public struct EnemyCountRange
    {
        public Vector2Int Wormii;
        public Vector2Int Kohl;
        public Vector2Int Cloud;
        public Vector2Int Alarm;
    }
    public static EnemyCountRange GetRange(int layer)
    {
        EnemyCountRange range = new EnemyCountRange();
        switch (layer)
        {
            case 1:
                range.Wormii = new Vector2Int(5, 5);
                range.Kohl = new Vector2Int(0, 0);
                range.Cloud = new Vector2Int(0, 0);
                range.Alarm = new Vector2Int(0, 0);
                break;
            case 2:
                range.Wormii = new Vector2Int(2, 4);
                range.Kohl = new Vector2Int(1, 1);
                range.Cloud = new Vector2Int(0, 0);
                range.Alarm = new Vector2Int(0, 0);
                break;
            case 3:
                range.Wormii = new Vector2Int(3, 5);
                range.Kohl = new Vector2Int(1, 2);
                range.Cloud = new Vector2Int(1, 1);
                range.Alarm = new Vector2Int(0, 0);
                break;
            case 4:
                range.Wormii = new Vector2Int(4, 6);
                range.Kohl = new Vector2Int(1, 3);
                range.Cloud = new Vector2Int(1, 2);
                range.Alarm = new Vector2Int(1, 1);
                break;
            case 5:
                range.Wormii = new Vector2Int(5, 7);
                range.Kohl = new Vector2Int(2, 4);
                range.Cloud = new Vector2Int(1, 3);
                range.Alarm = new Vector2Int(0, 0);
                break;
            case 6:
                range.Wormii = new Vector2Int(6, 8);
                range.Kohl = new Vector2Int(2, 4);
                range.Cloud = new Vector2Int(2, 4);
                range.Alarm = new Vector2Int(0, 0);
                break;
            case 7:
                range.Wormii = new Vector2Int(9, 10);
                range.Kohl = new Vector2Int(3, 5);
                range.Cloud = new Vector2Int(2, 5);
                range.Alarm = new Vector2Int(1, 1);
                break;
            case 8:
                range.Wormii = new Vector2Int(10, 11);
                range.Kohl = new Vector2Int(3, 5);
                range.Cloud = new Vector2Int(3, 6);
                range.Alarm = new Vector2Int(0, 0);
                break;
            case 9:
                range.Wormii = new Vector2Int(12, 14);
                range.Kohl = new Vector2Int(4, 5);
                range.Cloud = new Vector2Int(3, 7);
                range.Alarm = new Vector2Int(0, 0);
                break;
            case 10: // Boss #1
                range.Wormii = new Vector2Int(0, 0);
                range.Kohl = new Vector2Int(0, 0);
                range.Cloud = new Vector2Int(0, 0);
                range.Alarm = new Vector2Int(1, 1);
                break;
            default:
                range = ProceduralRandom(layer);
                break;
        }
        return range;
    }

    private static EnemyCountRange ProceduralRandom(int layer)
    {
        EnemyCountRange range = new EnemyCountRange();
        int enemyCount = 2 * layer;
        int maxBosses = layer / 10;
        int BossCount = Random.Range(0, maxBosses + 1);
        // enemyCount -= BossCount * 4;
        // enemyCount = Mathf.Max(0, enemyCount);
        int wormiiCount = Random.Range(0, enemyCount + 1);
        enemyCount -= wormiiCount;
        int kohlCount = Random.Range(0, enemyCount + 1);
        enemyCount -= kohlCount;
        int cloudCount = enemyCount;

        range.Wormii = new Vector2Int(layer,wormiiCount);
        range.Kohl = new Vector2Int(layer,kohlCount);
        range.Cloud = new Vector2Int(layer,cloudCount);
        range.Alarm = new Vector2Int(BossCount,BossCount);
        return range;
    }
}
