using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoloButton : MenuButton
{
    #region Methods
    public override void SetMenuToOpen()
    {
        base.SetMenuToOpen();
        menuToOpen = mainUI.ThemeMenu;
    }
    #endregion Methods
}
