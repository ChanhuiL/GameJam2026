using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class RandomEventScript : MonoBehaviour
{
    static float[]           rotations = new float[] { 0f * Mathf.Deg2Rad, 0f * Mathf.Deg2Rad, 0f * Mathf.Deg2Rad, -50f * Mathf.Deg2Rad, 50f * Mathf.Deg2Rad };
    public Sprite[]          backgroundSprites;
    public GameObject[]      backgroundObjects;

    private Image[]          backgroundImage;
    public TextMeshProUGUI   randomEventName;
    public TextMeshProUGUI   randomEventDescription;
    public TextMeshProUGUI[] decisionTexts;
    Button[]                 buttons;

    Animator                 animator;
    
    private GameHandlerScript gameHandler;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        buttons = GetComponentsInChildren<Button>();

        backgroundImage = new Image[backgroundObjects.Length];
        for(int i = 0; i < backgroundObjects.Length; i++)
        {
            backgroundImage[i] = backgroundObjects[i].GetComponent<Image>();
        }
        
        
    }

    private void Start()
    {
        gameHandler = GameHandlerScript.Instance;
    }

    public void Update()
    {
    }

    public void SetRandomEvent(Quest randomEvent)
    {
        animator.SetBool("RandomEventDisplay", true);

        Quaternion quat = Quaternion.Euler(new Vector3(0, 0, rotations[Random.Range(0, rotations.Length)]));

        var sprite = backgroundSprites[Random.Range(0, backgroundSprites.Length)];

        for (int i = 0; i < backgroundObjects.Length; ++i)
        {
            backgroundObjects[i].transform.rotation = quat;
            backgroundImage[i].sprite = sprite;
        }

        randomEventName.text = randomEvent.QuestName;
        randomEventDescription.text = randomEvent.QuestDialog;
        for (int i = 0; i < decisionTexts.GetLength(0); i++)
        {
            decisionTexts[i].text = randomEvent.selections[i].displayName;
            buttons[i].onClick.AddListener(randomEvent.selections[i].Select);
        }
        CancelInvoke();
    }
    
    public void ShowAftermath(Quest quest, int idx)
    {
        //Effect
        
        backgroundObjects[0].SetActive(true);
        backgroundObjects[1].SetActive(false);
        backgroundObjects[0].GetComponentInChildren<TextMeshProUGUI>().text = quest.Get_AftermathDialog(idx);

        Invoke("CloseRandomEvent", 2f);
    }

    public void DisableAftermath()
    {
        backgroundObjects[0].SetActive(false);
        backgroundObjects[1].SetActive(true);
        CancelInvoke();
    }

    public void CloseQuestBoard()
    {
        animator.SetBool("RandomEventDisplay", false);
        gameHandler.UnfocusCamera();
    }
}
