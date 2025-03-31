using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Utils;

#pragma warning disable CS0618 // Random Range est obsolète, c'est pas le cas psk Range c'est en float

public class GameLogic : MonoBehaviour
{
    //public event Action OnAnswerFound;

    [SerializeField] EndGameLogic endGameLogic;
    [SerializeField] GamePanel gamePanel;
    [SerializeField] PlayerSlot player;
    [SerializeField] TMP_Text roundText;

    [SerializeField] int numberOfRounds = 10;
    [SerializeField] int chrono = 15;
    [SerializeField] int endRoundChronoDefault = 6;

    int score = 0;
    int currentRound = 0;
    float endRoundChrono = 0;
    float endGameChrono = 0;
    string currentCorrectAnswer = "";
    string[] currentCorrectAnswers;

    List<GameImage> imageList;
    Dictionary<GameImage, string[]> imageMap;

    public PlayerSlot Player { get => player; private set => player = value; }
    public string CurrentCorrectAnswer { get => currentCorrectAnswer; private set => currentCorrectAnswer = value; }
    public string[] CurrentCorrectAnswers { get => currentCorrectAnswers; private set => currentCorrectAnswers = value; }
    public int Score { get => score; private set => score = value; }
    public int Chrono { get => chrono; private set => chrono = value; }

    void Start()
    {
        endGameLogic.ComputeMedalsScore(numberOfRounds, gamePanel.BaseScoreIncrement);
        endRoundChrono = endRoundChronoDefault;
        imageList = new List<GameImage>();
        imageMap = new Dictionary<GameImage, string[]>();

        SetGamePanel();
        SetImages();
        RoundBegins();
        SetNewImageForTheRound();
    }

    void Update()
    {
        if (gamePanel.RoundBegan == false && endGameLogic.GameEnded == false)
            EndChronosUpdate();
    }

    void EndChronosUpdate()
    {
        endRoundChrono -= Time.deltaTime;
        if (endRoundChrono <= 0)
        {
            if (currentRound >= numberOfRounds)
            {
                endGameChrono -= Time.deltaTime;
                if (endGameChrono <= 0)
                {
                    endGameLogic.Score = player.Score;
                    gamePanel.GameBegan = false;
                    endGameLogic.GameEnded = true;
                    return;
                }
            }
            endRoundChrono = endRoundChronoDefault;
            RoundBegins();
            gamePanel.GrowingImage.SetRandomPosition();
            gamePanel.GrowingImage.CircleGrowthUpdate(gamePanel.ChronoTimer);
            SetNewImageForTheRound();
        }
    }


    private void SetGamePanel()
    {
        gamePanel.GameLogic = this;
        gamePanel.SetGamePanelAndGrowingImageTimer(chrono);
    }

    void SetImages()
    {
        GameImage[] _images;
        if (ThemeU.ActualGameTheme == "GamesImages/")
            _images = Resources.LoadAll<GameImage>(ThemeU.FilmAndSerieEasyString);
        else
            _images = Resources.LoadAll<GameImage>(ThemeU.ActualGameTheme);
        SelectRandomImagesByTheme(_images);
    }

    void SelectRandomImagesByTheme(GameImage[] _images)
    {
        int _numberOfImages = 0;
        int _maxRange = _images.Length;
        GameImage _texture;
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
            imageMap.Add(_texture, _texture.Answers);
            _numberOfImages++;
        }
    }

    void RoundBegins()
    {
        gamePanel.RoundBegins();
        roundText.text = $"round : {currentRound+1}/{numberOfRounds}";
    }

    private void SetNewImageForTheRound()
    {
        gamePanel.CurrentImage.texture = imageList[currentRound].Texture;
        currentCorrectAnswers = imageMap[imageList[currentRound]];
        //foreach (string _a in CurrentCorrectAnswers) Debug.LogError(_a);
        gamePanel.GrowingImage.ImageTitle.text = imageList[currentRound].name;
    }

    public void RoundEnds()
    {
        currentRound +=1;
        //TODO Si y'a plus de round, retour au menu
    }

}


