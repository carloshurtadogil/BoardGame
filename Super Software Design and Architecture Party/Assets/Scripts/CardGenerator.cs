using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardGenerator : MonoBehaviour
{

    public GameObject[] cards;//Array to hold all the cards that will determine the amount of spaces a player can move
    private float count = 0.0f;
    private int value = -1;

    // Prepare cards before the game begins
    void Start()
    {
        ScaleCards();
        StartCoroutine("Draw");
        Debug.Log("Value: " + value);
    }

    // Update is called once per frame
    void Update()
    {

        if(count < 4.0f) {
            count += Time.deltaTime;
            //Debug.Log(count);
        } else  {
            //StartCoroutine ("Draw");
        }
    }

    //Draw a card at random and instantiate it to the world. Includes animation
    IEnumerator Draw()
    {
        float x = 90.0f; //Card is face down when instantiated
        int index = Random.Range(0, cards.Length);//Select a random card
        value = index % 13 + 1;//Get numerical value of card
        GameObject dealt = Instantiate(cards[index], new Vector3(0.0f, 22.0f, 0.0f), Quaternion.Euler(90.0f, 0.0f, 0.0f));//Deal a card

        //Rotate the card on its x-axis to reveal number
        while (dealt.transform.rotation.x > 0.0f)
        {
            x -= 2.0f;
            dealt.transform.rotation = Quaternion.Euler(x, 0.0f, 0.0f);
            yield return new WaitForSeconds(0.00000000000000000001f);
        }
        dealt.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
    }

    //Return the value of the most recently dealt card
    public int getValue() {
        return value;
    }

    //Change the scale of all cards so that it may be easily seen be all players
    public void ScaleCards()
    {
        if (cards.Length > 0)
        {
            Vector3 scale = new Vector3(50.0f, 50.0f, 50.0f); //Standard scale for now
            for (int i = 0; i < cards.Length; i++)
            {
                cards[i].gameObject.transform.localScale = scale;
                cards[i].gameObject.tag = "Card";
            }
        }
    }
}
