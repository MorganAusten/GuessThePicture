using UnityEngine;

public class GameImage : MonoBehaviour
{
    [SerializeField] Texture2D texture;
    [SerializeField] string[] answers;

    public Texture2D Texture { get => texture; set { texture = value; } }
    public string[] Answers { get => answers; set { answers = value; }  }


}
