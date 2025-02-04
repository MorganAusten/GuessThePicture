using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiButton : MenuButton
{
    #region Methods
    public override void SetMenuToOpen()
    {
        base.SetMenuToOpen();
        menuToOpen = mainUI.LobbyMenu;
    }
    #endregion Methods
}
