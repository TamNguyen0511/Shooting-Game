using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject ballPrefab;
    public GameObject playerPrefab;
    public Text scoreText;
    public Text ballText;
    public Text levelText;

    public GameObject panelMenu;
    public GameObject panelPlay;
    public GameObject panelLevelCompleted;
    public GameObject panelGameOver;

    public GameObject[] levels;

    public static GameManager instance { get; private set; }

    public enum State { MENU, INIT, PLAY, LEVELCOMPLETED, LOADLEVEL, GAMEOVER}
    State state;
    GameObject curBall;
    GameObject curLevel;
    bool isSwitchingState;

    private int score;

    public int Score
    {
        get { return score; }
        set { score = value;
            scoreText.text = "SCORE: " + score;
        }
    }

    private int level;

    public int Level
    {
        get { return level; }
        set { level = value; }
    }

    private int ball;

    public int Ball
    {
        get { return ball; }
        set { ball = value; }
    }




    // Start is called before the first frame update
    void Start()
    {
        SwitchSate(State.MENU);
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.MENU:
                break;
            case State.INIT:
                break;
            case State.PLAY:
                if (curBall == null)
                {
                    if (Ball > 0)
                    {
                        curBall = Instantiate(ballPrefab);
                    }
                    else SwitchSate(State.GAMEOVER);
                }
                break;
            case State.LEVELCOMPLETED:
                break;
            case State.LOADLEVEL:
                break;
            case State.GAMEOVER:
                break;
        }
    }

    public void SwitchSate(State newState, float delay=0)
    {
        StartCoroutine(SwitchDelay(newState, delay));
    }

    IEnumerator SwitchDelay(State newState, float delay)
    {
        isSwitchingState = true;
        yield return new WaitForSeconds(delay);
        EndState();
        state = newState;
        BeginState(newState);
        isSwitchingState = false;
    }

    void BeginState(State newState)
    {
        switch (newState)
        {
            case State.MENU:
                panelMenu.SetActive(true);
                break;
            case State.INIT:
                panelPlay.SetActive(true);
                Score = 0;
                Level = 0;
                Ball = 1;
                Instantiate(playerPrefab);
                SwitchSate(State.LOADLEVEL);
                break;
            case State.PLAY:
                break;
            case State.LEVELCOMPLETED:
                panelLevelCompleted.SetActive(true);
                break;
            case State.LOADLEVEL:
                if (Level > levels.Length)
                {
                    SwitchSate(State.GAMEOVER);
                }
                else
                {
                    curLevel = Instantiate(levels[level]);
                    SwitchSate(State.PLAY);
                }
                break;
            case State.GAMEOVER:
                break;
        }
    }

    void EndState()
    {
        switch (state)
        {
            case State.MENU:
                panelMenu.SetActive(false);
                break;
            case State.INIT:
                break;
            case State.PLAY:
                break;
            case State.LEVELCOMPLETED:
                panelLevelCompleted.SetActive(false);
                break;
            case State.LOADLEVEL:
                break;
            case State.GAMEOVER:
                panelPlay.SetActive(false);
                panelGameOver.SetActive(false);
                break;
        }
    }

    public void PlayClicked()
    {
        SwitchSate(State.INIT);
    }
}
