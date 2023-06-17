using UnityEngine;

public static class MouseController
{
    private static bool s_state = false;

    public static void SetVisibility(bool state)
    {
        s_state = state;
        Cursor.lockState = state ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = state;
    }

    public static void SwitchState()
    {
        SetVisibility(!s_state);
    }
}
