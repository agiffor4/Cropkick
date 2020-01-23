using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableCropScript : MonoBehaviour
{
    public float cropspeed;
    //  public bool cropfollowing;

    // private Transform player1pos;
    // private Transform player2pos;

    private Transform target;
    private GameObject targetplayer;
    public Collider2D detectradius;
    public Collider2D collectradius;
    private GameManagerScript gmscript;


    // Start is called before the first frame update
    void Start()
    {
        gmscript = GameObject.FindWithTag("GameManager").GetComponent<GameManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerStay2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            if (other.GetComponent<SpriteRenderer>().enabled == true)
            {
                float step = cropspeed * Time.deltaTime;
                target = GameObject.FindWithTag("Player").transform;
                targetplayer = GameObject.FindWithTag("Player");
                transform.position = Vector2.MoveTowards(transform.position, target.position, step);

                if (Vector2.Distance(transform.position, target.position) < 0.2f)
                {

                    gmscript.PlayAudioOverlap(gmscript.GrabCrop);
                    target.GetComponent<PlayerInventoryScript>().addone();
                    target.GetComponent<PlayerInventoryScript>().updateText();
                    Destroy(gameObject);
                }
            }

        }
        else if (other.tag == "Player2")
        {
            if (other.GetComponent<SpriteRenderer>().enabled == true)
            {
                float step = cropspeed * Time.deltaTime;
                target = GameObject.FindWithTag("Player2").transform;
                targetplayer = GameObject.FindWithTag("Player2");
                transform.position = Vector2.MoveTowards(transform.position, target.position, step);

                if (Vector2.Distance(transform.position, target.position) < 0.2f)
                {
                    gmscript.PlayAudioOverlap(gmscript.GrabCrop);
                    target.GetComponent<PlayerInventoryScript>().addone();
                    target.GetComponent<PlayerInventoryScript>().updateText();
                    Destroy(gameObject);
                }
            }
        }
    }


}
