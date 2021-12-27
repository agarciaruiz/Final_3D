using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button quitButt;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button backButton;

    private List<Button> startMenuButtons = new List<Button>();

    void Start()
    {
        // Añadimos botones a la lista para eliminar los listeners
        startMenuButtons.Add(newGameButton);
        startMenuButtons.Add(settingsButton);
        startMenuButtons.Add(backButton);
        startMenuButtons.Add(quitButt);

        // Desactivamos el panel principal y activamos el panel de selección de circuito
        newGameButton.onClick.AddListener(() => ListenerMethods.ChangeScene(Scenes.level1));
        settingsButton.onClick.AddListener(() => ListenerMethods.ChangeMenu(mainMenu, settingsMenu));
        backButton.onClick.AddListener(() => ListenerMethods.ChangeMenu(settingsMenu, mainMenu));
        quitButt.onClick.AddListener(() => ListenerMethods.QuitApp());
    }

    private void OnDestroy()
    {
        ListenerMethods.RemoveListeners(startMenuButtons);
    }
}