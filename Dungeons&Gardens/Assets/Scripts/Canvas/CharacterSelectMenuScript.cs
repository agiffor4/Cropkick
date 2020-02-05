using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
struct PlayerData
{
   public int playerID;
   public int characterID;
}*/

public class CharacterSelectMenuScript : MonoBehaviour
{
    public List<PlayerData> PlayerList = new List<PlayerData>();// = new int[4];

    bool disableInput = false;

    public int player;
    public int character;
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

    private bool columnAactive;
    private bool columnBactive;
    private bool columnCactive;
    private bool columnDactive;

    private bool player1Ready;
    private bool player2Ready;
    private bool player3Ready;
    private bool player4Ready;
    int playerTotal;

    public GameObject characterColumns;
    public GameObject stageSelect;

    public GlobalControl gcscript;

    // Start is called before the first frame update
    void Start()
    {
        gcscript = GameObject.FindWithTag("GControl").GetComponent<GlobalControl>();

        stageSelect.SetActive(false);
        characterColumns.SetActive(true);
        columnAactive = false;
        columnBactive = false;
        columnCactive = false;
        columnDactive = false;
        player1Ready = false;
        player2Ready = false;
        player3Ready = false;
        player4Ready = false;
        //ColumnASwitch();
        // ColumnBSwitch();
        // ColumnCSwitch();
        // ColumnDSwitch();
    }

    // Update is called once per frame
    void Update()
    {

      
        if (player1Ready == true && player2Ready == true && player3Ready == true && player4Ready == true)
        {
            playerTotal = 4;
        }
        else if (player1Ready == true && player2Ready == true && player3Ready == true)
        {
            playerTotal = 3;
        }
        else if (player1Ready == true && player2Ready == true)
        {
            playerTotal = 2;
        }


        if (disableInput == false)
        {


            if (Input.GetKeyDown(KeyCode.Space))
            {
                InitializeGame();
            }

            #region player1 inputs
            if (Input.GetKeyDown(KeyCode.UpArrow))      //player1 up
            {
                if (columnAactive == true)
                {
                    playercharA += 1;
                    ColumnASwitch();
                }
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))         //player1 down
            {
                if (columnAactive == true)
                {
                    playercharA -= 1;
                    ColumnASwitch();
                }
            }

            if (Input.GetKeyDown(KeyCode.Keypad1))     //player1 confirm
            {
                if (columnAactive == true)
                {
                    columnAactive = false;
                    player1Ready = true;
                }
                else if (player1Ready == false)
                {
                    columnAactivate();
                    ColumnASwitch();
                }
            }

            if (Input.GetKeyDown(KeyCode.Keypad2))  //player1 deselect
            {
                if (player1Ready == true)
                {
                    columnAactive = true;
                    player1Ready = false;
                }
            }
            #endregion

            #region player2 inputs
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (columnBactive == true)
                {
                    playercharB += 1;
                    ColumnBSwitch();
                }
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                if (columnBactive == true)
                {
                    playercharB -= 1;
                    ColumnBSwitch();
                }
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                if (columnBactive == true)
                {
                    columnBactive = false;
                    player2Ready = true;
                }
                else if (player2Ready == false)
                {
                    columnBactivate();
                    ColumnBSwitch();
                }
            }
        

        if (Input.GetKeyDown(KeyCode.G))  //player2 deselect
        {
            if (player2Ready == true)
            {
                columnBactive = true;
                player2Ready = false;
            }
        }
        #endregion


        }
    }

    public void SetPlayerData(int setPlayer, int setCharacter)
    {
        PlayerList.Add(new PlayerData(setPlayer, setCharacter));           
    }

    public void InitializeGame()
    {
      
        if (player1Ready == true){
            // PlayerList.Add(new PlayerData(0, playercharA));
            //gcscript.gp_player1char = playercharA;
            GlobalControl.Instance.gc_player1char = playercharA;
        }

        if (player2Ready == true)
        {
            //   PlayerList.Add(new PlayerData(1, playercharB));
            //gcscript.gp_player2char = playercharB;
            GlobalControl.Instance.gc_player2char = playercharB;

        }

        if (player3Ready == true)
        {
            //PlayerList.Add(new PlayerData(2, playercharC));
            //gcscript.gp_player3char = playercharC;
            GlobalControl.Instance.gc_player3char = playercharC;

        }

        if (player4Ready == true)
        {
            //PlayerList.Add(new PlayerData(3, playercharD));
            //gcscript.gp_player4char = playercharD;
            GlobalControl.Instance.gc_player4char = playercharD;

        }

        if(playerTotal >= 2)
        {
            disableInput = true;
            GlobalControl.Instance.gc_playertotal = playerTotal;
            //gcscript.gp_playertotal = playerTotal;            
            characterColumns.SetActive(false);
            stageSelect.SetActive(true);
            
            //Transition scene
        }
        else
        {
            print("NOT ENOUGH PLAYERS");

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
                ColumnA.GetComponent<Image>().sprite = HarvestColumn;
                // ColumnSwitch(columnObject, columnValue);
                break;
            case 4:
                playercharA = 0;
                ColumnA.GetComponent<Image>().sprite = TerraColumn;
                //  ColumnSwitch(columnObject, columnValue);
                break;
            default:
                Debug.Log("Something broke");
                break;

        }
    }

    public void ColumnBSwitch()
    {

        switch (playercharB)
        {
            case 0:
                ColumnB.GetComponent<Image>().sprite = TerraColumn;
                break;
            case 1:
                ColumnB.GetComponent<Image>().sprite = DaveColumn;
                break;
            case 2:
                ColumnB.GetComponent<Image>().sprite = DusterColumn;
                break;
            case 3:
                ColumnB.GetComponent<Image>().sprite = HarvestColumn;
                break;
            case -1:
                playercharB = 3;
                ColumnBSwitch();
                break;
            case 4:
                playercharB = 0;
                ColumnBSwitch();
                break;
            default:
                Debug.Log("Something broke");
                break;

        }
    }

    public void ColumnCSwitch()
    {

        switch (playercharC)
        {
            case 0:
                ColumnC.GetComponent<Image>().sprite = TerraColumn;
                break;
            case 1:
                ColumnC.GetComponent<Image>().sprite = DaveColumn;
                break;
            case 2:
                ColumnC.GetComponent<Image>().sprite = DusterColumn;
                break;
            case 3:
                ColumnC.GetComponent<Image>().sprite = HarvestColumn;
                break;
            case -1:
                playercharC = 3;
                ColumnCSwitch();
                break;
            case 4:
                playercharC = 0;
                ColumnCSwitch();
                break;
            default:
                Debug.Log("Something broke");
                break;

        }
    }

    public void ColumnDSwitch()
    {

        switch (playercharD)
        {
            case 0:
                ColumnD.GetComponent<Image>().sprite = TerraColumn;
                break;
            case 1:
                ColumnD.GetComponent<Image>().sprite = DaveColumn;
                break;
            case 2:
                ColumnD.GetComponent<Image>().sprite = DusterColumn;
                break;
            case 3:
                ColumnD.GetComponent<Image>().sprite = HarvestColumn;
                break;
            case -1:
                playercharD = 3;
                ColumnDSwitch();
                break;
            case 4:
                playercharD = 0;
                ColumnDSwitch();
                break;
            default:
                Debug.Log("Something broke");
                break;

        }
    }


    public void columnAactivate()
    {
        if (columnAactive == false)
        {
            columnAactive = true;
        }
        else
        {
            return;
        }
    }

    public void columnBactivate()
    {
        if (columnBactive == false)
        {
            columnBactive = true;
        }
        else
        {
            return;
        }
    }

    public void columnCactivate()
    {
        if (columnCactive == false)
        {
            columnCactive = true;
        }
        else
        {
            return;
        }
    }

    public void columnDactivate()
    {
        if (columnDactive == false)
        {
            columnDactive = true;
        }
        else
        {
            return;
        }
    }
}
