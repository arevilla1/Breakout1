using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehavior : MonoBehaviour
{

    public Rigidbody2D rb;
    public bool inPlay;
    public Transform paddle;
    public Transform explosion;
    public Transform lifePowerup;
    public GameManager gm;

    private AudioSource audioSource;
    public AudioClip Hit, Jump, Death;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!inPlay)
        {
            transform.position = paddle.position;
        }

        //Jump - built in preset input
        //and inPlay is false
        if (Input.GetButtonDown("Jump") && !inPlay)
        {
            inPlay = true;
            rb.AddForce(Vector2.up * 500);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //If the other "thing" (bottom border) the ball hit is "bottom", 
        if (other.CompareTag("bottom"))
        {
            //Vector2.zero = (0,0); so, 0 velocity
            rb.velocity = Vector2.zero;
            inPlay = false;
            gm.UpdateLives(-1);

            if (audioSource.clip != Death)
            {
                audioSource.clip = Death;
            }

            audioSource.Play();
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("brick"))
        {
            Bricks bricksScript = other.gameObject.GetComponent<Bricks>();

            if (bricksScript.hitsToBreak > 1)
            {
                bricksScript.BreakBrick();
            }
            else
            {
                //Percentage chance for life powerup to happen
                int randomChance = Random.Range(1, 101);
                if (randomChance < 50)
                {
                    Instantiate(lifePowerup, other.transform.position, other.transform.rotation);
                }

                //Save the explosion in a variable so it destroys after.
                Transform newExplosion = Instantiate(explosion, other.transform.position, other.transform.rotation);

                Destroy(newExplosion.gameObject, 2.5f);

                //Go to gameObject -> go to its script -> get the points variable 
                gm.UpdateScore(bricksScript.points);

                Destroy(other.gameObject);
            }

            if (audioSource.clip != Hit)
            {
                audioSource.clip = Hit;
            }

            audioSource.Play();
        }

        if (other.transform.CompareTag("paddle"))
        {
            if (audioSource.clip != Jump)
            {
                audioSource.clip = Jump;
            }

            audioSource.Play();
        }
    }

}