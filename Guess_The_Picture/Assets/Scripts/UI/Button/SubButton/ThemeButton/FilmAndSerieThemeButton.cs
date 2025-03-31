using UnityEngine;
using UnityEngine.SceneManagement;
using  Utils;

public class FilmAndSerieThemeButton : MenuButton
{ 
    protected override void TransitionForNextMenu()
    {
        ThemeU.ChoosenTheme = ThemeU.FilmAndSerieEasyString;
        SceneManager.LoadScene("Test", LoadSceneMode.Single);
    }
}
