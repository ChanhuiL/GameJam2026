using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
        Initialize();
    }
    #endregion

    public enum StatType { STAT_MONEY, STAT_FOOD, STAT_POWER, STAT_CONFLICT, STAT_END };

    [Header("UI")]
    private int[]            statValues     = new int[(int)StatType.STAT_END];
    private float[]          curGaugeValues = new float[(int)StatType.STAT_END];
    public TextMeshProUGUI[] gaugeTexts     = new TextMeshProUGUI[(int)(StatType.STAT_END)];
    public Image[]           gaugeImages    = new Image[(int)(StatType.STAT_END)];
    public float             gaugeDrag      = 0.05f;

    private int[]            statInitialValues = new int[]{30, 50, 50, 30};
    private int              statMinimum = 0;
    private int              statMaximum = 100;
    
    public MapCameraMovement mcm;
    public AudioManager audioManager;
    public TransitionManager transitionManager;
    public Event_Pages eventPages;

    public QuestNode currentNode;
    
    private bool hasGameEnded = false;
    
    public void DecisionMade(StatType[] statTypes, int[] amounts)
    {
        for (int i = 0; i < statTypes.Length; i++)
            statValues[i] += amounts[i];
    }


    public void FocusCamera(Vector2 pos)
    {
        mcm.FocusCameraToHere(pos);
    }
    public void UnfocusCamera()
    {
        mcm.UnfocusCamera();
    }

    public void PlayAudioClip(AudioClip clip)
    {
        GetComponent<AudioSource>().PlayOneShot(clip);
    }
    
    void Initialize()
    {
        for (int i = 0; i < (int)(StatType.STAT_END); i++)
            statValues[i] = statInitialValues[i];
    }

    void Update()
    {
        for(int i = 0; i < (int)(StatType.STAT_END); ++i)
        {
            gaugeTexts[i].text = statValues[i].ToString();

            var targetGaugeValue = Mathf.Clamp(Mathf.InverseLerp(statMinimum, statMaximum, statValues[i]), 0, 1);
            curGaugeValues[i] = Mathf.Lerp(curGaugeValues[i], targetGaugeValue, gaugeDrag);
            gaugeImages[i].fillAmount = curGaugeValues[i];
        }

        for (int i = 0; i < (int)(StatType.STAT_END); i++)
        {
            if (statValues[i] < 0 && !hasGameEnded)
            {
                hasGameEnded = true;
                transitionManager.StartFadeOut();
                audioManager.ChangeSceneWithFade("Game Over Scene");
            }
        }
    }

    public void OpenQuest(Quest interactedEvent, QuestNode quest)
    {
        eventPages.OpenQuest(interactedEvent, quest);
    }

    public void CloseQuest()
    {
        eventPages.CloseQuestBoard();
    }
}
