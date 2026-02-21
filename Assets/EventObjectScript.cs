using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventObjectScript : MonoBehaviour, IPointerClickHandler
{
    public MapCameraMovement mcm;
    public Quest quest;

    public void OnPointerClick(PointerEventData eventData)
    {
        GameHandlerScript.Instance.OpenQuest(quest);
        mcm.FocusCameraToHere(transform.position);
    }
}
