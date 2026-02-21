using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameHandlerScript : MonoBehaviour
{
    #region Singleton Declare
    public static GameHandlerScript Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        Initialize();
    }
    #endregion

    public enum StatType { STAT_MONEY, STAT_A, STAT_B, STAT_END };

    [Header("Stat Gauge")]
    private int[]            statValues     = new int[(int)StatType.STAT_END];
    private float[]          curGaugeValues = new float[(int)StatType.STAT_END];
    public TextMeshProUGUI[] gaugeTexts     = new TextMeshProUGUI[(int)(StatType.STAT_END)];
    public Image[]           gaugeImages    = new Image[(int)(StatType.STAT_END)];
    public float             gaugeDrag      = 0.05f;

    bool                     isLastBoardNumberOne = false;
    public GameObject[]      questboardObjects = new GameObject[2];
    private Animator[]       questboardAnimators = new Animator[2];

    public MapCameraMovement mcm;
    public AudioManager audioManager;
    public TransitionManager transitionManager;
    
    public void DecisionMade(StatType[] statTypes, int[] amounts)
    {
        for (int i = 0; i < statTypes.Length; i++)
            statValues[i] += amounts[i];

        mcm.UnfocusCamera();
    }
    
    //������ �ѹ� ����
    void Initialize()
    {
        for (int i = 0; i < questboardObjects.Length; ++i)
            questboardAnimators[i] = questboardObjects[i].GetComponent<Animator>();
    }

    void Update()
    {
        // ������ ���� �ε巴�� ����
        for(int i = 0; i < (int)(StatType.STAT_END); ++i)
        {
            gaugeTexts[i].text = statValues[i].ToString();

            var targetGaugeValue = Mathf.Clamp(Mathf.InverseLerp(0, 10, statValues[i]), 0, 1);
            curGaugeValues[i] = Mathf.Lerp(curGaugeValues[i], targetGaugeValue, gaugeDrag);
            gaugeImages[i].fillAmount = curGaugeValues[i];
        }
    }

    public void CloseRandomEvent()
    {
        questboardAnimators[isLastBoardNumberOne ? 0 : 1].SetBool("RandomEventDisplay", false);
    }

    public void NewRandomEvent(Quest interactedEvent)
    {
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

        questboardAnimators[preIdx].SetBool("RandomEventDisplay", false);
        questboardAnimators[postIdx].SetBool("RandomEventDisplay", true);
        questboardObjects[postIdx].transform.SetAsLastSibling();
        questboardObjects[postIdx].GetComponent<RandomEventScript>().SetRandomEvent(interactedEvent);

        isLastBoardNumberOne = !isLastBoardNumberOne;
    }
}
