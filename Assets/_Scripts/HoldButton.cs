using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using TMPro;

public class HoldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public UnityEvent OnPress;
    public UnityEvent OnRelease;

    [Header("Visuals")]
    public float tweenDuration = 0.1f;
    public Color activeColor;
    public Color defaultColor;

    [Header("References")]
    public CanvasGroup group;
    public Image background;
    public Image icon;
    public TextMeshProUGUI text;
    public MovementButtons movementButtons;

    private void Awake()
    {
        DefaultVisuals();
    }
    public void ActiveVisuals()
    {
        if (icon != null)
        {
            icon.DOColor(activeColor, tweenDuration);
        }
        if (text != null)
        {
            text.DOColor(activeColor, tweenDuration);
        }

        group.DOFade(1, tweenDuration);
        background.rectTransform.DOScale(Vector3.one * 1.1f, tweenDuration).SetEase(Ease.OutFlash);
    }
    public void DefaultVisuals()
    {
        if (icon != null)
        {
            icon.DOColor(defaultColor, tweenDuration);
        }
        if (text!= null)
        {
            text.DOColor(defaultColor, tweenDuration);
        }
        group.DOFade(0.5f, tweenDuration);
        background.rectTransform.DOScale(Vector3.one, tweenDuration).SetEase(Ease.OutFlash);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (movementButtons.currentButton == null)
        {
            ActiveVisuals();
            OnPress?.Invoke();
            movementButtons.SetBusy(this);
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (movementButtons.currentButton == this)
        {
            DefaultVisuals();
            OnRelease?.Invoke();
            movementButtons.SetAvailable();
        }
    }

}
