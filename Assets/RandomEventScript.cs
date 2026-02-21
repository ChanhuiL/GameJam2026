using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RandomEventScript : MonoBehaviour
{
    public TextMeshProUGUI randomEventName;
    public TextMeshProUGUI randomEventDescription;
    public TextMeshProUGUI[] DecisionTexts;
    Button[] buttons;

    private void Awake()
    {
        buttons = GetComponentsInChildren<Button>();
    }

    public void SetRandomEvent(Quest randomEvent)
    {
        randomEventName.text = randomEvent.QuestName;
        randomEventDescription.text = randomEvent.QuestDialog;

        for (int i = 0; i < DecisionTexts.GetLength(0); i++)
        {
            DecisionTexts[i].text = randomEvent.selections[i].displayName;
            buttons[i].onClick.AddListener(randomEvent.selections[i].Select);
        }
    }
}
