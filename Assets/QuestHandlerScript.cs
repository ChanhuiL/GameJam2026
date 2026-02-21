using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestHandlerScript : MonoBehaviour
{
    public Transform QuestNodeParent;
    public GameObject QuestNodePrefab;
    public MapCameraMovement mcm;
    private List<Vector2> QuestNodePositions = new List<Vector2>()
    {
        new Vector2(0, 0),
        new Vector2(3, 0),
        new Vector2(0, 3),
    };

    private void Awake()
    {
        foreach (Vector2 pos in QuestNodePositions)
        {
            var tmp = Instantiate(QuestNodePrefab, QuestNodeParent);
            tmp.transform.position = pos;
            tmp.GetComponent<QuestNode>().mcm = mcm;
        }
    }
}
