using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventObjectScript : MonoBehaviour, IPointerClickHandler
{
    public Animator animator;
    public GameHandlerScript gameHandler;

    public void OnPointerClick(PointerEventData eventData)
    {
        gameHandler.NewRandomEvent();
        animator.SetBool("RandomEventDisplay", true);
    }
}
