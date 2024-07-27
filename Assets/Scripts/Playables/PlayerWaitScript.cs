using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWaitScript : MonoBehaviour
{
    string PlayerName;
    string PlayerDescript;
    GameObject WaitingGameObject;

    PlayerSO Data;
    statusContainer CharacterStatus;

    public PlayerSO data { get { return Data; } set { Data = value; dataDisassemble(); } }

    private void dataDisassemble()
    {
        PlayerName = Data.Name;
        PlayerDescript = Data.Descript;
        CharacterStatus = Data.Stat;
        WaitingGameObject = Instantiate(Data.waitPrefab, transform);
    }

    public void LevelCheck()
    {
        // DataManager.instance
        int level = DataManager.instance.level -1;
        StatGrantsSO grants = DataManager.instance.getGrants();
        CharacterStatus.hp += (int)(CharacterStatus.hp * level * grants.hpGrants * 0.01f);
        CharacterStatus.atk += (int)(CharacterStatus.atk * level * grants.atkGrants * 0.01f);
        CharacterStatus.spd = (int)(level * grants.spdGrants);
        CharacterStatus.def = (int)(level * grants.defGrants);
        CharacterStatus.crt = (int)(level * grants.crtGrants);
    }
}
