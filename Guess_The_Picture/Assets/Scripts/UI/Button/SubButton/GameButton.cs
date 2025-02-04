using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameButton : MenuButton
{
    protected override void OnClick()
    {
        if (/*InputField*/  this)
            Debug.Log("ok");
    }
}
