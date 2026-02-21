using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Quest")]
public class Quest : ScriptableObject
{
    public string            QuestName;
    [TextArea] public string QuestDialog;

    public List<SelectEffect> selections = new List<SelectEffect>();
}
