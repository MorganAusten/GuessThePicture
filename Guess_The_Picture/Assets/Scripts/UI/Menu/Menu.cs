using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Menu : MonoBehaviour
{
    #region Var
    public Action<Menu> onOpen;
    public Action onClose;
    [SerializeField, HideInInspector] MainUI mainUI;
    [SerializeField, HideInInspector] Menu precedentMenu = null;
    [SerializeField] MenuButton[] buttonList;
    public MainUI MainUI { get => mainUI; set => mainUI = value; }
    public MenuButton[] ButtonList => buttonList;
    public Menu PrecedentMenu { get => precedentMenu; set => precedentMenu = value; }
    #endregion Var

    #region Methods

    protected virtual void Awake()
    {
        onOpen += OnOpen;
        onClose += OnEscapePressed;
        gameObject.SetActive(false);
    }

    protected virtual void OnEscapePressed()
    {
        if (precedentMenu)
        {
            //Debug.Log(ToString());
            mainUI.CurrentMenu = precedentMenu;
            precedentMenu.gameObject.SetActive(true);
            gameObject.SetActive(false);
            //Debug.Log("[OnEscapePressed::Menu] -> Escape of " + ToString());
        }
        else
            SceneManager.LoadScene("SampleScene");
    }

    protected virtual void OnOpen(Menu _precedentMenu)
    {
        //Debug.Log("Ouverture du menu");
        precedentMenu = _precedentMenu;
        gameObject.SetActive(true);
    }
    #endregion Methods
}
