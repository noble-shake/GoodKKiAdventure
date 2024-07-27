using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "StatGrants", menuName = "ScriptableObjects/StatGrantsAsset", order = 1)]
public class StatGrantsSO : ScriptableObject
{
    [SerializeField] int gHP;
    [SerializeField] int gATK;
    [SerializeField] int gSPD;
    [SerializeField] int gDEF;
    [SerializeField] int gCRT;
    

    public int hpGrants { get { return gHP; } }
    public int atkGrants { get { return gATK; } }
    public int spdGrants { get { return gSPD; } }
    public int defGrants { get { return gDEF; } }
    public int crtGrants { get { return gCRT; } }
}
