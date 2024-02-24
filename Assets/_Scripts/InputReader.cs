using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputReader : MonoBehaviour
{
    public HoldButton currentButton;
    private void Update()
    {
        if(Input.touchCount > 0)
        {
            CheckTouch(Input.GetTouch(0));
        }
    }

    private void CheckTouch(Touch touch)
    {
        //if(touch.phase == TouchPhase.Began)
        //{
        //    if(EventSystem.current.IsPointerOverGameObject(currentButton.gameObject))
        //    Ray ray = Camera.main.ScreenPointToRay(touch.position);
        //    RaycastHit hit;

        //    if(Physics.Raycast(ray, out hit))
        //    {
        //        if(hit.)
        //    }
        //}
        //if(touch.phase == TouchPhase.Ended)
        //{
        //    Release(touch.position);
        //}
    }
}
