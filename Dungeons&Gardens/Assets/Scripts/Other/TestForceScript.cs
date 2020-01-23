using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestForceScript : MonoBehaviour
{
    private Rigidbody2D rb;
    public float thrust;


    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            rb.AddForce(new Vector2(5.0f, 5.0f) * thrust);
        }
    }
}
