using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour
{
    
    public GameManager gameManager;

    public static bool isGameOnComputer = true;

    [SerializeField] GameObject ComputerMenu;

    [SerializeField] GameObject BuyPage;
    [SerializeField] GameObject ManagePage;
    [SerializeField] GameObject SellPage;

    [SerializeField] GameObject Torch;

    public float torchCost = 50;

    public static bool PlayerInHitBox = false;

    // Start is called before the first frame update
    void Start()
    {
        // Time.timeScale = 0f;

        ComputerMenu.SetActive(false);

        BuyPage.SetActive(false);
        ManagePage.SetActive(false);
        SellPage.SetActive(false);

        Torch.SetActive(false);

        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if ((PlayerInHitBox == true))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (isGameOnComputer == false)
                {
                    OpenMenu();
                }
                else
                {
                    CloseMenu();
                }
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerInHitBox = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerInHitBox = false;
        }
    }


    public void OpenMenu()
    {
        ComputerMenu.SetActive(true);
        Time.timeScale = 0f;
        isGameOnComputer = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void CloseMenu()
    {
        ComputerMenu.SetActive(false);
        Time.timeScale = 1f;
        isGameOnComputer = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Buy()
    {
        BuyPage.SetActive(true);
    }

    public void Manage()
    {
        ManagePage.SetActive(true);
    }

    public void Sell()
    {
        SellPage.SetActive(true);
    }

    public void BuyTorch()
    {
        
        if (gameManager.P_money >= torchCost)
        {
            Torch.SetActive(true);
            gameManager.P_money -= torchCost;
        }
        else
        {
            Debug.Log("Not enough money");
        }
        
      //Debug.Log("BuyTorch");
      //Torch.SetActive(true);
    }

    /*
    void PauseGame()
    {
        ComputerMenu.SetActive(true);
        Time.timeScale = 0f;
        isGameOnComputer = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void RestartGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void RestartentireGame()
    {
       // SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();

        Debug.Log("Quit");
    }
    */
    
}
