using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public class AnimeThemeButton : MenuButton
{
    protected override void TransitionForNextMenu()
    {
        ThemeU.ChoosenTheme = ThemeU.FilmAndSerieHardString;
        Debug.Log(ThemeU.ChoosenTheme);
        SceneManager.LoadScene("Test", LoadSceneMode.Single);
    }
}
