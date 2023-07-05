using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public StatSave m_StatSaves;
    public CameraControl m_cameraControl;

    //Reference to the overlay text next to the display winning text, etc
    public TMP_Text m_MessageText;
    public TMP_Text m_TimerText;

    public GameObject m_statDisplayPanel;
    public TMP_Text m_StatDisplayText;

    public Button m_NewGameButton;
    public Button m_StatdisplayButton;

    public GameObject[] m_Player;
    public GameObject[] m_PlayerRespwanLocation;

    public float P_money;

    [SerializeField] GameObject MoneyText;

    public TMP_Text Txt_money;

    //public GameManager[] m_PreTimes;

    private float m_gameTime = 0;
    public float GameTime { get { return m_gameTime; } }

    public enum GameState
    {
        Start,
        Playing,
        GameOver
    };

    private GameState m_GameState;

    public GameState State { get { return m_GameState; } }

    private void Awake()
    {
        m_GameState = GameState.Start;
    }

    private void Start()
    {
        for (int i = 0; i < m_Player.Length; i++)
        {
            m_Player[i].SetActive(false);
        }

        m_TimerText.gameObject.SetActive(false);
        m_MessageText.text = "Get Ready";

        m_statDisplayPanel.gameObject.SetActive(false);
        m_NewGameButton.gameObject.SetActive(false);
        m_StatdisplayButton.gameObject.SetActive(false);
    }

    void Update()
    {
        switch (m_GameState)
        {
            case GameState.Start:
                if (Input.GetKeyUp(KeyCode.Return) == true)
                {
                    m_TimerText.gameObject.SetActive(true);
                    Txt_money.gameObject.SetActive(true);
                    m_MessageText.text = "";
                    m_GameState = GameState.Playing;

                    for (int i = 0; i < m_Player.Length; i++)
                    {
                        m_Player[i].SetActive(true);
                    }
                }
                break;
            case GameState.Playing:
                bool isGameOver = false;

                m_gameTime += Time.deltaTime;
                int seconds = Mathf.RoundToInt(m_gameTime);
                m_TimerText.text = string.Format("{0:D2}:{1:D2}",
                            (seconds / 60), (seconds % 60));

                m_gameTime += Time.deltaTime;

                Cursor.lockState = CursorLockMode.Locked;

                // m_cameraControl.iscursorlocked = true;

                //Money
                if (Input.GetKeyDown(KeyCode.Semicolon))
                {
                    P_money += 100;
                }

                Txt_money.text = "$" + P_money.ToString(); //ToString converts to string

                /*
                if (OneEnemyLeft() == true)
                {
                    isGameOver = true;
                }
                else if (IsPlayerDead() == true)
                {
                    isGameOver = true;
                }
                */

                if (Input.GetKeyDown(KeyCode.K))
                {
                    isGameOver = true;
                }

                if (isGameOver == true)
                {
                    m_GameState = GameState.GameOver;
                    m_TimerText.gameObject.SetActive(false);

                    m_NewGameButton.gameObject.SetActive(true);
                    m_StatdisplayButton.gameObject.SetActive(true);

                    for (int i = 0; i < m_Player.Length; i++)
                    {
                        m_Player[i].SetActive(false);
                    }

                    if (IsPlayerDead() == true)
                    {
                        m_MessageText.text = "Try Again";
                    }
                    else
                    {
                        m_MessageText.text = "Winner!";

                        //save the score
                        m_StatSaves.AddStat(Mathf.RoundToInt(m_gameTime));
                        m_StatSaves.SaveStatsToFile();
                    }
                }
                break;
            case GameState.GameOver:
                if (Input.GetKeyUp(KeyCode.Return) == true)
                {
                    // if (m_gameTime > m_PreTimes[]) ;

                    m_gameTime = 0;
                    m_GameState = GameState.Playing;
                    m_MessageText.text = "";
                    m_TimerText.gameObject.SetActive(true);
                    Txt_money.gameObject.SetActive(false);

                    m_cameraControl.iscursorlocked = false;

                    for (int i = 0; i < m_Player.Length; i++)
                    {
                        m_Player[i].SetActive(true);
                    }
                }
                break;

                //Save the score
                m_StatSaves.AddStat(Mathf.RoundToInt(P_money));
                m_StatSaves.SaveStatsToFile();
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }

    }

    private bool OneEnemyLeft()
    {
        int numEnemeyLeft = 0;

        for (int i = 0; i < m_Player.Length; i++)
        {
            if (m_Player[i].activeSelf == true)
            {
                numEnemeyLeft++;
            }
        }

        return numEnemeyLeft <= 1;
    }

    private bool IsPlayerDead()
    {
        for (int i = 0; i < m_Player.Length; i++)
        {
            if (m_Player[i].activeSelf == false)
            {
                if (m_Player[i].tag == "Player")
                    return true;
            }
        }

        return false;
    }

    public void OnNewGame()
    {
        m_NewGameButton.gameObject.SetActive(false);
        m_StatdisplayButton.gameObject.SetActive(false);
        m_statDisplayPanel.gameObject.SetActive(false);

        m_gameTime = 0;
        m_GameState = GameState.Playing;
        m_TimerText.gameObject.SetActive(true);
        m_MessageText.text = "";

        for (int i = 0; i < m_Player.Length; i++)
        {
            m_Player[i].SetActive(true);
            // m_Tanks[i].transform.position = m_TanksRespwanLocation.trans
        }
    }

    public void OnStatDisplay()
    {
        m_MessageText.text = "";

        m_StatdisplayButton.gameObject.SetActive(false);
        m_statDisplayPanel.SetActive(true);

        string text = "";
        for (int i = 0; i < m_StatSaves.stats.Length; i++)
        {
            int seconds = m_StatSaves.stats[i];
            text += string.Format("{0:D2}:{1:D2}\n",
                                        (seconds / 60), (seconds % 60));
        }
        m_StatDisplayText.text = text;
    }
}

/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    private float m_gameTime = 0;

    //Referenced scripts
    //Computer computer;
    public StatSave m_StatSaves;

    //stats
    public float P_money;


    [SerializeField] GameObject MoneyText;

    //Text
    public TMP_Text Txt_money;

    // Start is called before the first frame update
    void Start()
    {
        //Money
        Txt_money.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        m_gameTime += Time.deltaTime;

        //Money
        if (Input.GetKeyDown(KeyCode.Semicolon))
        {
            P_money += 100;
        }
    
        Txt_money.text = "$" + P_money.ToString(); //ToString converts to string

        // save the stats
        m_StatSaves.AddStat(Mathf.RoundToInt(m_gameTime));
        m_StatSaves.SaveStatsToFile();
    }
    
}
*/