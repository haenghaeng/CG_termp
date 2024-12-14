using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// P키를 눌러 마우스 커서의 위치를 고정시키고, 보이지 않게 합니다. 다시 누르면 원래대로 돌아갑니다.
/// </summary>
public class MouseEvent : MonoBehaviour
{
    private void Update()
    {
        HideRevealCursor();
    }

    void HideRevealCursor() 
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            Cursor.visible = !Cursor.visible;
            if (Cursor.visible)
                Cursor.lockState = CursorLockMode.None;
            else
                Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
