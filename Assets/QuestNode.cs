using UnityEngine;
using UnityEngine.EventSystems;

public class QuestNode : MonoBehaviour, IPointerClickHandler
{
    public Quest quest;
       
    public Sprite[] iconSprites;

    public System.Action<QuestNode> OnLifeEnd;

    private void Awake()
    {
    }

    public void SetUp(Quest quest)
    {
        this.quest = quest;

        GetComponent<Animator>().speed = 8f / Quest.RareTimer[(int)quest.rarity] / 12f;

        var SRs = GetComponentsInChildren<SpriteRenderer>();
        SRs[0].color = Quest.RareColor[(int)quest.rarity];
        SRs[1].sprite = iconSprites[(int)quest.questType];
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameHandlerScript.Instance.currentNode == this) return;

        GameHandlerScript.Instance.OpenQuest(quest, this);
    }

    public void ExpiredQuest()
    {
        if (GameHandlerScript.Instance.currentNode == this)
            GameHandlerScript.Instance.CloseQuest();
        quest.expireSelection.Select();
        OnLifeEnd?.Invoke(this);
    }

    public void Solved()
    {
        OnLifeEnd?.Invoke(this);
    }
}
