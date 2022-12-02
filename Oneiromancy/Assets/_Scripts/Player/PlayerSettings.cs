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
    public float MoveSpeed = 5f;
    #endregion

    #region Sword
    [Header("Sword")]
    
    [Range(0, 2)]public float SlashCooldown = 1f;
    [Range(0.01f, 1)] public float SlashDuration = 0.5f;
    public LerpType SlashLerpType = LerpType.EaseInOut;
    public bool DebugSword = false;

    #endregion

    #region Magic
    [Header("Magic")]
    [Range(0, 2)] public float MagicCooldown = 1f;
    #endregion
}
