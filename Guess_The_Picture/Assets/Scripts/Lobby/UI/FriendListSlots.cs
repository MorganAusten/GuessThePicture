using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FriendListSlots : MonoBehaviour
{
    [SerializeField] TMP_Text userName ;
    [SerializeField] Button addFriend ;

    bool canAdd;
    [SerializeField] Button AddButton;

    public void Init(string _username, bool _isInGame)
    {
        userName.text = _username;
        canAdd =  _isInGame;
    }

    void AddFriend()
    {

    }
}
