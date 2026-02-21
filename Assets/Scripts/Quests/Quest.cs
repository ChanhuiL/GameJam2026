using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Quest")]
public class Quest : ScriptableObject
{
    public enum QuestRarity { NORMAL, UNCOMMON, RARE };
    public enum QuestType { TYPE_DIALOG, TYPE_REQUEST, TYPE_WARNING, TYPE_TREASURE };
    public static Color32[] RareColor = { Color.white, Color.green, Color.red };
    public static float[] RareTimer = { 60f, 30f, 12f };

    public QuestRarity        rarity = QuestRarity.NORMAL;
    public QuestType          questType = QuestType.TYPE_DIALOG;
    public string             QuestName;
    [TextArea] public string  QuestDialog;

    public SelectEffect       expireSelection;
    public List<SelectEffect> selections = new List<SelectEffect>();

    public string Get_AftermathDialog(int idx)
    {
        return selections[idx].Get_AfterMathDialog();
    }
}
