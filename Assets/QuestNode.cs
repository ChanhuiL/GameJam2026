using UnityEngine;
using UnityEngine.EventSystems;

public class QuestNode : MonoBehaviour, IPointerClickHandler
{
    public MapCameraMovement mcm;
    public Quest quest;
       
    private bool isActivated = true;
    private Animator animator;
    public Sprite[] iconSprites;

    private void Awake()
    {
        GetComponent<Animator>().speed = 8f / Quest.RareTimer[(int)quest.rarity] / 12f;

        var SRs = GetComponentsInChildren<SpriteRenderer>();
        SRs[0].color = Quest.RareColor[(int)quest.rarity];
        SRs[1].sprite = iconSprites[(int)quest.questType];
    }

    public void SetCameraTarget(MapCameraMovement _camera)
    {
        mcm = _camera;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isActivated) return;
        GameHandlerScript.Instance.OpenQuest(quest);
        mcm.FocusCameraToHere(transform.position);
    }

    public void ExpiredQuest()
    {
        GameHandlerScript.Instance.CloseQuest();
        quest.expireSelection.Select();
    }
}
