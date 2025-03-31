using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Utils;
using GooglePlayGames;

//params for the scale animation in DisplayUI
struct EndGameUI
{
    public Transform transform;
    public Vector3 growthScale;
    public Vector3 finalScale;

    public EndGameUI(Transform _transform, Vector3 _growthScale, Vector3 _finalScale)
    {
        transform = _transform;
        growthScale = _growthScale;
        finalScale = _finalScale;
    }
}

public class EndGameLogic : MonoBehaviour
{
    #region vars
    EndGameUI[] endGameUI;

    #region UIS
    [SerializeField] GameObject scorePanel;
    [SerializeField, HideInInspector] Vector3 scorePanelGrowthScale;
    [SerializeField, HideInInspector] Vector3 scorePanelFinalScale;

    [SerializeField] Image medalField;
    [SerializeField, HideInInspector] Vector3 medalGrowthScale;
    [SerializeField, HideInInspector] Vector3 medalFinalScale;

    [SerializeField] TMP_Text nameField;
    [SerializeField, HideInInspector] Vector3 nameGrowthScale;
    [SerializeField, HideInInspector] Vector3 nameFinalScale;

    [SerializeField] TMP_Text scoreField;
    [SerializeField, HideInInspector] Vector3 scoreGrowthScale;
    [SerializeField, HideInInspector] Vector3 scoreFinalScale;

    [SerializeField] TMP_Text newRecordField;
    [SerializeField, HideInInspector] Vector3 newRecordGrowthScale;
    [SerializeField, HideInInspector] Vector3 newRecordFinalScale;

    [SerializeField] Button exitButton;
    [SerializeField, HideInInspector] Vector3 exitGrowthScale;
    [SerializeField, HideInInspector] Vector3 exitFinalScale;

    [SerializeField] Image[] medals;
    #endregion UIS
    [SerializeField, HideInInspector] float defaultGrowthScaleTime = 0.8f;
    [SerializeField, HideInInspector] float growthScaleTime = 0;
    [SerializeField, HideInInspector] float defaultFinalScaleTime = 0.2f;
    [SerializeField, HideInInspector] float finalScaleTime = 0;

    [SerializeField] AudioSource newRecordAudio;
    int boolIndex = 0;
    float score;

    #region medalScores
    float bronzeScore;
    float silverScore;
    float goldScore;
    float diamondScore;
    float rubiScore;
    #endregion medalScores

    bool gameEnded = false;
    #endregion vars

    #region Properties
    public float Score
    {
        get => score;
        set
        {
            score = value;
            scoreField.text = ((int)score).ToString();
            float[] _medalTreshold = { bronzeScore, silverScore, goldScore, diamondScore,rubiScore };
            SetMedalImage(_medalTreshold);
            CheckForNewRecord(SetMedalImage(_medalTreshold));
        }
    }


    public bool GameEnded
    {
        get => gameEnded;
        set
        {
            gameEnded = value;
            SetListsforUpdate();
            gameObject.SetActive(gameEnded);
        }
    }
    #endregion Properties

    #region private
    void Awake()
    {
        exitButton.onClick.AddListener(ThemeU.GoToMainScene);
        exitButton.enabled = false;
    }

    void Start()
    {
        nameField.text =  PlayGamesPlatform.Instance.localUser.userName;
        ResetScaleTimes();
        SetUISScaleToZero();
        SetGrowthAndFinalScale();
        gameObject.SetActive(gameEnded);
    }
    void ResetScaleTimes()
    {
        growthScaleTime = defaultGrowthScaleTime;
        finalScaleTime = defaultFinalScaleTime;
    }

    void SetUISScaleToZero()
    {
        foreach (Image _medal in medals)
            _medal.transform.localScale = Vector3.zero;
        nameField.transform.localScale = Vector3.zero;
        scoreField.transform.localScale = Vector3.zero;
        medalField.transform.localScale = Vector3.zero;
        newRecordField.transform.localScale = Vector3.zero;
        exitButton.transform.localScale = Vector3.zero;
        scorePanel.transform.localScale = Vector3.zero;
    }

    void SetGrowthAndFinalScale()
    {
        scorePanelGrowthScale = new Vector3(1.2f, 0.8f, 0);
        scorePanelFinalScale = new Vector3(1.11f, 0.72f, 0);

        nameGrowthScale = new Vector3(17, 29, 0);
        nameFinalScale = new Vector3(15, 25, 0);

        medalGrowthScale = new Vector3(3.3f, 4.5f, 0);
        medalFinalScale = new Vector3(3, 4, 0);

        scoreGrowthScale = new Vector3(17, 29, 0);
        scoreFinalScale = new Vector3(15, 25, 0);

        newRecordGrowthScale = new Vector3(17, 29, 0);
        newRecordFinalScale = new Vector3(15, 25, 0);

        exitGrowthScale = new Vector3(3.3f, 10.6f, 0);
        exitFinalScale = new Vector3(3, 10, 0);
    }
    void SetListsforUpdate()
    {
        EndGameUI _medalData;
        if (!medalField)
            _medalData = new EndGameUI(null, medalGrowthScale, medalFinalScale);
        else
            _medalData = new EndGameUI(medalField.transform, medalGrowthScale, medalFinalScale);


        EndGameUI[] _EGUIs = {
            new EndGameUI(scorePanel.transform, scorePanelGrowthScale, scorePanelFinalScale),
            new EndGameUI(nameField.transform, nameGrowthScale, nameFinalScale) ,
            new EndGameUI(scoreField.transform, scoreGrowthScale, scoreFinalScale),
            _medalData,
            new EndGameUI(newRecordField.transform, newRecordGrowthScale, newRecordFinalScale),
            new EndGameUI(exitButton.transform, exitGrowthScale, exitFinalScale)
            };
        endGameUI = _EGUIs;
    }

    void Update()
    {
        if (gameEnded)
            ProcessEndGame();
    }

    void ProcessEndGame()
    {
        if (boolIndex != -1)
        {
            if (boolIndex != 4  ||  SaveManager.LoadScore(ThemeU.GetDifficulty()) < score)
            {
                EndGameUI _currentEGUI = endGameUI[boolIndex];
                DisplayUI(_currentEGUI.transform, _currentEGUI.growthScale, _currentEGUI.finalScale);
            }
            else
                boolIndex++;
        }
        else
            exitButton.enabled = true;
    }
    void DisplayUI(Transform _UITransform, Vector3 _growthScale, Vector3 _finalScale)
    {
        if (!_UITransform)
        {
            boolIndex++;
            //Debug.LogWarning("EndGameLogic::DisplayUI => no transform ");
            //Debug.LogWarning("EndGameLogic::DisplayUI => Actual boolIndex = " + boolIndex);
            return;
        }

        if (growthScaleTime > 0)
        {
            growthScaleTime -= Time.deltaTime * 2;
            _UITransform.localScale = Vector3.Lerp(_growthScale, Vector3.zero, growthScaleTime/defaultGrowthScaleTime);
        }
        else if (finalScaleTime > 0)
        {
            finalScaleTime -= Time.deltaTime * 2;
            _UITransform.localScale = Vector3.Lerp(_finalScale, _growthScale, finalScaleTime/defaultFinalScaleTime);
        }
        else
        {
            if (boolIndex == 4)
                newRecordAudio.Play();
            ResetScaleTimes();
            boolIndex = endGameUI.Length > boolIndex + 1 ? boolIndex + 1 : -1;
        }
    }

    #region Score Setter methods
    void CheckForNewRecord(int _medal)
    {
        if (SaveManager.LoadScore(ThemeU.GetDifficulty()) <= score)
        {
            //Debug.LogWarning($"Medal Index to save {_medal}");
            SaveManager.SaveScoreAndMedal(ThemeU.GetDifficulty(), (int)score, _medal);
        }

    }

    int SetMedalImage(float[] _medalTreshold)
    {
        if (medals.Length != _medalTreshold.Length)
        {
            Debug.LogError("EndGameLogic::Score Setter => error with medals or _medaltreshold number of variables in the list");
            return -1;
        }

        //Defines what medal the player won
        Image[] _selectedMedal = { null, medals[0], medals[1], medals[2], medals[3], medals[4] };
        int _index = 0;
        while (_index < _medalTreshold.Length  && score >= _medalTreshold[_index])
            _index++;

        if (_index != 0)
        {
            medalField.sprite = _selectedMedal[_index].sprite;
            return _index;
        }
        else
        {
            medalField = null; /* medal won't appear */
            return -1;
        }
    }
    #endregion Score Setter methods
    #endregion private
    #region public
    public void ComputeMedalsScore(int _rounds, float _baseScorePerRound)
    {
        float _baseScore = _rounds * _baseScorePerRound - _rounds/2;
        float _maxPoints = _baseScore * 2; /*  _baseScorePerRound*2 => max Score per round */

        bronzeScore = 0.5f * _baseScore;
        silverScore = _baseScore;
        goldScore = 0.7f * _maxPoints;
        diamondScore = 0.85f * _maxPoints;
        rubiScore = _maxPoints;
        //Debug.LogError("max score :" + " " + rubiScore);
    }
    #endregion public
}