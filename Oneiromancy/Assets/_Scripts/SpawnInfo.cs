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
                range.Wormii = new Vector2Int(1, 1);
                range.Kohl = new Vector2Int(0, 0);
                range.Cloud = new Vector2Int(0, 0);
                range.Alarm = new Vector2Int(0, 0);
                break;
            case 2:
                range.Wormii = new Vector2Int(1, 1);
                range.Kohl = new Vector2Int(1, 1);
                range.Cloud = new Vector2Int(0, 0);
                range.Alarm = new Vector2Int(0, 0);
                break;
            case 3:
                range.Wormii = new Vector2Int(2, 3);
                range.Kohl = new Vector2Int(1, 1);
                range.Cloud = new Vector2Int(1, 1);
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
        int enemyCount = 3 + layer / 2;
        int maxBosses = layer / 10;
        int BossCount = Random.Range(0, maxBosses + 1);
        enemyCount -= BossCount * 4;
        enemyCount = Mathf.Max(0, enemyCount);
        int wormiiCount = Random.Range(0, enemyCount + 1);
        enemyCount -= wormiiCount;
        int kohlCount = Random.Range(0, enemyCount + 1);
        enemyCount -= kohlCount;
        int cloudCount = enemyCount;

        range.Wormii = new Vector2Int(Mathf.Min(0, wormiiCount - wormiiCount / 2), Mathf.Max(0, wormiiCount));
        range.Kohl = new Vector2Int(Mathf.Min(0, kohlCount - kohlCount / 2), Mathf.Max(0, kohlCount));
        range.Cloud = new Vector2Int(Mathf.Min(0, cloudCount - cloudCount / 2), Mathf.Max(0, cloudCount));
        range.Alarm = new Vector2Int(BossCount, BossCount);
        return range;
    }
}
