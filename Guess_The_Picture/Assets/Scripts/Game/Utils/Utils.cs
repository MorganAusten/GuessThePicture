using UnityEngine.SceneManagement;
using UnityEngine;
using System.IO;

namespace Utils
{
    #region ThemeUtils
    public static class ThemeU
    {
        #region vars
        static string filmAndSerieEasyString = "Film&Serie/Facile";
        static string filmAndSerieMediumString = "Film&Serie/Medium";
        static string filmAndSerieHardString = "Film&Serie/Hard";
        static string choosenTheme = "";
        #endregion vars


        public static string FilmAndSerieEasyString => filmAndSerieEasyString;
        public static string FilmAndSerieMediumString => filmAndSerieMediumString;
        public static string FilmAndSerieHardString => filmAndSerieHardString;

        public static string ChoosenTheme { get => choosenTheme; set => choosenTheme = value; }
        public static string ActualGameTheme => "GameImages/" + ChoosenTheme;

        public static void GoToMainScene()
        {
            SceneManager.LoadScene("MainMenuScene");
        }

        public static string GetDifficulty()
        {
            if (ChoosenTheme == filmAndSerieEasyString)
                //Debug.LogWarning(" GetDifficulty::ThemeU => Easy Mode");
                return "/Easy";
            else if (ChoosenTheme == filmAndSerieMediumString)
                //Debug.LogWarning(" GetDifficulty::ThemeU => Medium Mode");
                return "/Medium";
            else
                //Debug.LogWarning(" GetDifficulty::ThemeU => Hard Mode");
                return "/Hard";
        }
    }



    #endregion ThemeUtils
}
