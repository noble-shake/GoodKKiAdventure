using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerAsset", order = 1)]
public class PlayerSO : ScriptableObject
{
    [SerializeField] string CharName;
    [SerializeField] string CharDescript;
    [SerializeField] int HP;
    [SerializeField] int ATTACK;
    [SerializeField] int SPEED;
    [SerializeField] int DEFENCE;
    [SerializeField] int CRITICAL;
    [SerializeField] GameObject WaitingPrefab;
    [SerializeField] GameObject InGamePrefab;
    public string Name { get { return CharName; } }
    public string Descript { get { return CharDescript; } }
    public GameObject waitPrefab { get { return WaitingPrefab; } }
    public GameObject gamePrefab { get { return InGamePrefab; } }
    public statusContainer Stat { get { return new statusContainer() { hp = HP, atk = ATTACK, spd = SPEED, def = DEFENCE, crt = CRITICAL }; } }
}
