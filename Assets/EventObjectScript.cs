using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventObjectScript : MonoBehaviour, IPointerClickHandler
{
    public MapCameraMovement mcm;
    public Quest quest;
    
    private bool isActivated = true;

    public void SetCameraTarget(MapCameraMovement _camera)
    {
        mcm = _camera;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isActivated) return;
        GameHandlerScript.Instance.OpenQuest(quest);
        mcm.FocusCameraToHere(transform.position);
    }
}
