using GooglePlayGames;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;
using UIButton = UnityEngine.UI.Button;
using UIImage = UnityEngine.UI.Image;
using SGoogleM = Singleton<GoogleManager>;

public class LobbyManager : Singleton<LobbyManager>
{
    #region Var

    public event Action<int> OnLobbySlotRemove; 
    #region Friend
    [SerializeField] UIButton addFriendButton;
    [SerializeField] GameObject addFriendPanel;
    [SerializeField] ScrollView addFriendScrollView;
    [SerializeField] FriendListSlots friendListSlotItem;
    [SerializeField] Transform friendListContent;
    #endregion Friend
    #region Lobby
    [SerializeField] UIImage lobbySlotPanel; 
    [SerializeField] LobbySlots LobbySlotItem;
    [SerializeField] List<LobbySlots> playersSlots;
    [SerializeField] LobbyMenu lobbyMenu;
    #endregion Lobby

    int totalPlayers = 0;

    #endregion Var

    [SerializeField, HideInInspector] ILocalUser localUser;

    #region Properties
    public int TotalPlayers { get =>totalPlayers;  set => totalPlayers = value; }
    public LobbyMenu LobbyMenu => lobbyMenu;
    public ILocalUser LocalUser { get { return localUser; } set { localUser = value; } }
    public List<LobbySlots> PlayersSlots { get { return playersSlots; } private set { playersSlots = value; } }
    #endregion Properties

#pragma warning disable 0114
    private void Awake()
    {
        base.Awake();
        addFriendPanel.SetActive(false);
        addFriendButton.onClick.AddListener(OpenAddFriendMenu);
        addFriendButton.onClick.AddListener(AddLobbySlot);
    }
#pragma warning restore 0114

    #region LobbyFriendList
    public void AddLocalPlayerToUI()
    {
        if (totalPlayers == 0)
            if (PlayGamesPlatform.Instance.localUser.authenticated)
            {
                //AddLobbySlot(PlayGamesPlatform.Instance.localUser.userName);
                AddLobbySlot();
            }
            else
            {
                //AddLobbySlot("Offline Name");
                AddLobbySlot();
            }
        else
        {
            lobbyMenu.onOpen -= lobbyMenu.onLobbyFirstOpen;
            Debug.Log("ADDLOCAL DISAPEARED ");
        }
    }

    //TODO Mettre _name en parametre
    void AddLobbySlot()
    {
        LobbySlots _lobbySlot = Instantiate<LobbySlots>(LobbySlotItem, lobbySlotPanel.transform);

        playersSlots.Add(_lobbySlot);
        _lobbySlot.InitLobbySlot("Guest "+ totalPlayers,totalPlayers);
        //_lobbySlot.InitLobbySlot(_name,totalPlayers);
        totalPlayers++;
    }

    public void RemoveLobbySlot(int _index)
    {
        playersSlots.RemoveAt(_index);
        totalPlayers--;
        OnLobbySlotRemove?.Invoke(_index);
    }

    #endregion LobbyFriendList 
    async public void OpenAddFriendMenu()
    {
        addFriendPanel.SetActive(true);
        await SGoogleM.Instance.LoadLocalUserFriends();
        List<IUserProfile> _friendList = SGoogleM.Instance.FriendList;
        if (_friendList == null)
        {
            Debug.LogError("La liste n'a pas pu être chargée");
            return;
        }
        if (_friendList.Count != 0)
            for (int i = 0; i < _friendList.Count; i++)
            {
                FriendListSlots _friendListSlot = Instantiate(friendListSlotItem, friendListContent);
                _friendListSlot.Init(_friendList[i].userName, true);
            }
        else
            for (int i = 0; i < 8; i++)
            {
                FriendListSlots _friendSlot = Instantiate(friendListSlotItem, friendListContent);
                _friendSlot.Init($"User n°{i}", true);
            }
    }
}
