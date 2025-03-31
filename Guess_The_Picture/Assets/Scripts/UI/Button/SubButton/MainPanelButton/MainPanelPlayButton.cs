using UnityEngine;

public class MainPanelPlayButton : MenuButton
{
    #region Methods
    public override void SetMenuToOpen()
    {
        //Debug.Log(ToString());
        base.SetMenuToOpen();
        menuToOpen = mainUI.ThemeMenu;
    }
    #endregion Methods
}
