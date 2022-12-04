using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        ReferenceManager.GameManager = this;
    }
    [SerializeField] private PlayerSettings _playerSettings;
    [SerializeField] private GameObject _KohlPrefab;
    [SerializeField] private GameObject _WormiiPrefab;
    [SerializeField] private GameObject _CloudPrefab;
    [SerializeField] private GameObject _AlarmPrefab;
    public Action<GameState, GameState> OnStateChange = delegate { };
    public GameState CurrentState { get; private set; }
    public List<Enemy> Enemies = new List<Enemy>();
    public int CurrentLayer { get; private set; } = 1;

    public enum GameState
    {
        MainMenu,
        Ingame,
        Portal,
        Paused,
        GameOver
    }

    public void SetState(GameState state)
    {
        Debug.Log("Gamestate changed from " + CurrentState + " to " + state);
        GameState oldState = CurrentState;
        CurrentState = state;
        OnStateChange(oldState, CurrentState);
        if (CurrentState == GameState.Ingame)
        {
            ReferenceManager.PortalDoor.PlaySpawnAnimation();
        }
    }

    private void SpawnEnemies(SpawnInfo.EnemyCountRange enemySpawnRanges)
    {
        int wormiiCount = UnityEngine.Random.Range(enemySpawnRanges.Wormii.x, enemySpawnRanges.Wormii.y + 1);
        int kohlCount = UnityEngine.Random.Range(enemySpawnRanges.Kohl.x, enemySpawnRanges.Kohl.y + 1);
        int cloudCount = UnityEngine.Random.Range(enemySpawnRanges.Cloud.x, enemySpawnRanges.Cloud.y + 1);
        int alarmCount = UnityEngine.Random.Range(enemySpawnRanges.Alarm.x, enemySpawnRanges.Alarm.y + 1);

        SpawnLoop(_WormiiPrefab, wormiiCount);
        SpawnLoop(_KohlPrefab, kohlCount);
        SpawnLoop(_CloudPrefab, cloudCount);
        SpawnLoop(_AlarmPrefab, alarmCount);
    }

    private void SpawnLoop(GameObject prefab, int count)
    {
        for (int i = 0; i < count; i++)
        {
            float randomAngle = UnityEngine.Random.Range(0, 360);
            float randomDistance = UnityEngine.Random.Range(_playerSettings.MinSpawnDistance, _playerSettings.MaxSpawnDistance);
            Vector3 spawnPosition = new Vector3(Mathf.Cos(randomAngle * Mathf.Deg2Rad) * randomDistance, 0, Mathf.Sin(randomAngle * Mathf.Deg2Rad) * randomDistance);
            GameObject enemy = Instantiate(prefab, spawnPosition, Quaternion.identity);
            Enemies.Add(enemy.GetComponent<Enemy>());
        }
    }
    public void AdvanceLayer()
    {
        SetState(GameState.Ingame);
        CurrentLayer++; 
    }

    public void EnemyKilled(Enemy enemy)
    {
        Enemies.Remove(enemy);
        if (Enemies.Count == 0)
        {
            SetState(GameState.Portal);
        }
    }


}
