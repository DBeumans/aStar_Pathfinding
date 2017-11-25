using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseBehaviour : MonoBehaviour {

    private static bool mouse_left;
    public static bool MouseLeft { get { return mouse_left; } }

    private static bool mouse_right;
    public static bool MouseRight { get { return mouse_right; } }

    private KeyCode mouseL;
    private KeyCode mouseR;

    private void Update()
    {
        mouseL = KeyCode.Mouse0;
        mouseR = KeyCode.Mouse1;

        mouse_left = Input.GetKeyDown(mouseL);
        mouse_right = Input.GetKeyDown(mouseR);
        
    }
}
