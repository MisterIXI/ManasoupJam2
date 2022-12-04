using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _GameOverScreenPrefab;
    [SerializeField] private GameObject _IngameUIPrefab;
    [SerializeField] public PlayerSettings PlayerSettings;
    [SerializeField] private GameObject _KohlPrefab;
    [SerializeField] private GameObject _WormiiPrefab;
    [SerializeField] private GameObject _CloudPrefab;
    [SerializeField] private GameObject _AlarmPrefab;
    public Action<GameState, GameState> OnStateChange = delegate { };
    public GameState CurrentState { get; private set; }
    public List<Enemy> Enemies = new List<Enemy>();
    public int CurrentLayer { get; private set; } = 1;
    private UiScript _ingameUI;
    public enum GameState
    {
        MainMenu,
        Ingame,
        Portal,
        Paused,
        GameOver
    }
    private void Awake()
    {
        if (ReferenceManager.GameManager != null)
        {
            Destroy(gameObject);
            return;
        }
        ReferenceManager.GameManager = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.activeSceneChanged += OnSceneChange;
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "SceneGame")
        {
            SetState(GameState.Ingame);
        }
        else
        {
            CurrentState = GameState.MainMenu;
        }
    }
    public void OnSceneChange(Scene oldScene, Scene newScene)
    {
        // Debug.Log("Scene changed from " + oldScene.name + " to " + newScene.name);
        Debug.Log("Scene changed from " + oldScene.buildIndex + " to " + newScene.buildIndex);
        int mainIndex = SceneManager.GetSceneByName("SceneMain").buildIndex;
        int gameIndex = SceneManager.GetSceneByName("SceneGame").buildIndex;
        if (newScene.buildIndex != 1)
        {
            Debug.Log("MainMenu was activated");
            SetState(GameState.MainMenu);
        }

        if (newScene.buildIndex == 1)
        {
            Debug.Log("SceneGame was activated");
            SetState(GameState.Ingame);
        }

    }
    public void SetState(GameState state)
    {
        Debug.Log("Gamestate changed from " + CurrentState + " to " + state);
        GameState oldState = CurrentState;
        CurrentState = state;
        OnStateChange(oldState, CurrentState);
        if (CurrentState == GameState.Ingame)
        {
            if (oldState == GameState.MainMenu)
            {
                _ingameUI = Instantiate(_IngameUIPrefab).GetComponent<UiScript>();
            }
            ReferenceManager.PortalDoor.PlayDespawnAnimation();
            SpawnEnemies();
        }
        if (CurrentState == GameState.MainMenu)
        {
            ResetValues();
            if (oldState == GameState.GameOver)
            {
                Time.timeScale = 1;
            }
        }
        if (CurrentState == GameState.GameOver)
        {
            Time.timeScale = 0;
            Destroy(_ingameUI.gameObject);
            Instantiate(_GameOverScreenPrefab);
            ReferenceManager.PlayerController.UnSubscribeToInputEvents();
        }
    }
    public void UpdateHealth(int health)
    {
        _ingameUI.CurrentHealth = health;
    }
    private void SpawnEnemies()
    {
        SpawnInfo.EnemyCountRange enemySpawnRanges = SpawnInfo.GetRange(CurrentLayer);
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
            float randomDistance = UnityEngine.Random.Range(PlayerSettings.MinSpawnDistance, PlayerSettings.MaxSpawnDistance);
            Vector3 spawnPosition = new Vector3(Mathf.Cos(randomAngle * Mathf.Deg2Rad) * randomDistance, 1, Mathf.Sin(randomAngle * Mathf.Deg2Rad) * randomDistance);
            GameObject enemy = Instantiate(prefab, spawnPosition, Quaternion.identity);
            Enemies.Add(enemy.GetComponent<Enemy>());
        }
    }
    public void AdvanceLayer()
    {
        CurrentLayer++;
        _ingameUI.StageNr = CurrentLayer;
        SetState(GameState.Ingame);
    }

    public void SetBossHealthBarMax()
    {
        int maxHealth = 0;
        foreach (Enemy enemy in Enemies)
        {
            if (enemy is ClockBoss)
            {
                maxHealth += enemy.CurrentHealth;
            }
        }
        _ingameUI.MaxBossHealth = maxHealth;
        _ingameUI.CurrentBossHealth = maxHealth;
        _ingameUI.ToggleBossHealthBar(true);
    }
    public void UpdateBossHealthBar()
    {
        int currentHealth = 0;
        foreach (Enemy enemy in Enemies)
        {
            if (enemy is ClockBoss)
            {
                currentHealth += enemy.CurrentHealth;
            }
        }
        _ingameUI.CurrentBossHealth = currentHealth;
    }

    public void EnemyKilled(Enemy enemy)
    {
        Enemies.Remove(enemy);
        if (Enemies.Count == 0)
        {
            _ingameUI.ToggleBossHealthBar(false);
            SetState(GameState.Portal);
        }
    }
    private void ResetValues()
    {
        CurrentLayer = 1;
        foreach (Enemy enemy in Enemies)
        {
            Destroy(enemy.gameObject);
        }
        Enemies.Clear();
    }

    private void OnDrawGizmos()
    {
        if (PlayerSettings != null && PlayerSettings.ShowSpawnGizmos)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(Vector3.zero, PlayerSettings.MinSpawnDistance);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(Vector3.zero, PlayerSettings.MaxSpawnDistance);
        }
    }
}
