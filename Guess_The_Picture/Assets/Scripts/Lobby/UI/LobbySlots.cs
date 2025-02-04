using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using SLobbyM = Singleton<LobbyManager>;
public class LobbySlots : MonoBehaviour
{
    int index = 0;
    event Action<int> readaptIndex;
    [SerializeField] Button removeButton;
    [SerializeField] TMP_Text nameField;

    public int Index { get { return index; } set { index = value; } }
    public TMP_Text NameField { get => nameField; set { nameField = value; } }

    private void Awake()
    {
        removeButton.onClick.AddListener(RemoveSlot);
        readaptIndex += ReadaptIndex;
        SLobbyM.Instance.OnLobbySlotRemove += readaptIndex;
    }

    public void InitLobbySlot(string _playerName, int _index)
    {
        nameField.text = _playerName;
        index = _index;
        Debug.Log("Index = " + index + "\n y = " + (transform.position.y - (120 * index)));
        transform.position = new Vector3(transform.position.x, transform.position.y - (120 * index), transform.position.z);
    }

    //Quand un slot s'enleve, l'index s'adapte pour que le tableau reste fluide et sans trous grace au replacement
    void ReadaptIndex(int _index)
    {
        Debug.Log("Readapt " + name);
        if (_index < index)
        {
            index--;
            nameField.text = "Guest "+ index;
            transform.position = new Vector3(transform.position.x, transform.position.y + 120, transform.position.z);
        }
    }

    void RemoveSlot()
    {
        Destroy(gameObject);
    }

    public void OnDestroy()
    {
        SLobbyM.Instance.OnLobbySlotRemove -= readaptIndex;
        SLobbyM.Instance.RemoveLobbySlot(index);
    }
}
