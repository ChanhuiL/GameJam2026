using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RandomEvent
{
    public string eventName;
    public string eventDescription;
    public int[,] DecisionResult;

    public RandomEvent(string eventName, string eventDescription, int[,] DecisionResult)
    {
        this.eventName = eventName;
        this.eventDescription = eventDescription;
        this.DecisionResult = DecisionResult;
    }
}

public class GameHandlerScript : MonoBehaviour
{
    private int money = 5;
    private int approvalA = 5;
    private int approvalB = 5;
    
    private List<RandomEvent> randomEvents = new List<RandomEvent>()
    {
        new RandomEvent("랜덤 이벤트1", "랜덤 이벤트 이다", new int[,] { {1, 0, 0}, {-1, 0, 0} }),
        new RandomEvent("랜덤 이벤트2", "랜덤 이벤트 2!! 이다", new int[,] { {1, 1, 1}, {-1, -1, -1} }),
        new RandomEvent("랜덤 이벤트3", "랜덤 이벤트 3!!!!!!! 이다", new int[,] { {3, -3, 0}, {3, 0, -3} }),
    };
    private RandomEvent currentRandomEvent;
    
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI approvalAText;
    public TextMeshProUGUI approvalBText;
    public GameObject randomEventObject;
    public bool hasRandomEvent = false;
    
    public void DecisionMade(int decisionIndex)
    {
        if (!hasRandomEvent) return;
        this.money += currentRandomEvent.DecisionResult[decisionIndex, 0];
        this.approvalA += currentRandomEvent.DecisionResult[decisionIndex, 1];
        this.approvalB += currentRandomEvent.DecisionResult[decisionIndex, 2];
        
        randomEventObject.GetComponent<Animator>().SetBool("RandomEventDisplay", false);
        hasRandomEvent = false;
    }
    
    // Update is called once per frame
    void Update()
    {
        moneyText.text = money.ToString();
        approvalAText.text = approvalA.ToString();
        approvalBText.text = approvalB.ToString();
    }

    public void NewRandomEvent()
    {
        if (hasRandomEvent) return;
        currentRandomEvent = randomEvents[Random.Range(0, randomEvents.Count)];
        randomEventObject.GetComponent<Animator>().SetBool("RandomEventDisplay", true);
        randomEventObject.GetComponent<RandomEventScript>().SetRandomEvent(currentRandomEvent);
        hasRandomEvent = true;
    }
}
