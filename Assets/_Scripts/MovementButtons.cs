using UnityEngine;

public class MovementButtons : MonoBehaviour
{
    public HoldButton currentButton { get; private set; }

    private void Awake()
    {
        SetAvailable();
    }

    public void SetBusy(HoldButton button)
    {
        currentButton = button;
    }
    public void SetAvailable()
    { 
        currentButton = null;
    }

}
