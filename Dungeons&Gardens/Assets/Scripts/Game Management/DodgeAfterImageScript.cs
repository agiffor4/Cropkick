using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeAfterImageScript : MonoBehaviour
{
    public GameObject Player1;
    public GameObject Player2;
    public SpriteRenderer dodgespriteRend;
    public string playerType;
    [SerializeField] private Sprite dodgecurrSprite;
    private bool flipped = false;

    // Start is called before the first frame update
    void Start()
    {      
        Player1 = GameObject.FindWithTag("Player");
        Player2 = GameObject.FindWithTag("Player2");
        dodgespriteRend = this.GetComponent<SpriteRenderer>();
        if (playerType == "Player")
        {
            flipped = Player1.GetComponent<PlayerControllerScript>().faceRight;
            dodgecurrSprite = Player1.GetComponent<PlayerControllerScript>().currSprite;
        }
        else if (playerType == "Player2")
        {

            flipped = Player2.GetComponent<PlayerControllerScript>().faceRight;
            dodgecurrSprite = Player2.GetComponent<PlayerControllerScript>().currSprite;
        }

        if(flipped == true)
        {            
            transform.Rotate(0, 180f, 0f);
        }
        dodgespriteRend.sprite = dodgecurrSprite;
        StartCoroutine(Cleaner());
        //coroutine = CleanerCoroutine();
    }


    // Update is called once per frame
    void Update()
    {
      
    }

    public IEnumerator Cleaner()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
