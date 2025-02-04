using Unity.Netcode;
using UnityEngine;

public class GamePlayer : NetworkBehaviour
{

    [SerializeField] string playerName;
    [SerializeField] int score;

    // Le string n'est pas serializable dans le netcode. Il faut soit faire une methode pour gerer sa sérialization, soit utiliser une autre valeur 
    //[SerializeField] NetworkVariable<string> nPlayerName ;
    [SerializeField] NetworkVariable<int> nScore;

    public string PlayerName { get => playerName;  set => playerName = value; }
    public int Score { get => score; private set => score = value; }
    
    //public NetworkVariable<string> NPlayerName => nPlayerName;
    public NetworkVariable<int> NScore { get => nScore; private set => nScore = value; } 

    void Start()
    {
      //  nPlayerName =  new NetworkVariable<string>("playerName");
    }

    public override void OnNetworkSpawn()
    {
        // nPlayerName.Value = playerName;
    }

    void Update()
    {
        
    }
}
