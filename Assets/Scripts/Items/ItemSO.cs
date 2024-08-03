using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class statusContainer
{
    public int hp;
    public int atk;
    public int spd;
    public int def;
    public int crt;
}

public enum ItemRare
{ 
    Normal,
    Rare,
    Epic,
}


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ItemAsset", order = 1)]
public class ItemSO : ScriptableObject
{
    [SerializeField] enumItemType itemType;
    [SerializeField] string EquipName;
    [SerializeField] string EquipDescript;
    [SerializeField] Sprite ItemAttach;
    [SerializeField] int HP;
    [SerializeField] int ATTACK;
    [SerializeField] int SPEED;
    [SerializeField] int DEFENCE;
    [SerializeField] int CRITICAL;
    [SerializeField] int cost;
    [SerializeField] ItemRare rare;
    public string Name { get { return EquipName; }}
    public string Descript{ get { return EquipDescript; }}
    public int Cost{ get { return cost; }} 
    public Sprite Attach { get { return ItemAttach; }}
    public statusContainer Stat { get { return new statusContainer() { hp=HP, atk=ATTACK, spd=SPEED, def=DEFENCE, crt=CRITICAL}; }}

    public ItemRare EquipRare { get { return rare; } }

    public enumItemType ItemType { get { return itemType; } }
}
