using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    #region Methods
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.N))
        {
            MainUI _mainUI = GetComponent<MainUI>();
            Menu _currentScreen = GetComponent<MainUI>().CurrentMenu;

            if (!_mainUI)
                //Debug.Log("[BackButton::Update] -> No Main UI");
                return;

            if (!_currentScreen)
                //Debug.Log("[BackButton::Update] -> No CurrentScreen");
                return;

            _currentScreen.onClose.Invoke();
           // Debug.Log("[BackButton::Update] -> Escape");
        }
    }
    #endregion Methods
}
