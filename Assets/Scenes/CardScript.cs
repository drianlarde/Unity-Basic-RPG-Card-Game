using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScript : MonoBehaviour
{

    public GameObject player;
    public GameObject enemy;

    public string cardName;
    public string cardDescription;
    public int attack;
    public int heal;
    public int manaCost;

    public void PlayCard()
    {
        Debug.Log("Current Mana: " + player.GetComponent<PlayerScript>().currentMana);
        if (player.GetComponent<PlayerScript>().currentMana >= manaCost)
        {
            player.GetComponent<PlayerScript>().currentMana -= manaCost;

            if (attack == 0)
            {
                player.GetComponent<PlayerScript>().Heal(heal);
            }
            else
            {
                enemy.GetComponent<EnemyScript>().TakeDamage(attack);
                player.transform.LeanMoveLocalX(1, 0.3f).setEaseOutQuad().setLoopPingPong(1);
                // Animate using leanTween to the right then go back to the original position. Make it ease in and ease out.



            }

            Debug.Log("Played " + cardName);

            // Destroy the card
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Not enough mana");
        }
    }

    void OnMouseDown()
    {
        PlayCard();
    }

    // When hovered make it bigger on its current scale
    void OnMouseEnter()
    {
        // // It's original scale is 2, 3, 1, make animation smooth
        // transform.localScale = new Vector3(3f, 4f, 1);
        // // Set layer order to 1 so it's on top of other cards
        // GetComponent<SpriteRenderer>().sortingOrder = 1;

        // // The y postition of the card is -2.2, so add 1 to it to make it 0.8
        // transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);

        // // Set layer order to 1 so it's on top of other cards
        GetComponent<SpriteRenderer>().sortingOrder = 2;

        // Use LeanTween to make the animation smooth, original scale is 0.6, 0.6
        LeanTween.scale(gameObject, new Vector3(0.6f + 0.5f, 0.6f + 0.5f, 1), 0.1f).setEaseOutQuad();




    }

    // When not hovered make it back to its original scale
    void OnMouseExit()
    {
        // transform.localScale = new Vector3(2, 3, 1);
        // // Set layer order to 0 so it's on top of other cards
        // GetComponent<SpriteRenderer>().sortingOrder = 0;

        // // The y postition of the card is 0.8, so subtract 1 to it to make it -2.2
        // transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);

        GetComponent<SpriteRenderer>().sortingOrder = 0;

        // Use LeanTween to make the animation smooth
        LeanTween.scale(gameObject, new Vector3(0.6f, 0.6f, 1), 0.1f).setEaseOutQuad();


        // // Set layer order to 0 so it's on top of other cards
    }




}
