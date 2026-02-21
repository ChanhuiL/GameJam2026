using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RandomEventScript : MonoBehaviour
{
    static float[]           rotations = new float[] { 0f * Mathf.Deg2Rad, 0f * Mathf.Deg2Rad, 0f * Mathf.Deg2Rad, -45f * Mathf.Deg2Rad, 45f * Mathf.Deg2Rad };
    public Sprite[]          backgroundSprites;
    public GameObject        backgroundObject;
    public Image             backgroundImage;
    public TextMeshProUGUI   randomEventName;
    public TextMeshProUGUI   randomEventDescription;
    public TextMeshProUGUI[] DecisionTexts;
    Button[] buttons;

    private void Awake()
    {
        buttons = GetComponentsInChildren<Button>();
        backgroundImage = backgroundObject.GetComponentInChildren<Image>();
    }

    public void SetRandomEvent(Quest randomEvent)
    {
        backgroundObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotations[Random.Range(0, rotations.Length)]));
        //backgroundImage.sprite = backgroundSprites[Random.Range(0, backgroundSprites.Length)];
        randomEventName.text = randomEvent.QuestName;
        randomEventDescription.text = randomEvent.QuestDialog;

        for (int i = 0; i < DecisionTexts.GetLength(0); i++)
        {
            DecisionTexts[i].text = randomEvent.selections[i].displayName;
            buttons[i].onClick.AddListener(randomEvent.selections[i].Select);
        }
    }
}
