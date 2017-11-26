using UnityEngine;
using System.Collections;

public class KeyboardInput : MonoBehaviour {

    private static bool key_ctrl;
    public static bool Ctrl
    {
        get { return key_ctrl; }
    }

    private KeyCode keyCtrl;

    private void Update()
    {
        keyCtrl = KeyCode.LeftControl;

        key_ctrl = Input.GetKey(keyCtrl);
    }
}
