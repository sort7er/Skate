using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HoldButton : MonoBehaviour
{
    public UnityEvent OnPress;
    public UnityEvent OnRelease;

    public CanvasGroup group;
    public Image image;

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
        image.color = activeColor;
        group.alpha = activeAlpha;
    }
    public void InActive()
    {
        OnRelease?.Invoke();
        image.color = inActiveColor;
        group.alpha = defaultAlpha;
    }
}
