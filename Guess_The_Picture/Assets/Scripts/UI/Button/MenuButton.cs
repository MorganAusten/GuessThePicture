using UnityEngine;

public class MenuButton : UnityEngine.UI.Button
{
    #region Var
    [SerializeField] protected MainUI mainUI;
    [SerializeField]protected Menu currentMenu;
    [SerializeField]protected Menu menuToOpen;
    [SerializeField]bool loul;

    //Properties
    public Menu MenuToOpen { get; set; }
    public MainUI MainUI { get => mainUI; set { mainUI = value; } }
    public Menu CurrentMenu { get => currentMenu; set { currentMenu = value; } }
    #endregion Var

    #region Methods
    protected override void Awake()
    {
        //Debug.Log(ToString() + "Registered !");
        onClick.AddListener(OnClick);
    }
    
    public virtual void SetMenuToOpen()
    {
        if (!currentMenu)
        {
            Debug.Log("No _menu");
            return;
        }
    }

    protected virtual void OnClick()
    {
        if (!currentMenu)
        {
            Debug.Log("No _menu");
            return;
        }
        TransitionForNextMenu();
    }

    protected virtual void TransitionForNextMenu()  
    {
        mainUI.CurrentMenu.gameObject.SetActive(false);
        menuToOpen.onOpen.Invoke(currentMenu);
        mainUI.CurrentMenu = menuToOpen;
    }
    #endregion Methods

}
