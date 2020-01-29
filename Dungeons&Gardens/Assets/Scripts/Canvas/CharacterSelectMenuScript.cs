using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectMenuScript : MonoBehaviour
{

    private int playercharA = 0;
    private int playercharB = 0;
    private int playercharC = 0;
    private int playercharD = 0;
    public Sprite TerraColumn;
    public Sprite DaveColumn;
    public Sprite DusterColumn;
    public Sprite HarvestColumn;

    public GameObject ColumnA;
    public GameObject ColumnB;
    public GameObject ColumnC;
    public GameObject ColumnD;
    

    // Start is called before the first frame update
    void Start()
    {
        ColumnASwitch();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            playercharA += 1;
            ColumnASwitch();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            playercharA -= 1;
            ColumnASwitch();
        }
    }

    public void ColumnASwitch()
    {

        switch (playercharA)
        {
            case 0:
                ColumnA.GetComponent<Image>().sprite = TerraColumn;
                break;
            case 1:
                ColumnA.GetComponent<Image>().sprite = DaveColumn;
                break;
            case 2:
                ColumnA.GetComponent<Image>().sprite = DusterColumn;
                break;
            case 3:
                ColumnA.GetComponent<Image>().sprite = HarvestColumn;
                break;
            case -1:               
                playercharA = 3;
                ColumnASwitch();
                break;
            case 4:               
                playercharA = 0;
                ColumnASwitch();
                break;
            default:
                Debug.Log("Something broke");
                break;

        }
    }
}
