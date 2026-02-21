using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OnlyStatData
{
    public string fileName;
    public string displayName;
    public int[] stats;
    public int[] amounts;
    public string aftermathDialog;
}

[System.Serializable]
public class OnlyStatDataWrapper
{
    public List<OnlyStatData> OnlyStats;
}

[CreateAssetMenu(menuName = "Selection/OnlyStats")]
public class SelectEffect_Stat : SelectEffect
{
    public GameHandlerScript.StatType[]  stats;
    public int[]                         amounts;

    public override void Select()
    {
        GameHandlerScript.Instance.DecisionMade(stats, amounts);
    }
}
