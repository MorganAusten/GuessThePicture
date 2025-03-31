using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using SLobbyM = Singleton<LobbyManager>;

public class LobbyMenu : Menu
{
    #region Var
    public Action<Menu> onLobbyFirstOpen;

    [SerializeField] Button addButton;
    [SerializeField] LobbySlots[] playersSlots;
    [SerializeField] Dictionary<int, LobbySlots> playersSlotsDictionnary = new Dictionary<int, LobbySlots>();


    public LobbySlots[] PlayersSlots { get { return playersSlots; } private set { playersSlots = value; } }
    public Dictionary<int, LobbySlots> PlayersSlotsDictionnary { get { return playersSlotsDictionnary; } private set { playersSlotsDictionnary = value; } }
    #endregion Var

    #region Methods
    protected override void Awake()
    {
        onLobbyFirstOpen += (_a) => SLobbyM.Instance.AddLocalPlayerToUI();
        onOpen += onLobbyFirstOpen;
        base.Awake();
    }
    #endregion Methods
}
