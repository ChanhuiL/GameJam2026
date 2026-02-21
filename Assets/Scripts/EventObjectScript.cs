using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventObjectScript : MonoBehaviour, IPointerClickHandler
{
    public MapCameraMovement mcm;
    public Quest quest;
    
    public bool isActivated = false;

    public void SetCameraTarget(MapCameraMovement _camera)
    {
        mcm = _camera;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isActivated) return;
        GameHandlerScript.Instance.currentNode = this.GetComponent<EventObjectScript>();
        GameHandlerScript.Instance.OpenQuest(quest);
        mcm.FocusCameraToHere(transform.position);
    }

    public void Update()
    {
        if (isActivated)
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
        else
            GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0.5f);
    }

    public void Activate()
    {
        isActivated = true;
    }

    public void Solved()
    {
        isActivated = false;
        // quest reset
    }
}
