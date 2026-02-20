using TMPro;
using UnityEngine;

public class RandomEventScript : MonoBehaviour
{
    public TextMeshProUGUI randomEventName;
    public TextMeshProUGUI randomEventDescription;
    public TextMeshProUGUI[] DecisionTexts;
    
    public void SetRandomEvent(RandomEvent randomEvent)
    {
        randomEventName.text = randomEvent.eventName;
        randomEventDescription.text = randomEvent.eventDescription;
        for(int i=0;i<randomEvent.DecisionResult.GetLength(0);i++)
        {
            DecisionTexts[i].text = "이거 선택하면\n";
            for (int j = 0; j < randomEvent.DecisionResult.GetLength(1); j++)
            {
                DecisionTexts[i].text += randomEvent.DecisionResult[i, j].ToString() + " ";
            }
        }
    }
}
