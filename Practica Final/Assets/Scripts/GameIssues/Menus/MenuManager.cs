using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    private Button newGameButton;
    private Button quitButt;

    private List<Button> startMenuButtons = new List<Button>();

    void Start()
    {
        newGameButton = GameObject.Find("NewGameButton").GetComponent<Button>();
        quitButt = GameObject.Find("QuitButton").GetComponent<Button>();

        // Añadimos botones a la lista para eliminar los listeners
        startMenuButtons.Add(newGameButton);
        startMenuButtons.Add(quitButt);

        // Desactivamos el panel principal y activamos el panel de selección de circuito
        newGameButton.onClick.AddListener(() => ListenerMethods.ChangeScene(Scenes.level1));
        quitButt.onClick.AddListener(() => ListenerMethods.QuitApp());
    }

    private void OnDestroy()
    {
        ListenerMethods.RemoveListeners(startMenuButtons);
    }
}