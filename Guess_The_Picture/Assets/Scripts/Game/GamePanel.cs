using GooglePlayGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using IF = TMPro.TMP_InputField;
using TF = TMPro.TMP_Text;

public class GamePanel : MonoBehaviour
{
    [SerializeField] GameLogic gameLogic;
    [SerializeField] GrowingImage growingImage;
    [SerializeField] RawImage currentImage;
    [SerializeField] IF gameInputField;
    [SerializeField] TF userNameField;
    [SerializeField] TF scoreField;
    [SerializeField] TF chronoTimerField;
    [SerializeField] Button validateAnswerButton;

    float chrono = 0;
    float chronoTimer = 0;
    bool roundBegan = true;

    public GrowingImage GrowingImage { get => growingImage ; set => growingImage = value; }
    public RawImage CurrentImage { get => currentImage; set { currentImage = value; } }
    public IF GameInputField { get => gameInputField; set { gameInputField = value; } }
    public TF ChronoTimerField {  get => chronoTimerField; set {  chronoTimerField = value; } }
    public GameLogic GameLogic { get => gameLogic; set {  gameLogic = value; } }

    public bool RoundBegan { get => roundBegan; set { roundBegan = value; } }
    public float Chrono { get => chrono; set {  chrono = value; } }
    public float ChronoTimer { get => chronoTimer; set {  chronoTimer = value; } }

    private void Awake()
    {
        gameInputField.onEndEdit.AddListener(OnValidateAnswer);
    }

    private void Start()
    {
        scoreField.text = gameLogic.Score.ToString();
        userNameField.text = PlayGamesPlatform.Instance.localUser.userName;    
    }

    private void Update()
    {
        if (roundBegan)
        {
            UpdateTimer();
            growingImage.CircleGrowthUpdate(chronoTimer);
        }
    }

    private void UpdateTimer()
    {
        chronoTimer -= Time.deltaTime;
        chronoTimerField.text = chronoTimer > 10 ? (chronoTimer+1).ToString().Remove(2) : 
                                                    (chronoTimer+1).ToString().Remove(1);
        if (chronoTimer <= 0)
            gameLogic.RoundEnds();
    }

    #region Timer
    //Set Timer for UI and GrowingCircle for the actual game
    public void SetGamePanelAndGrowingImageTimer(float _chrono)
    {
        chrono = _chrono;
        chronoTimerField.text = (_chrono + 1).ToString();
        SetGrowingImage(_chrono);
    }

    public void ResetTimer()
    {
        chronoTimerField.text = chrono.ToString();
        chronoTimer = chrono;
        growingImage.ResetScaleTime();
    }
    #endregion Timer

    public void SetGrowingImage(float _chrono)
    {
        growingImage.Chrono = _chrono ;
    }

    private void OnValidateAnswer(string _answer)
    {
        //Debug.Log("[GamePanel::OnValidateAnswer] => Your answer is: " + _answer);
        //Debug.Log("[GamePanel::OnValidateAnswer] => Answer is: " + gameLogic.CurrentCorrectAnswer);
        if (_answer.ToLower() == gameLogic.CurrentCorrectAnswer)
        {
            //scoreField.text = ++;
            gameLogic.CorrectAnswerFound();
        }
        //TODO Else : wrong answer on UI
    }

}
