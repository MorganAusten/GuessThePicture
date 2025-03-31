using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    #region Var
    [SerializeField] Menu currentMenu;

    //Menus 
    [SerializeField] Menu mainMenu;
    [SerializeField] Menu themeMenu;
    [SerializeField] Menu scoreMenu;
    Menu[] menus;

    //Properties
    public bool IsValid => mainMenu && themeMenu && scoreMenu;
    public Menu CurrentMenu {get => currentMenu; set { currentMenu = value; } }
    public Menu MainMenu => mainMenu;
    public Menu ThemeMenu => themeMenu;
    public Menu ScoreMenu => scoreMenu;
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
        SetMainUIInMenus();
        SetMenusManagementInMenusButton();
    }
    private void SetMainUIInMenus()
    {
        mainMenu.MainUI =  this;
        themeMenu.MainUI = this;
        scoreMenu.MainUI = this;
    }

    private void SetMenusManagementInMenusButton()
    {
        menus = new Menu[]
        {   
            mainMenu, 
            themeMenu, 
            scoreMenu
        };

        for (int i = 0; i < menus.Length; i++)
            for (int x = 0; x < menus[i].ButtonList.Length; x++)
            {
                MenuButton _button = menus[i].ButtonList[x];
                if (!_button)
                    continue;
                else
                {
                    _button.CurrentMenu = menus[i];
                    _button.MainUI = this;
                    _button.SetMenuToOpen();
                }
            }
    }

    #endregion Methods
}
