using UnityEngine;

public class MovementButtons : MonoBehaviour
{
    public HoldButton currentButton { get; private set; }

    public void Press(HoldButton button)
    {
        if(currentButton != null)
        {
            currentButton.ButtonDone();
        }
        currentButton = button;
    }
}
