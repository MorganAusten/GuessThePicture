using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

#pragma warning disable CS0618 // Random Range est obsolète, c'est pas le cas psk Range c'est en float

public class GameLogic : MonoBehaviour
{
    //A Utiliser plus tard
    //public event Action OnAnswerFound;
    //public event Action OnNewRoundBegins;

    [SerializeField] GamePanel gamePanel;
    [SerializeField] int numberOfRounds = 10;
    [SerializeField]  int chrono = 15;

    int score = 0;
    int currentRound = 0;
    string currentCorrectAnswer = "";

    List<Texture2D> imageList;
    Dictionary<Texture2D, string> imageMap;

    public string CurrentCorrectAnswer { get => currentCorrectAnswer; private set => currentCorrectAnswer = value; }
    public int Score { get => score; set => score = value; }
    public int Chrono { get => chrono; set => chrono = value; }


    void Start()
    {
        //Debug.Log(ThemeU.ChoosenTheme);
        imageList = new List<Texture2D>();
        imageMap = new Dictionary<Texture2D, string>();
        SetGamePanel();
        SetImages();
        RoundBegins();
    }

    private void SetGamePanel()
    {
        gamePanel.GameLogic = this;
        gamePanel.SetGamePanelAndGrowingImageTimer(chrono);
    }

    void SetImages()
    {
        Texture2D[] _images;
        if (ThemeU.ActualGameTheme == "GameImages/")
        {
            Debug.Log("[GameLogic::SetImages] => No actual Game theme");
            _images = Resources.LoadAll<Texture2D>("GameImages/Film&Serie");
        }
        else
        {
            //Debug.Log("[GameLogic::SetImages] => actual Game theme");
            //Debug.Log("[GameLogic::SetImages] => " + ThemeU.ActualGameTheme);
            _images = Resources.LoadAll<Texture2D>(ThemeU.ActualGameTheme);
        }
        int _numberOfImages = 0;
        int _maxRange = _images.Length;
        Texture2D _texture;
        while (_numberOfImages < numberOfRounds)
        {
            int _randomNumber = UnityEngine.Random.RandomRange(0, _maxRange);
            _texture = _images[_randomNumber];
            while (imageMap.ContainsKey(_texture))
            {
                _randomNumber = UnityEngine.Random.RandomRange(0, _maxRange);
                _texture = _images[_randomNumber];
            }
            imageList.Add(_texture);
            //Debug.Log(_texture.name);
            imageMap.Add(_texture, _texture.name);
            _numberOfImages++;
        }
        //Debug.Log("[GameLogic::SetImages] => number of images: " + _numberOfImages);
    }

    void RoundBegins()
    {
        //TODO Faire un systeme ou il y a une pause entre la fin d'une manche et le début d'une nouvelle (RoundBegan)

        //Debug.Log("[GameLogic::RoundBegins] => current Round image in List : " + imageList[currentRound].name);
        //Debug.Log("[GameLogic::RoundBegins] => current Answer : " + imageMap[imageList[currentRound]]);
        SetNewImageForTheRound();
        gamePanel.RoundBegan = true;
    }

    private void SetNewImageForTheRound()
    {
        gamePanel.CurrentImage.texture = imageList[currentRound];
        currentCorrectAnswer = imageMap[imageList[currentRound]].ToLower();
    }

    void GameEnds()
    {

    }

    public void RoundEnds()
    {
        currentRound +=1;
        gamePanel.GrowingImage.SetRandomPosition();
        if (currentRound >= numberOfRounds)
            SceneManager.LoadScene("MainMenuScene", LoadSceneMode.Single);
        gamePanel.ResetTimer();
        SetNewImageForTheRound();
        //TODO mettre en place une autre image, reset le crop et le timer.
        //TODO Si y'a plus de round, retour au menu
        //TODO Afficher le nom
    }



    public void CorrectAnswerFound()
    {
        //TODO score en fonction de la vitesse de réponse
        score += 1;
        RoundEnds();
        Debug.Log("[GameLogic::CorrectAnswerFound] => Correct Answer !");
    }

}
