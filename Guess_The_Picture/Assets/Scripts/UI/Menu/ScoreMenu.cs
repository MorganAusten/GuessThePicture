using UnityEngine;
using TMPro;
using Image = UnityEngine.UI.Image;

struct MedalsData
{
    public Image medal;
    public string difficulty;

    public MedalsData(Image _medal, string _difficulty)
    {
        medal = _medal;
        difficulty = _difficulty;
    }
}

public class ScoreMenu : Menu
{
    #region vars
    [SerializeField] TMP_Text scoreEasy;
    [SerializeField] TMP_Text scoreMedium;
    [SerializeField] TMP_Text scoreHard;

    [SerializeField] Image medalEasy;
    [SerializeField] Image medalMedium;
    [SerializeField] Image medalHard;

    MedalsData[] medalsDatas;
    [SerializeField] Sprite[] medals;
    #endregion  vars 


    void SetMedalsData()
    {
        MedalsData[] _medalsDatas =
        {
            new MedalsData(medalEasy,"Easy"),
            new MedalsData(medalMedium,"Medium"),
            new MedalsData(medalHard,"Hard"),
        };
        medalsDatas = _medalsDatas;
    }

    void UpdateScoresAndMedals(Menu _a)
    {
        UpdateScores();
        UpdateMedals();
    }

    void UpdateScores()
    {
        scoreEasy.text = SaveManager.LoadScore("/Easy").ToString();
        scoreMedium.text = SaveManager.LoadScore("/Medium").ToString();
        scoreHard.text = SaveManager.LoadScore("/Hard").ToString();
    }

    void UpdateMedals()
    {
        if(medalsDatas == null || medalsDatas.Length == 0)
            SetMedalsData();
        foreach (MedalsData _medalData in medalsDatas)
            LoadMedalByDifficulty(_medalData.medal, _medalData.difficulty);
    }

    void LoadMedalByDifficulty(Image _medal, string _mode)
    {
        int _medalIndex = SaveManager.LoadMedal("/" + _mode);
        if (_medalIndex != -1)
        {
            _medal.gameObject.SetActive(true);
            _medal.sprite = medals[_medalIndex-1];
        }
        else
            _medal.gameObject.SetActive(false);
    }

    protected override void OnOpen(Menu _precedentMenu)
    {
        base.OnOpen(_precedentMenu);
        UpdateScoresAndMedals(_precedentMenu);
    }
}
