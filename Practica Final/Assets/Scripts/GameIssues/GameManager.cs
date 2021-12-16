using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public bool bossDefeted = false;

    void Update()
    {
        if (bossDefeted)
        {
            EndGame();
        }
    }

    public void EndGame()
    {
        ListenerMethods.ChangeScene(Scenes.gameOver);
        StartCoroutine(End());
    }

    IEnumerator End()
    {
        yield return new WaitForSeconds(5);
        ListenerMethods.ChangeScene(Scenes.gameOver);
    }
}
