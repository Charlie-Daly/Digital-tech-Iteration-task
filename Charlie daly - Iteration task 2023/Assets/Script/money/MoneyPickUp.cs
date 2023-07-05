using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyPickUp : MonoBehaviour
{
    public GameManager gameManager;

    public float pickUpMoney = 100;

    public float rotateX = 10;
    public float rotateY = 10;

    public static bool Money_PlayerInHitBox = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotateX * Time.deltaTime, rotateY * Time.deltaTime, 0);

        rotateY += Time.deltaTime;

        if (Money_PlayerInHitBox == true)
        {
            gameManager.P_money += pickUpMoney;

            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Money_PlayerInHitBox = true;
        }
    }
}
