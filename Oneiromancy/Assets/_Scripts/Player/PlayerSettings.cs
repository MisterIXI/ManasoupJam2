using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSettings", menuName = "Player/PlayerSettings", order = 0)]
public class PlayerSettings : ScriptableObject
{
    public enum LerpType { Linear, EaseIn, EaseOut, EaseInOut }
    public float Lerp(float t, LerpType lerpType)
    {
        switch (lerpType)
        {
            case LerpType.Linear:
                return t;
            case LerpType.EaseIn:
                return Mathf.Sin(t * Mathf.PI * 0.5f);
            case LerpType.EaseOut:
                return 1 - Mathf.Cos(t * Mathf.PI * 0.5f);
            case LerpType.EaseInOut:
                return Mathf.Sin(t * Mathf.PI - Mathf.PI * 0.5f) * 0.5f + 0.5f;
            default:
                return t;
        }
    }

    #region Movement
    [Header("Movement")]
    [Range(0, 1)] public float MoveSpeedMult = 0.5f;
    [Range(0, 10)] public float MoveAcceleration = 5f;
    #endregion

    #region Sword
    [Header("Sword")]
    [Range(1, 5)] public int SwordDamage = 2;
    [Range(0, 2)] public float SlashDirectionDeltaMin = 0.3f;
    [Range(0, 2)] public float SlashCooldown = 0.05f;
    [Range(0.01f, 1)] public float SlashDuration = 0.5f;
    public LerpType SlashLerpType = LerpType.EaseIn;
    public bool DebugSword = false;

    #endregion

    #region Magic
    [Header("Magic")]
    [Range(1, 5)] public int MagicDamage = 1;
    [Range(0, 2)] public float MagicCooldown = 1f;
    [Range(0f, 50f)] public float MagicProjectileSpeed = 10f;
    public float BoundsLength = 10f;
    #endregion
    #region Blocking
    [Header("Blocking")]
    [Range(0, 1f)] public float BlockSlowdown = 0.2f;
    [Range(0.5f, 2f)] public float BlockColliderSize = 1f;
    public int MaxProjectiles = 8;
    public bool CanPushEnemies = true;
    #endregion
    #region Health
    [Header("Health")]
    public int MaxHealth = 5;

    #endregion

    #region Enemies
    [Header("Enemies")]
    [Range(0f, 50f)] public float EnemyKnockBackForce = 5f;
    [Range(1f, 20f)] public float EnemySpeed = 5f;
    [Header("FollowEnemy")]
    public Vector2Int FollowEnemyHealthRange = new Vector2Int(15, 30);


    [Header("ShooterEnemy")]
    public Vector2Int SE_HealthRange = new Vector2Int(10, 25);
    [Range(5f, 50f)] public float SE_ShotRange = 10f;
    [Range(0f, 50f)] public float EnemyProjectileSpeed = 50f;
    [Range(0f, 50f)] public float SE_ShotCooldown = 1f;
    [Header("ChargeEnemy")]
    public Vector2Int ChargeEnemyHealthRange = new Vector2Int(15, 25);
    [Range(0.1f, 5f)] public float CE_Cooldown = 1f;
    [Range(0.1f, 2f)] public float CE_ChargeTime = 0.3f;
    [Range(1, 30)] public float CE_ImpluseMultiplier = 1;
    [Range(5f, 50f)] public float CE_Range = 15f;

    #endregion

    #region Camera
    [Header("Camera")]
    public Vector3 CameraOffset = new Vector3(0, 8f, -5);
    #endregion

    #region Portal
    [Header("Portal")]
    [Range(1f, 5f)] public float PortalAnimDuration = 2f;
    [Range(-5, 15f)] public float PortalMaxPosition = 5f;
    [Range(-15f, 0f)] public float PortalMinPosition = -5f;
    public bool PortalGizmos = false;
    #endregion
    
    #region Spawning
    [Header("Spawning")]
    [Range(0f,50f)] public float MinSpawnDistance = 10f;
    [Range(5f, 70f)] public float MaxSpawnDistance = 20f;
    #endregion 
    }
