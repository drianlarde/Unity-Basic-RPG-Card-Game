using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    public List<GameObject> hand;
    public List<GameObject> deck;
    public List<GameObject> discard;

    public Transform handPanel;
    public float cardSpacing = 3f;

    public GameObject doneObject;
    public GameObject gameOverObject;

    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            hand.Add(deck[Random.Range(0, deck.Count)]);
        }

        float handWidth = (hand.Count - 1) * cardSpacing;
        Vector3 startPosition = handPanel.position - new Vector3(handWidth / 2, 0, 0);

        for (int i = 0; i < hand.Count; i++)
        {
            GameObject card = hand[i];

            // Instantiate the card in the hand
            GameObject newCard = Instantiate(card, Vector3.zero, Quaternion.identity);

            // Set the parent of the card to the handPanel
            newCard.transform.SetParent(handPanel, false);

            // Position the card relative to the center of the handPanel
            Vector3 position = startPosition + new Vector3(i * cardSpacing, 0, 0);
            newCard.transform.localPosition = position;

            // Enable the card's collider
            newCard.GetComponent<BoxCollider2D>().enabled = true;

            newCard.transform.localScale = new Vector3(0.6f, 0.6f, 1);
        }
    }

    void Update()
    {
        // If enemy health is 0, then end the game, find the object named 'Done' then enable it
        if (enemy.GetComponent<EnemyScript>().currentHealth <= 0)
        {
            // Enable the 'Done' object
            doneObject.SetActive(true);
        }
        if (player.GetComponent<PlayerScript>().currentHealth <= 0)
        {
            // Enable the 'Done' object
            gameOverObject.SetActive(true);
        }
    }

    // Maintain the current cards in hand, then just add 2 cards to the hand if it's less than 5. If exceeds 5, then only add 1 card. If it's 5, then do nothing.
    public void DrawCard()
    {

        hand.Clear();
        foreach (Transform child in handPanel)
        {
            hand.Add(child.gameObject);
        }
        Debug.Log("Hand Count: " + hand.Count);


        if (hand.Count == 4)
        {
            hand.Add(deck[Random.Range(0, deck.Count)]);
        }
        else if (hand.Count < 4)
        {
            for (int i = 0; i < 2; i++)
            {
                hand.Add(deck[Random.Range(0, deck.Count)]);
            }
        }

        foreach (Transform child in handPanel)
        {
            Destroy(child.gameObject);
        }

        float handWidth = (hand.Count - 1) * cardSpacing;
        Vector3 startPosition = handPanel.position - new Vector3(handWidth / 2, 0, 0);

        for (int i = 0; i < hand.Count; i++)
        {
            GameObject card = hand[i];

            // Instantiate the card in the hand
            GameObject newCard = Instantiate(card, Vector3.zero, Quaternion.identity);

            // Set the parent of the card to the handPanel
            newCard.transform.SetParent(handPanel, false);

            // Position the card relative to the center of the handPanel
            Vector3 position = startPosition + new Vector3(i * cardSpacing, 0, 0);
            newCard.transform.localPosition = position;

            // Enable the card's collider
            newCard.GetComponent<BoxCollider2D>().enabled = true;

            newCard.transform.localScale = new Vector3(0.6f, 0.6f, 1);
        }


    }

    // Add mana to the player when the turn ends
    public void GenerateMana()
    {
        player.GetComponent<PlayerScript>().currentMana += 2;
    }

    public void EndTurn()
    {
        enemy.GetComponent<EnemyScript>().RandomMove();
        GenerateMana();
        DrawCard();
    }
}
