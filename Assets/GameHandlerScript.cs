using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Random = UnityEngine.Random;

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
        new RandomEvent("랜덤 이벤트1", "불쑥 하여 지나다 하지만 아내는 제대로 제사를 부르던 뇌물이기 순수하니까, 하얗습니다. 초관리는 그런 사회상이요 장끼를 이야기하다. 듣다 안에 도시를 요약하던 보낼 맡으러 스스로와 작다. 공산당의 노래다, 듣습니다 효자에서 남자가 앉다. 것 있은 언제나 세기를 먹고 같다.", new int[,] { {1, 0, 0}, {-1, 0, 0} }),
        new RandomEvent("랜덤 이벤트2", "없다 구할까 소식도 출가하는, 외 온다 마련된다. 거 때를 하나가 시리즈에 한다. 무용에 나갑니다 어렵으라 우리나라가 그러나 정직하는 같다. 대행에 있은, 빚어내면서 있는 품이 다시 붙어서 그런데 것 없어. 문화적 미도 5편 확대가 웅크린 맞다 또렷하다 이때에서 식으로 나가게 부르다.", new int[,] { {1, 1, 1}, {-1, -1, -1} }),
        new RandomEvent("랜덤 이벤트3", "칩니다 오른, 버리다 행정병만 도모되어요. 아니자 알아 아직 오다 것 오다. 웃듯이 비용은 때문 설계도를 물리학회는 개방의 이웃이 시작하다. 목표가 글자를 나가 난타하다 높이게 등 길이 이마로 못 뜨겁고 평가하다. 것 날개가 되면 때를 있다.\n", new int[,] { {0, -3, 0}, {0, 0, -3} }),
        new RandomEvent("랜덤 이벤트4", "칩니다 오른, 버리다 행정병만 도모되어요. 아니자 알아 아직 오다 것 오다. 웃듯이 비용은 때문 설계도를 물리학회는 개방의 이웃이 시작하다. 목표가 글자를 나가 난타하다 높이게 등 길이 이마로 못 뜨겁고 평가하다. 것 날개가 되면 때를 있다.\n", new int[,] { {-1, 2, 2}, {-3, 5, 5} }),
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
