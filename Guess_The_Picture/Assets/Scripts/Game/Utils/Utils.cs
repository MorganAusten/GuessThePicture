using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{

        #region ThemeUtils
    public static class ThemeU
    {
        static string filmAndSerieEasyString = "Film&Serie/Facile";
        static string gameEasyString = "Game/Facile";
        static string animeEasyString = "Anime/Facile";
        static string choosenTheme = "";
        public static string FilmAndSerieEasyString => filmAndSerieEasyString;
        public static string GameEasyString => gameEasyString;
        public static string AnimeEasyString => animeEasyString;

        public static string ChoosenTheme { get => choosenTheme; set => choosenTheme = value; }
        public static string ActualGameTheme => "GameImages/" + ChoosenTheme;
    }
    #endregion ThemeUtils
}
