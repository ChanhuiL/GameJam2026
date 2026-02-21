using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class Event_Pages : MonoBehaviour
{
    Quest curQuest;
    bool isLastBoardNumberOne = false;
    public GameObject[] questboardObjects = new GameObject[2];
    private RandomEventScript[] questboardScripts = new RandomEventScript[2];

    private void Awake()
    {
        for (int i = 0; i < questboardObjects.Length; ++i)
            questboardScripts[i] = questboardObjects[i].GetComponent<RandomEventScript>();
    }

    private void Update()
    {
        if(Keyboard.current.escapeKey.wasPressedThisFrame)
            questboardScripts[isLastBoardNumberOne ? 0 : 1].CloseQuestBoard();
    }

    public void OpenQuest(Quest interactedEvent)
    {
        curQuest = interactedEvent;

        int preIdx, postIdx;
        if (isLastBoardNumberOne)
        {
            preIdx = 0;
            postIdx = 1;
        }
        else
        {
            preIdx = 1;
            postIdx = 0;
        }

        questboardScripts[preIdx].CloseQuestBoard();
        questboardObjects[postIdx].transform.SetAsLastSibling();
        questboardScripts[postIdx].SetRandomEvent(interactedEvent);

        isLastBoardNumberOne = !isLastBoardNumberOne;
    }

    public void ShowAftermath(int idx)
    {
        questboardScripts[isLastBoardNumberOne ? 0 : 1].ShowAftermath(curQuest, idx);
    }

    public void CloseQuestBoard()
    {
        questboardScripts[isLastBoardNumberOne ? 0 : 1].CloseQuestBoard();
    }
}
