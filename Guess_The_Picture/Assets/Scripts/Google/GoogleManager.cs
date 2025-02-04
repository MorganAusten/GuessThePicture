using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class GoogleManager : Singleton<GoogleManager>
{
    [SerializeField] TMP_Text logAdd;
    [SerializeField] ILocalUser localUser;
    [SerializeField] List<IUserProfile> friendList;

    public ILocalUser LocalUser => localUser;
    public List<IUserProfile> FriendList => friendList;

    async void Start()
    {
        await Authenticate();
    }

#pragma warning disable 1998
    async Task Authenticate()
    {
        try
        {
            PlayGamesPlatform.Activate();
        }
        catch (Exception _e)
        {
            Debug.LogException(_e);
            throw;
        }

        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }


    internal async void ProcessAuthentication(SignInStatus _status)
    {
        //TODO Ajouter un systeme de notification au cas ou ça marche pas dans la build.
        if (_status == SignInStatus.Success)
        {
            Debug.Log("Login with google was succesful !");
            localUser = PlayGamesPlatform.Instance.localUser;
            await LoadLocalUserFriends();
        }
        else
        {
            Debug.Log("Login with google failed.");
        }
    }


    public async Task LoadLocalUserFriends()
    {
        List<IUserProfile> _friends = new List<IUserProfile>();

        if (localUser != null)
            if (localUser.authenticated)
                if (localUser.friends.Length > 0)
                {
                    logAdd.text = $"friendList has {localUser.friends.Length} user";
                    string _friendListSTR = "";
                    PlayGamesPlatform.Instance.LoadFriends(localUser, (status) =>
                    {
                        friendList.Clear();
                        foreach (IUserProfile friend in localUser.friends)
                        {
                            Debug.Log($"Friend: {friend.userName}, ID: {friend.id}");
                            _friendListSTR += friend + ", ";
                            friendList.Add(friend);
                        }
                        logAdd.text = _friendListSTR;
                    });
                }
                else
                {
                    logAdd.text = "No Friends";
                }
            else
            {
                Debug.LogError("Utilisateur non authenticated");
                logAdd.text = "Utilisateur non authenticated";
            }
        else
        {
            Debug.LogError("localUser null");
            logAdd.text = "localUser null";
        }

    }
#pragma warning restore 1998
}


