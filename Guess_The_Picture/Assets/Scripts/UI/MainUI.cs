using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    #region Var
    [SerializeField] RawImage mainMenuBackground;
    [SerializeField] Menu currentMenu;

    //Menus 
    [SerializeField] Menu mainMenu;
    [SerializeField] Menu themeMenu;
    [SerializeField] Menu scoreMenu;
    [SerializeField] Menu soloOrMultiMenu;
    [SerializeField] Menu lobbyMenu;
    Menu[] menus;

    //Properties
    public bool IsValid => mainMenuBackground && mainMenu && themeMenu && scoreMenu;
    public Menu CurrentMenu {get => currentMenu; set { currentMenu = value; } }
    public Menu MainMenu => mainMenu;
    public Menu ThemeMenu => themeMenu;
    public Menu ScoreMenu => scoreMenu;
    public Menu SoloOrMultiMenu => soloOrMultiMenu;
    public Menu LobbyMenu => lobbyMenu;
    #endregion Var

    #region Methods
    void Start()
    {
        if (!IsValid)
        {
            Debug.Log("No Main menu ");
            return;
        }
        ManagePanels();
        currentMenu = mainMenu;
        //Debug.Log("Main menu ");
    }

    // Gestion des menus (menus précedents, désactivation des menus au démarage, 
    void ManagePanels()
    {
        mainMenu.gameObject.SetActive(true);
        SetMainUIInMenus();
        SetMenusManagementInMenusButton();
    }
    private void SetMainUIInMenus()
    {
        mainMenu.MainUI =  this;
        themeMenu.MainUI = this;
        scoreMenu.MainUI = this;
        lobbyMenu.MainUI = this;
        soloOrMultiMenu.MainUI = this;
    }

    private void SetMenusManagementInMenusButton()
    {
        menus = new Menu[]
        {   
            mainMenu, 
            themeMenu, 
            scoreMenu, 
            soloOrMultiMenu,
            lobbyMenu
        };

        for (int i = 0; i < menus.Length; i++)
            for (int x = 0; x < menus[i].ButtonList.Length; x++)
            {
                MenuButton _button = menus[i].ButtonList[x];
                if (!_button)
                    continue;
                else
                {
                    //Debug.Log("[SetMenusManagementInMenusButton::MainUI] -> for " + _button.ToString() + ", CurrentMenu = " + menus[i].ToString());
                    _button.CurrentMenu = menus[i];
                    _button.MainUI = this;
                    _button.SetMenuToOpen();
                }
            }
    }

    #endregion Methods
}
