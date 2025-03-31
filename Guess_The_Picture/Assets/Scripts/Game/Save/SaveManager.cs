using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class ScoreSave
{
    public int score;
    public int medalIndex;
    public ScoreSave(int _score, int _medalIndex)
    {
        score =_score;
        medalIndex = _medalIndex;
    }
}


public class SaveManager : MonoBehaviour
{
    #region vars
    static string persistentPath = Application.persistentDataPath;
    static string saveString = "/Save.dat";
    static string scoreString = "/Score";
    #endregion vars

    /// <summary>
    /// <param name="_mode"> _mode : Difficulty Mode to open the directory for.</param>
    /// <param name="_score"> score : Score to save.</param>
    /// </summary>
    public static void SaveScoreAndMedal(string _mode, int _score, int _medalIndex)
    {
        FileStream _stream;
        ScoreSave _scoreSave = new ScoreSave(_score, _medalIndex);
        BinaryFormatter _binaryFormatter = new BinaryFormatter();
        string _persistentPathToScore = persistentPath + scoreString;
        string _persistentPathToDifficulty = _persistentPathToScore + _mode;
        string _savePath = _persistentPathToDifficulty + saveString;

        // if directory doesn't exists, creates it
        if (!Directory.Exists(_persistentPathToScore))
        {
            Directory.CreateDirectory(_persistentPathToScore);
            //Debug.LogWarning($"  SaveScore::SaveManager =>{_persistentPathToScore} directory created !");
        }

        if (!Directory.Exists(_persistentPathToDifficulty))
        {
            Directory.CreateDirectory(_persistentPathToDifficulty);
            //Debug.LogWarning($"  SaveScore::SaveManager =>{_persistentPathToDifficulty} directory created !");
        }

        if (File.Exists(_savePath))
            _stream = new FileStream(_savePath, FileMode.Open);
        else
            _stream = new FileStream(_savePath, FileMode.Create);

        _binaryFormatter.Serialize(_stream, _scoreSave);
        _stream.Close();
    }

    public static int LoadScore(string _mode)
    {
        string _fileToLoad = persistentPath + scoreString + _mode + saveString;
        if (File.Exists(_fileToLoad))
        {
            BinaryFormatter _binaryFormatter = new BinaryFormatter();
            FileStream _stream = new FileStream(_fileToLoad, FileMode.Open);

            ScoreSave _scoreSave = _binaryFormatter.Deserialize(_stream) as ScoreSave;
            _stream.Close();
            return _scoreSave.score;
        }
        else
        {
            //Debug.LogError($"LoadScore::SaveManager => {_fileToLoad} doesn't exist.");
            return 0;
        }
    }

    public static int LoadMedal(string _mode)
    {
        string _fileToLoad = persistentPath + scoreString + _mode + saveString;
        if (File.Exists(_fileToLoad))
        {
            BinaryFormatter _binaryFormatter = new BinaryFormatter();
            FileStream _stream = new FileStream(_fileToLoad, FileMode.Open);

            ScoreSave _medalSave = _binaryFormatter.Deserialize(_stream) as ScoreSave;
            _stream.Close();
            //Debug.LogWarning($"{_mode} : {_medalSave.medalIndex}");
            return _medalSave.medalIndex;
        }
        else
        {
            //Debug.LogError($"LoadScore::SaveManager => {_fileToLoad} doesn't exist.");
            return -1;
        }
    }
}
