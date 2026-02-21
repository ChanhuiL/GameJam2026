using UnityEngine;
using System.Collections.Generic;
using System;

public class Event_Manager : MonoBehaviour
{
    #region Singleton Declare
    public static Event_Manager Instance { get; private set; }

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

    Dictionary<string, Action> eventMap = new Dictionary<string, Action>();

    void Initialize()
    {
        eventMap.Add("TEST2", Test);
        eventMap.Add("TEST", () => Debug.Log("TEST Event"));
    }

    public void ExcuteEvent(string tag)
    {
        Action act;
        if (eventMap.TryGetValue(tag, out act))
        {
            act.Invoke();
        }
    }

    void Test()
    {
        Debug.Log("TEST EVENT2");
    }
}