using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using IF = TMPro.TMP_InputField;
using TF = TMPro.TMP_Text;

public class GamePanel : MonoBehaviour
{
    #region vars
    [SerializeField] GameLogic gameLogic;
    [SerializeField] GrowingImage growingImage;
    [SerializeField] RawImage currentImage;
    #region InputField
    [SerializeField] IF gameInputField;
    [SerializeField] TF userNameField;
    [SerializeField] TF scoreField;
    [SerializeField] TF scoreIncrementField;
    [SerializeField] TF chronoTimerField;
    #endregion InputField

    #region Audio
    [SerializeField] AudioSource decrementAudio;
    [SerializeField] AudioSource rightAnswerAudio;
    [SerializeField] AudioSource wrongAnswerAudio;
    #endregion Audio


    #region Timer
    float chrono = 0;
    [SerializeField] float answerTimerDefault = 10;
     float answerTimer = 10;
    float chronoTimer = -1;
    float colorTimer = 0.5f;
    #endregion Timer

    #region ScoreIncrement
    float scoreIncrement = 0;
    float baseScoreIncrement = 10;
    #endregion ScoreIncrement

    bool correctAnswer = false;
    bool roundBegan = false;
    bool gameBegan = false;
    bool isAnswering = false;
    #endregion  vars

    #region Properties 
    public GrowingImage GrowingImage { get => growingImage; private set => growingImage = value; }
    public RawImage CurrentImage { get => currentImage; private set => currentImage = value; }
    public GameLogic GameLogic { get => gameLogic; set => gameLogic = value; }
    public float BaseScoreIncrement { get => baseScoreIncrement; set => baseScoreIncrement = value; }
    public float ChronoTimer { get => chronoTimer; private set => chronoTimer = value; }
    public bool RoundBegan { get => roundBegan; private set => roundBegan = value; }
    public bool GameBegan { get => gameBegan; set => gameBegan = value; }
    #endregion Properties 

    private void Awake()
    {
        gameInputField.onSelect.AddListener(StopGame);
        gameInputField.onEndEdit.AddListener(OnValidateAnswer);
    }
    void StopGame(string _str)
    {
        isAnswering = true;
        chronoTimerField.color = Color.yellow;
    }

    private void Start()
    {
        userNameField.text = PlayGamesPlatform.Instance.localUser.userName;
    }

    private void Update()
    {
        if (gameBegan)
            if (roundBegan)
            {

                if (!isAnswering)
                {
                    UpdateTimer();
                    growingImage.CircleGrowthUpdate(chronoTimer);
                }
                else
                    UpdateAnsweringTimer();
            }
            else
            {
                UpdateScoreIncrementColor();
                gameInputField.enabled = false;
                growingImage.TurnCropOpacity(false);
                if (growingImage.TitleAppeared && scoreIncrement > 0)
                    UpdateScoreIncrementAndScore();
            }
    }

    // At The end of the round, when incrementScore decreases. If the answer was correct, score is increasing simultaneously 
    void UpdateScoreIncrementAndScore()
    {
        float _deltaTime = Time.deltaTime * 15;
        int _precedentScoreIncrement = (int)scoreIncrement;
        scoreIncrement -= _deltaTime;
        int _newScoreIncrement = (int)scoreIncrement;
        scoreIncrement = scoreIncrement < 0 ? 0 : scoreIncrement;
        if (_precedentScoreIncrement != _newScoreIncrement)
        {
            decrementAudio.PlayOneShot(decrementAudio.clip);
            scoreIncrementField.text = ((int)scoreIncrement).ToString();
        }
        if (correctAnswer)
            gameLogic.Player.IncrementScore(_deltaTime);

    }

    void UpdateScoreIncrementColor()
    {
        if (colorTimer == 0)
            return;

        colorTimer -= Time.deltaTime /5;
        colorTimer = colorTimer < 0 ? 0 : colorTimer;
        if (correctAnswer)
            scoreIncrementField.color = Color.Lerp(Color.green, scoreIncrementField.color, colorTimer/0.5f);
        else
            scoreIncrementField.color = Color.Lerp(Color.red, scoreIncrementField.color, colorTimer/0.5f);

    }

    public void RoundBegins()
    {
        ResetTimers();
        CancelFloatToIntConversion();
        gameInputField.enabled =true;
        gameInputField.text = "";
        scoreIncrementField.color = Color.grey;
        gameBegan = true;
        correctAnswer = false;
        roundBegan = true;
        growingImage.TitleAppeared = false;
        growingImage.TurnCropOpacity(true);
        growingImage.TurnTitleOpacity(false);
    }

    // ça permet d'avoir un score sans virgule derriere, sachant qu'elle n'est pas affichée dans l'ui, ça peut causer de l'incompréhension sinon (tu gagnes 16 points d'après l'UI,
    // et au final tu en gagnes 17)
    void CancelFloatToIntConversion()
    {
        int _newScore = (int)gameLogic.Player.Score;
        gameLogic.Player.Score = _newScore > gameLogic.Player.Score ? gameLogic.Player.Score : _newScore;
    }

    void RoundEnds()
    {
        if (correctAnswer)
            rightAnswerAudio.Play();
        else
            wrongAnswerAudio.Play();
        roundBegan = false;
        ExitFromGameInputField();
        gameLogic.RoundEnds();
    }


    #region Timer
    void UpdateTimer()
    {
        chronoTimer -= Time.deltaTime;
        chronoTimerField.text = ((int)chronoTimer).ToString();
        DecreasePossibleScoreGains();
        if (chronoTimer <= -1)
            RoundEnds();
    }

    void DecreasePossibleScoreGains()
    {
        scoreIncrement = baseScoreIncrement + ((chronoTimer/chrono) * baseScoreIncrement);
        scoreIncrementField.text = ((int)scoreIncrement).ToString();
    }

    void UpdateAnsweringTimer()
    {
        answerTimer -= Time.deltaTime;
        chronoTimerField.text = ((int)answerTimer).ToString();
        if (answerTimer <= -1)
            RoundEnds();
    }

    //Set Timer for UI and GrowingCircle for the actual game
    public void SetGamePanelAndGrowingImageTimer(float _chrono)
    {
        chrono = _chrono;
        chronoTimerField.text = (_chrono + 1).ToString();
        SetGrowingImage(_chrono);
    }

    public void ResetTimers()
    {
        answerTimer = answerTimerDefault;
        chronoTimerField.text = chrono.ToString();
        chronoTimer = chrono;
        colorTimer = 0.5f;
        growingImage.ResetScaleTime();
    }
    #endregion Timer

    public void SetGrowingImage(float _chrono)
    {
        growingImage.Chrono = _chrono;
    }


    void ExitFromGameInputField()
    {
        chronoTimerField.color = Color.white;
        isAnswering = false;
        EventSystem.current.SetSelectedGameObject(null);
    }

    void OnValidateAnswer(string _answer)
    {
        while (_answer.Length != 0 && _answer[_answer.Length -1] == ' ')
            _answer = _answer.Substring(0, _answer.Length - 1);

        if (_answer.Length != 0)
            foreach (string _a in gameLogic.CurrentCorrectAnswers)
            {
                if (_a.ToLower() == _answer.ToLower())
                {
                    correctAnswer = true;
                    break;
                }
            }
        RoundEnds();
    }
}
