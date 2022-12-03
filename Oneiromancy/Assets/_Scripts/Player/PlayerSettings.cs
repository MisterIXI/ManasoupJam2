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
    public float MoveSpeed = 15f;
    #endregion

    #region Sword
    [Header("Sword")]
    [Range(0, 2)] public float SlashDirectionDeltaMin = 0.3f;
    [Range(0, 2)] public float SlashCooldown = 0.05f;
    [Range(0.01f, 1)] public float SlashDuration = 0.5f;
    public LerpType SlashLerpType = LerpType.EaseIn;
    public bool DebugSword = false;

    #endregion

    #region Magic
    [Header("Magic")]
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
    [Range(0f, 50f)] public float EnemyProjectileSpeed = 50f;
    #endregion
}
