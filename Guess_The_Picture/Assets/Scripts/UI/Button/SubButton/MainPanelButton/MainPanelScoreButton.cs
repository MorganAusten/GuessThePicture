using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPanelScoreButton : MenuButton
{
    #region Methods
    public override void SetMenuToOpen()
    {
        base.SetMenuToOpen();
        menuToOpen = mainUI.ScoreMenu;
    }
    #endregion Methods
}
