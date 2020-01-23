using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverStageScript : MonoBehaviour
{

    [SerializeField] private float thrust;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player" || collision.tag == "Player2")
        {

      
        print("THRUSSSST");
        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f, -2.0f) * thrust, ForceMode2D.Impulse);
        }
    }
}
