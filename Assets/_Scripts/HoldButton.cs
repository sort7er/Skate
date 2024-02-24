using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HoldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UnityEvent OnPress;
    public UnityEvent OnRelease;

    public CanvasGroup group;
    public Image icon;

    public Color activeColor;
    public Color inActiveColor;

    private float defaultAlpha = 0.5f;
    private float activeAlpha = 1;

    private void Awake()
    {
        InActive();
    }

    public void Active()
    {
        OnPress?.Invoke();
        icon.color = activeColor;
        group.alpha = activeAlpha;
    }
    public void InActive()
    {
        OnRelease?.Invoke();
        icon.color = inActiveColor;
        group.alpha = defaultAlpha;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Active();
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        InActive();
    }

}
