using UnityEngine;
using TMPro;
using GooglePlayGames;

public class PlayerSlot : MonoBehaviour
{
    #region Vars 

    [SerializeField] TMP_Text nameIF;
    [SerializeField] TMP_Text scoreIF;

    [SerializeField] AudioSource incrementAudio;
    [SerializeField] string playerName = " ";
    [SerializeField] float score = 0;
    [SerializeField] bool isPlayer;

    GameLogic gameLogic;

    public GameLogic GameLogic { get { return gameLogic; } set { gameLogic = value; } }
    public float Score { get => score; set { score = value; } }
    public bool IsPlayer => isPlayer;

    #endregion Vars 

    #region methods 
    private void Start()
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated)
            playerName = PlayGamesPlatform.Instance.localUser.userName;
        else
            playerName = "Score";
        nameIF.text = playerName;
    }

    public void IncrementScore(float _increment)
    {
        int _precedentScore = (int)score;
        score += _increment;
        int _newScore = (int)score;
        if (_precedentScore != _newScore)
        {
            incrementAudio.PlayOneShot(incrementAudio.clip);
            scoreIF.text = (_newScore).ToString();
        }
    }
    #endregion methods 
}
