using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    private MenuTransitionScript mtscript;

    public GameObject button3select;
    public GameObject button2select;
    public GameObject button1select;
    public GameObject button0select;

    public Camera mainCam;

    public int buttonPos;

    private Scene currstage;
    [SerializeField] private string menu1 = "CharacterSelectMenu";
    [SerializeField] private string menu2 = "CreditsMenu";
    [SerializeField] private string menu3 = "SettingsMenu";

    [SerializeField] private string stage1 = "PrototypeFarmStage";
    [SerializeField] private string stage2 = "PrototypeCrystalStage";
    [SerializeField] private string stage3 = "PrototypeRiverStage";
    [SerializeField] private int stageID = 1;

    #region audioclips
 //   public AudioClip[] Attack;

    #endregion audioclips

    [SerializeField] public AudioClip[] Footsteps;
    [SerializeField] private AudioSource SSource1;

    //This script will handle starting and stopping the game, interacts with players, match timer, and canvas elements
    void Start()
    {
        mtscript = gameObject.GetComponent<MenuTransitionScript>();
        buttonPos = 3;
        if (mainCam == null)
        {
            mainCam = Camera.main;
        }

        Debug.Log("Working on Start");
    //    countdownController = GameObject.FindWithTag("CountdownManager").GetComponent<Animator>();
 
     //   Player1.GetComponent<PlayerControllerScript>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            confirmCase();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            buttonPos += 1;
            checkCase();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            buttonPos -= 1;
            checkCase(); 
        }


    }

    public void confirmCase()
    {

        switch(buttonPos)
        {
            case 3:
                StartButtonFunction();
                
                //SceneManager.LoadScene(menu1);
                break;
            case 2:
                CreditsButtonFunction();       
                break;
            case 1:
                SettingsButtonFunction();              
                break;
            case 0:
                ExitButtonFunction();            
                break;
            default:
                Debug.Log("Something broke");
                break;

        }
    }

    public void checkCase()
    {

        switch (buttonPos)
        {
            case 3:
                //select 1st button
                CleanSelection();
                button3select.SetActive(true);
                break;
            case 2:
                //select 1st button
                CleanSelection();
                button2select.SetActive(true);
                break;
            case 1:
                //select 1st button


                CleanSelection();
                button1select.SetActive(true);
                break;
            case 0:
                //select 1st button
                CleanSelection();
                button0select.SetActive(true);
                break;
            case -1:
                //reset to 3
                buttonPos = 3;
                CleanSelection();
                button3select.SetActive(true);

                break;
            case 4:
                //reset to 3
                buttonPos = 0;
                CleanSelection();
                button0select.SetActive(true);
                break;
            default:
                Debug.Log("Something broke");
                break;

        }
    }
    public void loadotherstage()
    {
        Debug.Log("Called");
        currstage = SceneManager.GetActiveScene();
        if (currstage.name == stage1)
        {
            SceneManager.LoadScene(stage2);
        }
        else if (currstage.name == stage2)           //currstage.name == stage2
        {
            SceneManager.LoadScene(stage1);
        }/*
        else if (currstage.name == stage3)           //currstage.name == stage2
        {
            SceneManager.LoadScene(stage1);
        }*/

    }

    public void CleanSelection()
    {
        button3select.SetActive(false);
        button2select.SetActive(false);
        button1select.SetActive(false);
        button0select.SetActive(false);
    }

    public void PlayAudio(AudioClip targetclip)
    {
     //   SSource4.clip = targetclip;
     //   SSource4.Play();
    }

    public void PlayMusic(AudioClip targetmusic)
    {
     //   MusicSource.clip = targetmusic;
      //  MusicSource.Play();
    }

    public void StartButtonFunction()
    {
        mtscript.MoveCameraTarget("Main");
        Debug.Log("pressed start button");
    }

    public void CreditsButtonFunction()
    {
        mtscript.MoveCameraTarget("Credits");
        Debug.Log("pressed credits button");
    }

    public void SettingsButtonFunction()
    {
        mtscript.MoveCameraTarget("Settings");
        Debug.Log("pressed settings button");
    }

    public void ExitButtonFunction()
    {
        Debug.Log("pressed exit button");
        Application.Quit();
    }

    public void StartButtonHover()
    {
        buttonPos = 3;
        checkCase();
    }

    public void CreditsButtonHover()
    {
        buttonPos = 2;
        checkCase();
    }

    public void SettingsButtonHover()
    {
        buttonPos = 1;
        checkCase();
    }

    public void ExitButtonHover()
    {
        buttonPos = 0;
        checkCase();
    }
}