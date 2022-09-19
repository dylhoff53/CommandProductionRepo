using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    bool gameHasEnded = false;

    public Button startButton;
    public GameObject startMenu;
    GameObject player;

    public float restartDelay = 2f;

    public GameObject completeLevelUI;
    bool instantReplay = false;
    float replayStartTime;

    void Start()
    {
        PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
        player = playerMovement.gameObject;

        if (CommandLog.commands.Count > 0)
        {
            instantReplay = true;
            replayStartTime = Time.timeSinceLevelLoad;
        }
    }

    void Update()
    {
        if (instantReplay)
        {
            RunInstantReplay();
        }
    }

    public void CompleteLevel ()
    {
        completeLevelUI.SetActive(true);
    }
    public void EndGame()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            Debug.Log("GAME OVER");
            Invoke("Restart", restartDelay);
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        FindObjectOfType<PlayerMovement>().gameStart = false;
        startMenu.SetActive(true);
    }

    public void OnStartButton ()
    {
        FindObjectOfType<PlayerMovement>().gameStart = true;
        startMenu.SetActive(false);
    }

    void RunInstantReplay()
    {
        if (CommandLog.commands.Count == 0)
        {
            return;
        }

        Command command = CommandLog.commands.Peek();
        if (Time.timeSinceLevelLoad >= command.timestamp) // + replayStartTime)
        {
            command = CommandLog.commands.Dequeue();
            command._player = player.GetComponent<Rigidbody>();
            Invoker invoker = new Invoker();
            invoker.disableLog = true;
            invoker.SetCommand(command);
            invoker.ExecuteCommand();
        }
    }
}
