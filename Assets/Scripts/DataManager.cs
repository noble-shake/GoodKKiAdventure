using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public enum GetItemType
{ 
    Equip,
    Playable,
}

public enum enumItemType
{ 
    NormalSword,
    NormalVest,
    NormalShoes,
    LookGoodSword,
    GoodShoes,
    CheatShoes,
    CheatSword,
}

public enum PlayableCharacters
{ 
    Female1,
    Female2,
    Female3,
    Female4,
    Male1,
    Male2,
    Male3,
    Male4,
}

[System.Serializable]
public class DataContainer
{
    public int curCharacter;
    public List<string> haveCharacter;
    public int curEquip;
    public List<string> haveEquip;
    public int level;
    public int exp;
    public int money;
    public int stageUnlock;
}


public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    // SAVE
    /*
     * Current Character (default : Female1)
     * Have Character
     * Current Equipped (default : null)
     * Current Equipped Index
     * Level
     * EXP
     * Money
     * HasItem
     * StageUnlockCounter
     */


    [Header("Data Management")]
    [SerializeField] TextAsset SaveData;
    DataContainer dataContainer;
    string FileDirectory;

    [Header("Prefabs")]
    [SerializeField] List<ItemSO> ItemObjects;
    [SerializeField] Item ItemPrefab;
    List<string> HasItemKeys;
    List<Item> HasItems;

    [SerializeField] List<PlayerSO> PlayerObjects;
    [SerializeField] PlayerScript PlayerPrefab;
    [SerializeField] PlayerWaitScript PlayerWaitingPrefab;
    List<PlayerScript> InGamePlayables;
    List<PlayerWaitScript> WaitingPlayables;

    [SerializeField] StatGrantsSO grants;

    public List<Item> ItemList { get { return HasItems; } }
    public List<string> ItemNames { get { return HasItemKeys; } }


    public PlayerWaitScript getRoomPlayerObject()
    {
        return WaitingPlayables[dataContainer.curCharacter];
    }

    public int level { get { return dataContainer.level; } set { dataContainer.level = value; } }
    public int exp { get { return dataContainer.exp; } set { dataContainer.exp = value; } }
    public int money { get { return dataContainer.money; } set { dataContainer.money = value; } }

    public Sprite TossItem()
    {
        float tossValue = UnityEngine.Random.Range(0f, 1f);
        if (tossValue < 0.77f)
        {
            return tossEquip();
        }
        else
        {
            return tossPlayable();
        }
         
        return null;
    }

    public Sprite tossEquip()
    {
        // int dice = System.Enum.GetValues(typeof(enumItemType)).Length;
        float tossValue = UnityEngine.Random.Range(0f, 1f);
        if (tossValue < 0.5f)
        {
            // normal
            List<ItemSO> NormalItems = new List<ItemSO>();
            for (int i = 0; i < ItemObjects.Count; i++)
            {
                ItemRare rare = ItemObjects[i].EquipRare;
                if (rare == ItemRare.Normal)
                {
                    NormalItems.Add(ItemObjects[i]);
                }
            }

            int dice = NormalItems.Count;
            int tossDice = UnityEngine.Random.Range(0, dice);

            enumItemType target = NormalItems[tossDice].ItemType;
            ItemInjection(target);
            return NormalItems[tossDice].Attach;
        }
        else if (tossValue < 0.85f)
        {
            List<ItemSO> RareItems = new List<ItemSO>();
            for (int i = 0; i < ItemObjects.Count; i++)
            {
                ItemRare rare = ItemObjects[i].EquipRare;
                if (rare == ItemRare.Rare)
                {
                    RareItems.Add(ItemObjects[i]);
                }
            }

            int dice = RareItems.Count;
            int tossDice = UnityEngine.Random.Range(0, dice);

            enumItemType target = RareItems[tossDice].ItemType;
            ItemInjection(target);
            return RareItems[tossDice].Attach;
        }
        else
        {
            // epic
            List<ItemSO> EpicItems = new List<ItemSO>();
            for (int i = 0; i < ItemObjects.Count; i++)
            {
                ItemRare rare = ItemObjects[i].EquipRare;
                if (rare == ItemRare.Epic)
                {
                    EpicItems.Add(ItemObjects[i]);
                }
            }

            int dice = EpicItems.Count;
            int tossDice = UnityEngine.Random.Range(0, dice);

            enumItemType target = EpicItems[tossDice].ItemType;
            ItemInjection(target);
            return EpicItems[tossDice].Attach;
        }
    }

    public Sprite tossPlayable()
    {
        int dice = System.Enum.GetValues(typeof(PlayableCharacters)).Length;
        int tossValue = UnityEngine.Random.Range(0, dice);

        PlayableCharacters PlayableTarget = (PlayableCharacters)tossValue;

        Debug.Log("Playable Get, But Not Implemented Yet");
        return ItemObjects[0].Attach;
    }


    private void SaveDataReset()
    {
        dataContainer.exp = 0;
        dataContainer.level = 1;
        dataContainer.money = 200;
        dataContainer.curCharacter = 0;
        dataContainer.curEquip = -1;
        dataContainer.haveCharacter = new List<string>();
        dataContainer.haveCharacter.Add(PlayableCharacters.Female1.ToString());
        dataContainer.haveEquip = new List<string>();
        dataContainer.haveEquip.Add(enumItemType.NormalSword.ToString());
        dataContainer.stageUnlock = 0;
    }
    private void GameInitializeLoad()
    {
        // After JSON LOAD
        foreach (string equip in dataContainer.haveEquip)
        {
            enumItemType itemType = (enumItemType)System.Enum.Parse(typeof(enumItemType), equip);
            ItemInjection(itemType);
        }

        if (dataContainer.curEquip != -1)
        {
            HasItems[dataContainer.curEquip].OnEquipped();
        }

        foreach (string playable in dataContainer.haveCharacter)
        { 
            PlayableCharacters playerType = (PlayableCharacters)System.Enum.Parse (typeof(PlayableCharacters), playable);
            PlayerInjection(playerType);
        }

        // dataContainer.curCharacter...
    }

    public StatGrantsSO getGrants()
    {
        return grants;
    }




    private void ItemInjection(enumItemType _item)
    {
        Item go = Instantiate(ItemPrefab, transform);
        go.data = ItemObjects[(int)_item];
        go.gameObject.SetActive(false);
        HasItemKeys.Add(go.data.ItemType.ToString());
        HasItems.Add(go);
        dataContainer.haveEquip = HasItemKeys;
    }

    private void PlayerInjection(PlayableCharacters _char)
    {
        PlayerWaitScript co = Instantiate(PlayerWaitingPrefab, transform);
        co.data = PlayerObjects[(int)_char];
        co.gameObject.SetActive(false);
        WaitingPlayables.Add(co);


        //Playable go = Instantiate(PlayablePrefab);
        //go.data = 
    }

    public void PlayerSortie(PlayableCharacters _char, Transform _moveTrs)
    {
        PlayerScript co = Instantiate(PlayerPrefab, _moveTrs);
        co.data = PlayerObjects[(int)_char];
        co.gameObject.SetActive(false);
        InGamePlayables.Add(co);
    }

    public int getCurrentEquip()
    {
        return dataContainer.curEquip;
    }

    public int getSelectedEquipID(Item _target)
    {
        for (int i = 0; i < HasItems.Count; i++)
        {
            if (_target == HasItems[i])
            {
                return i;
            }
        }

        Debug.LogError("Item Missing");
        return -1;
    }

    public void ItemSell(int _idx)
    {
        int sellCost = HasItems[_idx].data.Cost;
        dataContainer.money += sellCost;
        // Get Money.

        if (_idx < dataContainer.curEquip)
        {
            dataContainer.curEquip--;
        }

        Destroy(HasItems[_idx].gameObject);
        HasItemKeys.RemoveAt(_idx);
        HasItems.RemoveAt(_idx);

        dataContainer.haveEquip = HasItemKeys;

        GameSave();
    }

    public void LoadExternalSetup()
    {
        // 1. Resolution Setup
        //if (!PlayerPrefs.HasKey("Resolution"))
        //{
        //    string currentSet = EnumScreenSetup.WindowDefault.ToString();
        //    PlayerPrefs.SetString("Resolution", currentSet);
        //    resolution = currentSet;
        //}
        //else
        //{
        //    resolution = PlayerPrefs.GetString("Resolution");
        //}

        // 2. Language Setup
        //if (!PlayerPrefs.HasKey("Language"))
        //{
        //    string currentSet = LanguageSetup.GetLanguageCode(EnumLanguage.Japan);
        //    PlayerPrefs.SetString("Language", currentSet);
        //    language = currentSet;
        //}
        //else
        //{
        //    language = PlayerPrefs.GetString("Language");
        //}

        // 3. volume
        //if (!PlayerPrefs.HasKey("Volume"))
        //{
        //    PlayerPrefs.SetFloat("Volume", 0);
        //    volume = 0;
        //}
        //else
        //{
        //    volume = PlayerPrefs.GetFloat("Volume");
        //}

    }

    public void SaveLoad()
    {
        byte[] bytes;
        if (!File.Exists(FileDirectory))
        {
            // Create. 
            SaveDataReset();
            string data = JsonConvert.SerializeObject(dataContainer);
            bytes = System.Text.Encoding.UTF8.GetBytes(data);
            string encoded = System.Convert.ToBase64String(bytes);
            File.WriteAllText(FileDirectory, encoded);
        }

        string jsonData = File.ReadAllText(FileDirectory);
        bytes = System.Convert.FromBase64String(jsonData);
        string decoded = System.Text.Encoding.UTF8.GetString(bytes);
        SaveData = new TextAsset(decoded);
        JsonLoad();
    }

    IEnumerator Saver()
    {
        byte[] bytes;
        string data = JsonConvert.SerializeObject(dataContainer);
        bytes = System.Text.Encoding.UTF8.GetBytes(data);
        string encoded = System.Convert.ToBase64String(bytes);
        File.WriteAllText(FileDirectory, encoded);
        yield return null;
    }

    public void GameSave()
    {
        StartCoroutine(Saver());
    }

    public void JsonLoad()
    {
        /*
         *     public int curCharacter;
         *     public List<string> haveCharacter;
         *     public int curEquip;
         *     public List<string> haveEquip;
         *     public int level;
         *     public int exp;
         *     public int money;
         *     public int stageUnlock;
         */

        var SerialObject = Newtonsoft.Json.JsonConvert.DeserializeObject<DataContainer>(SaveData.ToString());

        dataContainer.curCharacter = SerialObject.curCharacter;
        dataContainer.haveCharacter = SerialObject.haveCharacter;
        dataContainer.curEquip = SerialObject.curEquip;
        dataContainer.haveEquip = SerialObject.haveEquip;
        dataContainer.level = SerialObject.level;
        dataContainer.exp = SerialObject.exp;
        dataContainer.money = SerialObject.money;
        dataContainer.stageUnlock = SerialObject.stageUnlock;
    }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        string OSDirectory = Application.persistentDataPath;
        string FileName = "/SaveData.data";

        FileDirectory = OSDirectory + FileName;

        Debug.Log(OSDirectory);
        Debug.Log(FileName);
        Debug.Log(FileDirectory);
    }

    public void DataManagerInitialize()
    {
        HasItemKeys = new List<string>();
        HasItems = new List<Item>();
        dataContainer = new DataContainer();
        WaitingPlayables = new List<PlayerWaitScript>();
        InGamePlayables = new List<PlayerScript>();

        LoadExternalSetup();
        SaveLoad();
        GameInitializeLoad();
    }

    public void UnEquipped()
    {
        dataContainer.curEquip = -1;
        GameSave();
    }
    public void EquipAdjust(Item _equip)
    {
        for (int i = 0; i < DataManager.instance.HasItems.Count; i++)
        {
            if (HasItems[i] == _equip)
            {
                dataContainer.curEquip = i;
                GameSave();
                break;
            }
        }
    }

    private void Start()
    {
        DataManagerInitialize();
    }
}
