using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class QuestHandlerScript : MonoBehaviour
{
    public Transform QuestNodeParent;
    public GameObject QuestNodePrefab;
    public MapCameraMovement mcm;
    private List<Vector2> QuestNodePositions = new List<Vector2>()
    {
        new Vector2(-26.59f, -4.29f),
        new Vector2(-26.05f, 19.1f),
        new Vector2(-21.96f, 2.78f),
        new Vector2(-20.91f, -15.79f),
        new Vector2(-11.85f, 2.49f),
        new Vector2(-11.52f, -7.92f),
        new Vector2(-6.17f, -19.23f),
        new Vector2(-5.39f, 11.23f),
        new Vector2(2.95f, 0.14f),
        new Vector2(5.4f, -13.14f),
        new Vector2(7.64f, -23.65f),
        new Vector2(9.6f, 16.45f),
        new Vector2(13.59f, -5.15f),
        new Vector2(18.8f, 5.35f),
        new Vector2(20.93f, -15.26f),
        new Vector2(34.18f, 16.36f),
        new Vector2(42.41f, 25.61f),
    };
    private List<EventObjectScript> QuestNodes = new List<EventObjectScript>();
    
    public List<Quest> quests;
    
    public double LastQuestTime = 0;

    private void Awake()
    {
        foreach (Vector2 pos in QuestNodePositions)
        {
            var tmp = Instantiate(QuestNodePrefab, QuestNodeParent);
            tmp.transform.position = pos;
            tmp.GetComponent<EventObjectScript>().mcm = mcm;
            tmp.GetComponent<EventObjectScript>().quest = GetQuest();
            QuestNodes.Add(tmp.GetComponent<EventObjectScript>());
        }
    }

    private Quest GetQuest()
    {
        return quests[Random.Range(0, quests.Count)];
    }

    private void Update()
    {
        if (LastQuestTime + 5 < GetComponent<TimeControllerScript>().GetTime())
        {

            var targetNode = QuestNodes[Random.Range(0, QuestNodes.Count)];
            if (!targetNode.isActivated)
            {
                targetNode.Activate();
                LastQuestTime = GetComponent<TimeControllerScript>().GetTime();
            }
        }
    }
}
