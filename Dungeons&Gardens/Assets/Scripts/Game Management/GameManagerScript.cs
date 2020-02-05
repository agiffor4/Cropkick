using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public MatchTimerScript mtscript;
    public PlayerInventoryScript piscript;
    public PlayerInventoryScript piscript2;
    public PlayerHealthScript phscript;
    public PlayerHealthScript phscript2;

    public GameObject MenuPanel;
    public Transform p1startpos;
    public Transform p2startpos;

    public GameObject Player1;
    public GameObject Player2;
    private string winnername;

    public Text winnertext;
        public Text P1controlstext;
        public Text P2controlstext;

    public bool matchrunning;

    public int CropLossMin;
    public int CropLossMax;
    public float respawntime;

    public int FullMatchTime;

    public Animator countdownController;
    public Animator endgameController;
    public Animator p1winController;
    public Animator p2winController;
    public Animator tieController;


    public float animationLength_countdown;
    public float animationLength_endgame;
    public float animationLength_playervictory;
    public float animationLength_tie;

    public Camera mainCam;

    float shakeAmount = 0;


    private Scene currstage;
    [SerializeField] private string stage1 = "PrototypeFarmStage";
    [SerializeField] private string stage2 = "PrototypeCrystalStage";
    [SerializeField] private string stage3 = "";
    [SerializeField] private int stageID = 1;

    #region audioclips
    public AudioClip[] Attack;
    public AudioClip[] Damage;
    public AudioClip GrabCrop;
    public AudioClip Interact;
    public AudioClip Dodge;
    public AudioClip Death;
    public AudioClip Respawn;
    public AudioClip GameSetClip;
    public AudioClip GameStartClip;
    public AudioClip MusicMenu;
    public AudioClip MusicStage;
    #endregion audioclips

    [SerializeField] public AudioClip[] Footsteps;

    [SerializeField] private AudioSource SSource1;
    [SerializeField] private AudioSource SSource2;
    [SerializeField] private AudioSource SSource3;
    [SerializeField] private AudioSource SSource4;
    [SerializeField] private AudioSource MusicSource;




    //This script will handle starting and stopping the game, interacts with players, match timer, and canvas elements
    void Start()
    {
        if (mainCam == null)
            {
            mainCam = Camera.main;
        }

        Debug.Log("Working on Start");
       
        countdownController = GameObject.FindWithTag("CountdownManager").GetComponent<Animator>();
        endgameController = GameObject.FindWithTag("EndGameManager").GetComponent<Animator>();
        p1winController = GameObject.FindWithTag("P1VictoryManager").GetComponent<Animator>();
        p2winController = GameObject.FindWithTag("P2VictoryManager").GetComponent<Animator>();
        tieController = GameObject.FindWithTag("TieManager").GetComponent<Animator>();
        

        Player1.GetComponent<PlayerControllerScript>().enabled = false;
        Player2.GetComponent<PlayerControllerScript>().enabled = false;
        mtscript = GameObject.FindWithTag("MatchTimer").GetComponent<MatchTimerScript>();
        matchrunning = false;
        GameStart();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            MenuPanel.SetActive(true);
            PlayMusic(MusicMenu);
        }
    }



    public void Shake(float amount, float length)
    {
        shakeAmount = amount;
        InvokeRepeating("BeginShake", 0, 0.01f);
        Invoke("StopShake", length);
    }

    public void BeginShake()
    {
        if (shakeAmount > 0)
        {
            Vector3 camPos = mainCam.transform.position;
            float offsetX = Random.value * shakeAmount * 2 - shakeAmount;
            float offsetY = Random.value * shakeAmount * 2 - shakeAmount;
            camPos.x += offsetX;
            camPos.y += offsetY;

            mainCam.transform.position = camPos;
        }
    }

    public void StopShake()
    {
        CancelInvoke("BeginShake");
        mainCam.transform.localPosition = Vector3.zero;
    }

    public void GameOver()
    {
        Shake(0.3f, 0.5f);
        mtscript.TimerStop();
        mtscript.TimerReset();
        Debug.Log("Calling GameOver");
        StartCoroutine(EndGameAnimation());
    
    }

    public void GameplayerOver()
    {
        Debug.Log("Calling GameplayOver");
        updatewinnertext();
        matchrunning = false;
        Player1.GetComponent<PlayerControllerScript>().enabled = false;
        Player2.GetComponent<PlayerControllerScript>().enabled = false;
        

        GameReset();
        MenuPanel.SetActive(true);
    }

    public void GameReset()
    {
        /*
        StopCoroutine(CountdownAnimation());
        StopCoroutine(EndGameAnimation());
        StopCoroutine(Player1VictoryAnimation());
        StopCoroutine(Player2VictoryAnimation());
        StopCoroutine(TieAnimation());*/
        Debug.Log("Calling GameReset");
        piscript.updatetextreset();
        piscript2.updatetextreset();
        phscript.resetHealth();
        phscript2.resetHealth();
        Player1.transform.position = p1startpos.position;
        Player2.transform.position = p2startpos.position;
    }

    public void Player1RespawnReset()
    {
        phscript.resetHealth();
        Player1.transform.position = p1startpos.position;
    }

    public void Player2RespawnReset()
    {
        phscript2.resetHealth();
        Player2.transform.position = p2startpos.position;
    }


    public void GameStart()
    {
        MenuPanel.SetActive(false);
        countdownController.SetTrigger("StartCountdown");
        StartCoroutine(CountdownAnimation());
       
    }

    public void GameplayStart()
    {
        PlayMusic(MusicStage);
        mtscript.TimerStart();
   
        Player1.GetComponent<PlayerControllerScript>().enabled = true;
        Player2.GetComponent<PlayerControllerScript>().enabled = true;
        matchrunning = true;
        countdownController.ResetTrigger("StartCountdown");
        p1winController.ResetTrigger("p1start");
        p2winController.ResetTrigger("p2start");
        tieController.ResetTrigger("tiestart");
        endgameController.ResetTrigger("StartFlash");
    }

    public void updatewinnertext()
    {
        winnertext.text = "Last Round Winner: " + winnername; 
    }

    public void updatecontroltext()
    {
       // P1controlstext.text = "Attack: " + Player1.GetComponent<PlayerControllerScript>().attackButton;  //"\n Dodge: " + Player1.GetComponent<PlayerControllerScript>().dodgeButton "\n Interact: " + Player1.GetComponent<PlayerControllerScript>().interactButton);
      // P2controlstext.text = "Attack: " + Player2.GetComponent<Player2ControlScript>().attackButton;
    }

    /*  public void loadotherstage()
      {
          Debug.Log("Called");
          currstage = SceneManager.GetActiveScene();
          if(currstage.name == stage1)
          {
              SceneManager.LoadScene(stage2);
          }
          else if(currstage.name == stage2)           //currstage.name == stage2
          {
              SceneManager.LoadScene(stage1);
          }

      }*/

    public void backtomenu()
    {
        SceneManager.LoadScene("PrototypeMainMenu");
    }

    public void EndApplication()
    {
        Application.Quit();
    }



    public void PlayAudioOverlap(AudioClip targetclipoverlap)
    {
      //  if(SSource1.isPlaying == false)
      //  {
            SSource1.clip = targetclipoverlap;
            SSource1.Play();
      /*  }
        else if(SSource2.isPlaying == false)
        {
            SSource2.clip = targetclipoverlap;
            SSource1.Play();
        }
        else if(SSource3.isPlaying == false)
        {
            SSource3.clip = targetclipoverlap;
            SSource1.Play();
        }
        else
        {
            print("No vacancy");
        }*/
    }

    public void PlayAudio(AudioClip targetclip)
    {
        SSource4.clip = targetclip;
        SSource4.Play();
    }

    public void PlayMusic(AudioClip targetmusic)
    {
        MusicSource.clip = targetmusic;
        MusicSource.Play();
    }
    public IEnumerator MatchStartCountdown()
    {

        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<BoxCollider2D>().enabled = true;

    }

    public IEnumerator CountdownAnimation()
    {

        yield return new WaitForSeconds(animationLength_countdown - 1.4f);
        Shake(0.2f, 0.1f);
        yield return new WaitForSeconds(0.2f);
        Shake(0.2f, 0.2f);
        yield return new WaitForSeconds(0.4f);
        GameplayStart();
       

    }

    public IEnumerator EndGameAnimation()
    {
        Debug.Log("Calling EndGameAnimation");
        endgameController.SetTrigger("StartFlash");
        yield return new WaitForSeconds(animationLength_endgame);
        if (piscript.InvCropCount > Mathf.Max(piscript2.InvCropCount))
        {
            StartCoroutine(Player1VictoryAnimation());
            winnername = "Player 1";
      
        }
        else if (piscript2.InvCropCount > Mathf.Max(piscript.InvCropCount))
        {
            StartCoroutine(Player2VictoryAnimation());
            winnername = "Player 2";
          
        }
        else
        {
            StartCoroutine(TieAnimation());
            winnername = "Tie";
            print("Tie!");
        }

        endgameController.ResetTrigger("StartFlash");

    }

    public IEnumerator Player1VictoryAnimation()
    {
        p1winController.SetTrigger("p1start");
        yield return new WaitForSeconds(animationLength_playervictory);
        GameplayerOver();
    }

    public IEnumerator Player2VictoryAnimation()
    {
        p2winController.SetTrigger("p2start");
        yield return new WaitForSeconds(animationLength_playervictory);
        GameplayerOver();
    }

    public IEnumerator TieAnimation()
    {
        tieController.SetTrigger("tiestart");
        yield return new WaitForSeconds(0.1f);
        Shake(0.2f, 0.1f);
        yield return new WaitForSeconds(animationLength_tie);
        GameplayerOver();
    }
}
