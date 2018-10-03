using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGenerator : MonoBehaviour {

    public GameObject[] cards;//Array to hold all the cards that will determine the amount of spaces a player can move

	// Prepare cards before the game begins
	void Start () {
        ScaleCards();
        Draw();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //Draw a card at random and instantiate it to the world. Includes animation
    public void Draw() {
        //@Casey: Use this method to random choose a value from 0 to cards.length exclusive (0,52), and call upon the index.
        // No need to shuffle, just be random. This will save us time and memory.
        // After you are able to generate random cards into the world, work on the animation. You will need to learn about Time.deltaTime and Euler Angles to do so using code.
        //   unless of course you are able to find a better way.
        // If you want to adjust where the cards appear in the world, just adjust the second parameter of the Instantiate method to do so; 
        //    we do not yet need to attach the cards to any players during this sprint.
        // If you have any questions, please do not hesitate in letting me know. THANK YOU!!!!
        int index = 0;
        Instantiate(cards[index], new Vector3(0.0f, 22.0f, 0.0f), Quaternion.identity);
    }

    //Change the scale of all cards so that it may be easily seen be all players
    public void ScaleCards() {
        if (cards.Length > 0)
        {
            Vector3 scale = new Vector3(2.0f, 2.0f, 2.0f); //Standard scale for now
            for (int i = 0; i < cards.Length; i++)
            {
                cards[i].gameObject.transform.localScale = scale;
            }
        }
    }
}
