using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteOrderScript : MonoBehaviour
{

    private SpriteRenderer spriteRend;
    private int Ysortingvalue;
    [SerializeField] private int priority;
    
    // Start is called before the first frame update
    void Start()
    {
        spriteRend = gameObject.GetComponent<SpriteRenderer>();
        Ysortingvalue = Mathf.RoundToInt(transform.position.y *-1) + 10 + priority;
        spriteRend.sortingOrder = Ysortingvalue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
