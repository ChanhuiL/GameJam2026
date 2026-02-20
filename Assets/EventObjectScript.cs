using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventObjectScript : MonoBehaviour, IPointerClickHandler
{

    public Camera camera;
    public Animator animator;
    public GameHandlerScript gameHandler;

    public void OnPointerClick(PointerEventData eventData)
    {
        gameHandler.NewRandomEvent();
        animator.SetBool("RandomEventDisplay", true);
        camera.transform.position = transform.position + new Vector3(4, 0, -10) * camera.orthographicSize / 5.0f;
    }
}
