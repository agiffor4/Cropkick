using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverforCropsScript : MonoBehaviour
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

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "DroppedCrop")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f, -2.0f) * thrust, ForceMode2D.Impulse);
        }
    }
}
