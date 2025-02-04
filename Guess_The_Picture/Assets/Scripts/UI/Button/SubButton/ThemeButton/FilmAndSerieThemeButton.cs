using UnityEngine;
using UnityEngine.SceneManagement;
using  Utils;

public class FilmAndSerieThemeButton : MenuButton
{ 
    protected override void TransitionForNextMenu()
    {
        ThemeU.ChoosenTheme = ThemeU.FilmAndSerieEasyString;
        Debug.Log(ThemeU.ChoosenTheme);
        SceneManager.LoadScene("Test", LoadSceneMode.Single);
    }
}
