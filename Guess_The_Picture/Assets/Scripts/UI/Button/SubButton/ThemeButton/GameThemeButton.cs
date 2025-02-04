using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;
public class GameThemeButton : MenuButton
{
    protected override void TransitionForNextMenu()
    {
        ThemeU.ChoosenTheme = ThemeU.GameEasyString;
        Debug.Log(ThemeU.ActualGameTheme);
        SceneManager.LoadScene("Test", LoadSceneMode.Single);
    }
}
