using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventoryScript : MonoBehaviour
{
    PlayerControllerScript pcscript;
    public float InvCropCount;

    public Text canvasInv;
    private bool collectCooldown;

    // Start is called before the first frame update
    void Start()
    {
        pcscript = gameObject.GetComponent<PlayerControllerScript>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void updateText()
    {
        if (pcscript.playerNumber == 1)
        {
            if (collectCooldown == false)
            {
               // 
                canvasInv = GameObject.FindWithTag("InventoryP1").GetComponent<Text>();
                canvasInv.text = "x " + InvCropCount;
                collectCooldown = true;
                StartCoroutine(CollectCooldown());
            }

        } else if (pcscript.playerNumber == 2)
        {
            if (collectCooldown == false)
            {
               // InvCropCount += 1;
                canvasInv = GameObject.FindWithTag("InventoryP2").GetComponent<Text>();
                canvasInv.text = "x " + InvCropCount;
                collectCooldown = true;
                StartCoroutine(CollectCooldown());
            }
        }
    }

    public void updatetextreset()
    {
        if (pcscript.playerNumber == 1)
        {
            InvCropCount = 0;
            canvasInv = GameObject.FindWithTag("InventoryP1").GetComponent<Text>();
                canvasInv.text = "x " + InvCropCount;
            

        }
        else if (pcscript.playerNumber == 2)
        {
            InvCropCount = 0;
            canvasInv = GameObject.FindWithTag("InventoryP2").GetComponent<Text>();
                canvasInv.text = "x " + InvCropCount;
            
        }
    }

    public void updatetextrespawn()
    {
        if (pcscript.playerNumber == 1)
        {
            if(InvCropCount < 0)
            {
                InvCropCount = 0;
            }
            canvasInv = GameObject.FindWithTag("InventoryP1").GetComponent<Text>();
            canvasInv.text = "x " + InvCropCount;


        }
        else if (pcscript.playerNumber == 2)
        {
            if (InvCropCount < 0)
            {
                InvCropCount = 0;
            }
            canvasInv = GameObject.FindWithTag("InventoryP2").GetComponent<Text>();
            canvasInv.text = "x " + InvCropCount;

        }
    }
    public IEnumerator CollectCooldown()
    {
        yield return new WaitForSeconds(0.01f);
        collectCooldown = false;
    }

    public void addone()
    {
        InvCropCount += 1;
    }

   
}
