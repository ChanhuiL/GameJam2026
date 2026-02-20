using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TestTools;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class RandomEvent
{
    public string eventName;
    public string eventDescription;
    public string[] DecisionDescription;
    public int[,] DecisionResult;

    public RandomEvent(string eventName, string eventDescription, string[] DecisionDescription, int[,] DecisionResult)
    {
        this.eventName = eventName;
        this.eventDescription = eventDescription;
        this.DecisionDescription = DecisionDescription;
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
        new RandomEvent("랜덤 이벤트1", "", new string[] {"한다", "안한다"} ,new int[,] { {1, 0, 0}, {-1, 0, 0} }),
        new RandomEvent("랜덤 이벤트2", "", new string[] {"A를 한다", "B를 한다"} ,new int[,] { {1, 1, 1}, {-1, -1, -1} }),
        new RandomEvent("랜덤 이벤트3", "", new string[] {"믿는다", "믿지 않는다"} ,new int[,] { {0, -3, 0}, {0, 0, -3} }),
        new RandomEvent("랜덤 이벤트4", "", new string[] {"AAA", "BBB"} ,new int[,] { {-1, 1, 1}, {-3, 2, 2} }),
    };
    private RandomEvent currentRandomEvent;
    
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI approvalAText;
    public TextMeshProUGUI approvalBText;
    public Image moneyGauge;
    public Image approvalAGauge;
    public Image approvalBGauge;
    private float currentMoneyGaugeValue = 0;
    private float currentApprovalAGaugeValue = 0;
    private float currentApprovalBGaugeValue = 0;
    public GameObject randomEventObject;
    public bool hasRandomEvent = false;

    public MapCameraMovement mcm;
    
    public void DecisionMade(int decisionIndex)
    {
        if (!hasRandomEvent) return;
        this.money += currentRandomEvent.DecisionResult[decisionIndex, 0];
        this.approvalA += currentRandomEvent.DecisionResult[decisionIndex, 1];
        this.approvalB += currentRandomEvent.DecisionResult[decisionIndex, 2];
        
        randomEventObject.GetComponent<Animator>().SetBool("RandomEventDisplay", false);
        hasRandomEvent = false;
        mcm.UnfocusCamera();
    }
    
    // Update is called once per frame
    void Update()
    {
        moneyText.text = money.ToString();
        approvalAText.text = approvalA.ToString();
        approvalBText.text = approvalB.ToString();
        
        var targetMoneyGaugeValue = Mathf.Clamp(Mathf.InverseLerp(0, 10, money), 0, 1);
        var targetApprovalAGaugeValue = Mathf.Clamp(Mathf.InverseLerp(0, 10, approvalA), 0, 1);
        var targetApprovalBGaugeValue = Mathf.Clamp(Mathf.InverseLerp(0, 10, approvalB), 0, 1);
        currentMoneyGaugeValue = Mathf.Lerp(currentMoneyGaugeValue, targetMoneyGaugeValue, 0.05f);
        currentApprovalAGaugeValue = Mathf.Lerp(currentApprovalAGaugeValue, targetApprovalAGaugeValue, 0.05f);
        currentApprovalBGaugeValue = Mathf.Lerp(currentApprovalBGaugeValue, targetApprovalBGaugeValue, 0.05f);

        moneyGauge.fillAmount = currentMoneyGaugeValue;
        approvalAGauge.fillAmount = currentApprovalAGaugeValue;
        approvalBGauge.fillAmount = currentApprovalBGaugeValue;
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
