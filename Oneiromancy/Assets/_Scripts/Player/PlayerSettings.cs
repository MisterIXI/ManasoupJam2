using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSettings", menuName = "Player/PlayerSettings", order = 0)]
public class PlayerSettings : ScriptableObject
{
    public float MoveSpeed = 5f;
    public float SlashCooldown = 1f;
}
