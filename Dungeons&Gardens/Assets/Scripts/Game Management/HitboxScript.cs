using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxScript : MonoBehaviour
{
    public float lifetime;
    public float lifespan;

    public float damagevalue;
    public GameObject PlayerHit;


    public float hitstun;
    public float knockback;
    public float hitInvultime;

    public int receivedplayerid;
    public int teamnumber;
    public GameManagerScript gmscript;
    [SerializeField] private bool nonpermanent;


    // Start is called before the first frame update
    void Start()
    {
        gmscript = GameObject.FindWithTag("GameManager").GetComponent<GameManagerScript>();
        //gmscript.PlayAudioOverlap(gmscript.Attack[Random.Range(0, gmscript.Attack.Length)]);
    }

    // Update is called once per frame
    void Update()
    {
        lifetime += 1 * Time.deltaTime;
        if(lifetime >= lifespan)
        {
           
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerHit = GameObject.FindWithTag("Player");
            receivedplayerid = 1;
            runHitCheck();
        }
        else if (other.tag == "Player2")
        {
            PlayerHit = GameObject.FindWithTag("Player2");
            receivedplayerid = 2;
            runHitCheck();
        }
        else
        {
            return;
        }
    }


    public void runHitCheck()
    {

            if (receivedplayerid != teamnumber)
            {

                if (PlayerHit.transform.position.x < transform.position.x)
                {
                    PlayerHit.GetComponent<PlayerControllerScript>().knockDirection = false;
                }
                else if ((PlayerHit.transform.position.x > transform.position.x))
                {
                    PlayerHit.GetComponent<PlayerControllerScript>().knockDirection = true;
                }
                PlayerHit.GetComponent<PlayerHealthScript>().updateHealth(damagevalue, hitInvultime, hitstun, knockback, teamnumber);

                if (nonpermanent != true)
                {

                    Destroy(this.gameObject);
            }
        
            }
        }
}

