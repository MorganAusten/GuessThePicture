using UnityEngine;

public class MainPanelExitButton : MenuButton
{
    #region Methods
    protected override void TransitionForNextMenu()
    {
        Application.Quit();
        Debug.Log("Caca");
    }
    #endregion Methods
}
