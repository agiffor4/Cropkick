using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerScript : MonoBehaviour
{
    GameManagerScript gmscript;
    private PlayerInventoryScript invscript;
    private MultiTargetCam camscript;
    public float movespeed;
    public float defaultmovespeed;
    public Animator animator;
    private Animator cutanimator;
    public Rigidbody2D player_rb;
    [SerializeField] private KeyCode dodgeButton;
    [SerializeField] private KeyCode attackButton;
    [SerializeField] private KeyCode interactButton;
    [SerializeField] private KeyCode ability1button;
    [SerializeField] private KeyCode ability2button;
    [SerializeField] private KeyCode ability3button;
    private Vector3 lastPosition;
    private Vector3 currPosition;
    private float comboTiming;
    private float timesincelastpress;
    private bool combotimeractive;
    private bool isMoving;
    [SerializeField] private GameObject hitbox1;
    [SerializeField] private GameObject hitbox2;
    [SerializeField] private GameObject hitbox3;
    [SerializeField] private GameObject interactNode;

    private int emptyplotCount;
    private int unwateredplotCount;
    private int readyplotCount;
    [SerializeField] float detectionRange = 2.0f;

    public GameObject InteractNodeS;
    public GameObject InteractNodeW;
    public GameObject InteractNodeH;

    public GameObject laserbuild;
    public GameObject flowerbuild;

    public bool faceRight;
    [SerializeField] private Transform spawnPoint;

    public SpriteRenderer playerspriteRender;
    public Sprite currSprite;
    public GameObject DodgeEffect;
    public GameObject AbilityEffect;
    public int dodgeimagenumber;
    public float faceDirection; //-1.0 is left, 1.0 is right 
                                //    public GameObject Ability1;

    [SerializeField] private bool canDodge;
    [SerializeField] private bool canAttack;
    [SerializeField] private bool canMove;
    [SerializeField] private bool canDo;
    [SerializeField] private bool canAbilityA;
    [SerializeField] private bool canAbilityB;
    [SerializeField] private bool canAbilityC;
    [SerializeField] private float dodgecooldownTime;

    private Text abilityAtext;
    private Text abilityBtext;
    private Text abilityCtext;
    private GameObject abilityAcooldownindicator;
    private GameObject abilityBcooldownindicator;
    private GameObject abilityCcooldownindicator;


    private int Ysortingvalue;
    [SerializeField] private int priority;

    public float stuntime;
    public bool knockDirection;

    public enum PlayerState { Base, Stunned, Knockedback, Dodging, Attacking };
    private enum CharacterType { Terra, Dave, Duster, Harvest };
    [SerializeField] private CharacterType currCharacter;
    private string characterName;

    public PlayerState currState;
    public int playerNumber;
    public string PlayerID;
    private Rigidbody2D myrigidbody2D;

    public class Ability
    {
        public Object abilityObject;
        public int abilityID;
        public string abilityName;
        public int abilityCost;
        public float abilityBuildUp;
        public float abilityCoolDown;

    }

    public Ability abilityA = new Ability();
    public Ability abilityB = new Ability();
    public Ability abilityC = new Ability();



    // Use this for initialization
    void Start()
    {
        InitializeCharacterAbilities();
        characterName = currCharacter.ToString();
        camscript = GameObject.FindWithTag("MainCamera").GetComponent<MultiTargetCam>();
        invscript = gameObject.GetComponent<PlayerInventoryScript>();
        currState = PlayerState.Base;
        changeState(PlayerState.Base);
        myrigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        PlayerID = "Player" + playerNumber;
        print(PlayerID);
        gmscript = GameObject.FindWithTag("GameManager").GetComponent<GameManagerScript>();
        Debug.Log(PlayerID + " attack button is " + attackButton);
        Debug.Log(PlayerID + " dodge button is " + dodgeButton);
        dodgeimagenumber = 4;
        movespeed = defaultmovespeed;
  

        abilityAtext = GameObject.FindWithTag("P" + playerNumber + "_AbilityA_UI").GetComponentInChildren<Text>();
        abilityAcooldownindicator = GameObject.FindWithTag("P" + playerNumber + "_AbilityA_UI");
        abilityBtext = GameObject.FindWithTag("P" + playerNumber + "_AbilityB_UI").GetComponentInChildren<Text>();
        abilityBcooldownindicator = GameObject.FindWithTag("P" + playerNumber + "_AbilityB_UI");
        abilityCtext = GameObject.FindWithTag("P" + playerNumber + "_AbilityC_UI").GetComponentInChildren<Text>();
        abilityCcooldownindicator = GameObject.FindWithTag("P" + playerNumber + "_AbilityC_UI");
        abilityAcooldownindicator.SetActive(false);
        abilityBcooldownindicator.SetActive(false);
        abilityCcooldownindicator.SetActive(false);

        // Connect the player to its sprite renderer and animator
        animator = this.GetComponent<Animator>();
        cutanimator = GameObject.FindWithTag("CutInManager").GetComponent<Animator>();
        playerspriteRender = this.GetComponent<SpriteRenderer>();

        // Defaults for bools
        combotimeractive = false;
        isMoving = false;
        canDodge = true;
        canAttack = true;
        canMove = true;
        canAbilityA = true;
        canAbilityB = true;
        canAbilityC = true;
        faceRight = false;


    }

    // Update is called once per frame
    void Update()
    {
      
        playerspriteRender = gameObject.GetComponent<SpriteRenderer>();
        Ysortingvalue = Mathf.RoundToInt(transform.position.y) + 10 + priority;
        playerspriteRender.sortingOrder = Ysortingvalue;

        currSprite = playerspriteRender.sprite;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            DevCrop();
            invscript.updateText();
        }

        if (canMove == true)
        {
            Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal" + playerNumber), Input.GetAxisRaw("Vertical" + playerNumber), 0.0f);
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Magnitude", movement.magnitude);
            transform.position = transform.position + movement * Time.deltaTime * movespeed;
            currPosition = transform.position;

            if (movement.x > 0.1f)
            {
                if (faceRight != true)
                {
                    Flip();
                }
                faceDirection = 1.0f;
                // animator.SetFloat("AttackDirection", 0.0f);
                // animator.SetFloat("VerticalIdle", 0.0f);
                // animator.SetFloat("HorizontalIdle", 1.0f);

            }
            else if (movement.x < -0.1f)
            {
                if (faceRight != false)
                {
                    Flip();
                }
                faceDirection = -1.0f;
                // animator.SetFloat("AttackDirection", 0.0f);
                // animator.SetFloat("VerticalIdle", 0.0f);
                // animator.SetFloat("HorizontalIdle", -1.0f);
            }

            if (movement.x != 0.0f || movement.y != 0.0f)
            {
                isMoving = true;
            }
            else
            {
                isMoving = false;
            }

        }

        // Combotimer parameters are used to manage a number that runs while doing basic attacks. It runs out over time, so you can only perform the combo chain in quick succession
        if (combotimeractive == true)
        {
            timesincelastpress += 1 * Time.deltaTime;
        }

        if (comboTiming < 0)
        {
            comboTiming = 0;
        }

        if (Input.GetKeyDown(attackButton))
        {
            if (canAttack == true)
            {
                comboTiming += 1;

                if (comboTiming <= 1 && timesincelastpress < 1)
                {
                    performAttack(hitbox1, 0.2f);
                }
                else if (comboTiming <= 2 && comboTiming > 1 && timesincelastpress < 1)
                {
                    performAttack(hitbox2, 0.2f);
                }
                else if (comboTiming <= 3 && comboTiming > 2 && timesincelastpress < 1)
                {
                    performAttack(hitbox3, 0.4f);
                    comboTiming = 0;
                }
                else
                {
                    ResetCombo();
                    performAttack(hitbox1, 0.2f);
                    combotimeractive = true;
                }
            }
        }

        if (Input.GetKeyDown(interactButton))
        {  
            gmscript.PlayAudioOverlap(gmscript.Interact);
            animator.SetTrigger("TerraGarden");
            InteractwithPlots();
        }

        if (Input.GetKeyDown(ability1button))
        {
            if (canAbilityA == true)
            {
                ActivateAbility1(abilityA.abilityObject, abilityA.abilityCost, abilityA.abilityBuildUp, abilityA.abilityCoolDown);
            }


        }

        if (Input.GetKeyDown(ability2button))
        {
            if (canAbilityB == true)
            {
                ActivateAbility2(abilityB.abilityObject, abilityB.abilityCost, abilityB.abilityBuildUp, abilityB.abilityCoolDown, abilityB.abilityID);
            }

        }

        if (Input.GetKeyDown(ability3button))
        {
            if (canAbilityC == true)
            {
                ActivateAbility3(abilityC.abilityObject, abilityC.abilityCost, abilityC.abilityBuildUp, abilityC.abilityCoolDown, abilityC.abilityID);
            }
        }

        // Dodge button only works while moving (default set to X). The coroutine makes an afterimage effect, the function itself changes the player movement speed to faster until they travel a set distance. Invulnerable status will be added here later
        if (Input.GetKeyDown(dodgeButton))
        {
            if (isMoving == true && canDodge == true)
            {
                gmscript.PlayAudioOverlap(gmscript.Dodge);
                StartCoroutine(DodgeTime());
                StartCoroutine(DodgeAfterImageTimer());
                StartCoroutine(DodgeCooldown());
                GetLastPosition();

            }
            else if (isMoving == false)
            {
                return;
            }
        }
    }


    #region functions
   
    //commands for script checking last position (used for dodge commond)
    public void GetLastPosition()
    {
        lastPosition = transform.position;
        // print(lastPosition);
    }

    public void InteractwithPlots()
    {
        GameObject[] plotse = GameObject.FindGameObjectsWithTag("PlotE");
        GameObject[] plotsuw = GameObject.FindGameObjectsWithTag("PlotUW");
        GameObject[] plotsr = GameObject.FindGameObjectsWithTag("PlotR");
      
        foreach (GameObject item in plotse)
        {
            if (Vector2.Distance(item.transform.position, transform.position) < detectionRange)
            {
                emptyplotCount += 1;
         
            }
        }

        foreach (GameObject item in plotsuw)
        {
            if (Vector2.Distance(item.transform.position, transform.position) < detectionRange)
            {
                unwateredplotCount += 1;

            }
        }

        foreach (GameObject item in plotsr)
        {
            if (Vector2.Distance(item.transform.position, transform.position) < detectionRange)
            {
                readyplotCount += 1;

            }
        }

  
        int highestcount = (Mathf.Max(emptyplotCount, unwateredplotCount, readyplotCount));

        if(highestcount == emptyplotCount)
        {
            Instantiate(InteractNodeS, transform.position, transform.rotation);
        }       
        
        else if (highestcount == unwateredplotCount)
        {
            Instantiate(InteractNodeW, transform.position, transform.rotation);
  
        
        }

        else if (highestcount == readyplotCount)
        {
            Instantiate(InteractNodeH, transform.position, transform.rotation);
        }
        StartCoroutine(InteractTime());
        ResetPlotCounts();
    }

    public void ResetPlotCounts()
    {
        emptyplotCount = 0;
        unwateredplotCount = 0;
        readyplotCount = 0;
}

    //Resets the number used for attack chain combo
    public void ResetCombo()
    {
        comboTiming = 1;
        timesincelastpress = 0;
        combotimeractive = false;
    }

    public void ActivateAbility1(Object obj, int cost, float build, float cooldown)
    {
        if (invscript.InvCropCount - cost < 0)
        {
            print("NOT ENOUGH CASH! Stranger....");

        } else //if (invscript.InvCropCount >= cost)
        {
            invscript.InvCropCount -= cost;
            invscript.updateText();

            Instantiate(AbilityEffect, currPosition, Quaternion.identity);
            // camscript.Begin(build, gameObject);
            StartCoroutine(AbilityTimerA(cooldown));
            StartCoroutine(AbilityTimingA(obj, build, cooldown));
            // StartCoroutine(TimeStunned(build));
        }
    }

    public void ActivateAbility2(Object obj, int cost, float build, float cooldown, int id)
    {
        if (invscript.InvCropCount - cost < 0)
        {
            print("NOT ENOUGH CASH! Stranger....");

        }
        else //if (invscript.InvCropCount >= cost)
        {
            invscript.InvCropCount -= cost;
            invscript.updateText();
            if (id == 2)
            {
                Instantiate(laserbuild, spawnPoint.position, spawnPoint.rotation);
            }
                Instantiate(AbilityEffect, currPosition, Quaternion.identity);
            StartCoroutine(AbilityTimerB(cooldown));
            StartCoroutine(AbilityTimingB(obj, build, cooldown));
            // StartCoroutine(TimeStunned(0.4f));
        }

    }

    public void ActivateAbility3(Object obj, int cost, float build, float cooldown, int id)
    {
        if (invscript.InvCropCount - cost <= 0)
        {
            print("NOT ENOUGH CASH! Stranger....");

        }
        else if (invscript.InvCropCount >= cost)
        {
            invscript.InvCropCount -= cost;
            invscript.updateText();
            if (id == 3)
            {
                Instantiate(flowerbuild, spawnPoint.position, spawnPoint.rotation);
            }
            Instantiate(AbilityEffect, currPosition, Quaternion.identity);
            StartCoroutine(AbilityTimerC(cooldown));
            StartCoroutine(AbilityTimingC(obj, build, cooldown));
            //  StartCoroutine(TimeStunned(0.4f));
        }

    }

    public void DevCrop()
    {
        invscript.InvCropCount += 10;
    }

    public void HitStunned(float stuntime)
    {
        if (stuntime <= 0)
        {
            movespeed = defaultmovespeed;
        }
        else
        {
            movespeed = 0;
            stuntime -= Time.deltaTime;
        }
    }

    public void KnockedBack(float knockback, float knockbacktime)
    {
        if (knockbacktime > 0)
        {


            StartCoroutine(KnockbackTime(knockbacktime));
            if (knockDirection == true)
            {
                animator.SetFloat("HurtDirection", 1.0f);
                myrigidbody2D.velocity = new Vector2(knockback, 0);
                //myrigidbody2D.AddForce(new Vector2(10, 0) * knockbackdistance);
                //transform.position = transform.position + new Vector3(knockback, 0) * Time.deltaTime * knockbackLength;
            }
            else
            {
                animator.SetFloat("HurtDirection", 0.0f);
                myrigidbody2D.velocity = new Vector2(-knockback, 0);
                // myrigidbody2D.AddForce(new Vector2(-10, 0) * knockbackdistance);
                // transform.position = transform.position + new Vector3(-knockback, 0) * Time.deltaTime * knockbackLength;
            }
        }
    }

    public void KnockbackReset()
    {
        myrigidbody2D.velocity = Vector2.zero;//new Vector2(movespeed, myrigidbody2D.velocity.y);
                                              //  myrigidbody2D.angularVelocity = Vector3.zero;
    }

    public void performAttack(GameObject attacktype, float attackcooldown)
    {
        //  Instantiate((attacktype), transform.position + new Vector3(faceDirection, 0.0f), Quaternion.identity);
        Instantiate((attacktype), spawnPoint.position, spawnPoint.rotation);
        StartCoroutine(AttackAnimationFlow(attackcooldown));
        StartCoroutine(AttackCooldown(attackcooldown));
        combotimeractive = true;
        timesincelastpress = 0;
    }

    public void changeState(PlayerState newState)
    {
        switch (currState) //on exit
        {
            case PlayerState.Base:
                break;
            case PlayerState.Stunned:
                break;
            case PlayerState.Knockedback:
                break;
            case PlayerState.Dodging:
                break;
            case PlayerState.Attacking:
                break;
            default:
                break;
        }
        currState = newState;
        switch (currState) //on enter
        {
            case PlayerState.Base:
                print("Base");
                break;
            case PlayerState.Stunned:
                print("Stunned");
                break;
            case PlayerState.Knockedback:
                print("Knocked");
                break;
            case PlayerState.Dodging:
                print("Dodging");
                break;
            case PlayerState.Attacking:
                print("Attacking");
                break;
            default:
                break;
        }
    }

    public void InitializeCharacterAbilities()
    {
        if (currCharacter == CharacterType.Terra)
        {
            SetTerra();
        }
        else if (currCharacter == CharacterType.Dave)
        {
            SetDave();
        }
        else if (currCharacter == CharacterType.Duster)
        {
            SetDuster();
        }
        else if (currCharacter == CharacterType.Harvest)
        {
            SetHarvest();
        }
    }

    public void SetTerra()
    {

        abilityA.abilityObject = Resources.Load<Object>("Abilities/Terra/AbilityA_P" + playerNumber);
        abilityA.abilityID = 1;
        abilityA.abilityCost = 0;
        abilityA.abilityBuildUp = 0.1f;
        abilityA.abilityName = "Watering Can";
        abilityA.abilityCoolDown = 2.0f;

        abilityB.abilityObject = Resources.Load<Object>("Abilities/Terra/AbilityB_P" + playerNumber);
        abilityB.abilityID = 2;
        abilityB.abilityCost = 5;
        abilityB.abilityBuildUp = 0.2f;
        abilityB.abilityName = "Little Miss Sunshine";
        abilityB.abilityCoolDown = 5.0f;

        abilityC.abilityObject = Resources.Load<Object>("Abilities/Terra/AbilityC_P" + playerNumber);
        abilityC.abilityID = 3;
        abilityC.abilityCost = 15;
        abilityC.abilityBuildUp = 1.0f;
        abilityC.abilityName = "Bloom Burst";
        abilityC.abilityCoolDown = 10.0f;
    }

    public void SetDave()
    {
        defaultmovespeed = defaultmovespeed + 2;
        abilityA.abilityObject = Resources.Load<Object>("Abilities/Dave/AbilityA_P" + playerNumber);
        abilityA.abilityID = 4;
        abilityA.abilityCost = 1;
        abilityA.abilityBuildUp = 0.0f;
        abilityA.abilityName = "Carrot Kunai";
        abilityA.abilityCoolDown = 1.0f;

        abilityB.abilityObject = Resources.Load<Object>("Abilities/Dave/AbilityB_P" + playerNumber);
        abilityB.abilityID = 5;
        abilityB.abilityCost = 4;
        abilityB.abilityBuildUp = 0.2f;
        abilityB.abilityName = "Aqua Jutsu";
        abilityB.abilityCoolDown = 5.0f;

        abilityC.abilityObject = Resources.Load<Object>("Abilities/Dave/AbilityC_P" + playerNumber);
        abilityC.abilityID = 6;
        abilityC.abilityCost = 8;
        abilityC.abilityBuildUp = 0.1f;
        abilityC.abilityName = "Crop Tag";
        abilityC.abilityCoolDown = 8.0f;
    }

    public void SetDuster()
    {
        print("ERROR: CHARACTER UNAVAILABLE");
    }

    public void SetHarvest()
    {
        print("ERROR: CHARACTER UNAVAILABLE");
    }

    public void Flip()
    {
        faceRight = !faceRight;
        transform.Rotate(0, 180f, 0f);
    }
    #endregion functions


    #region numerators
    public IEnumerator DodgeTime()
    {
        canDodge = false;
        movespeed = 15;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(0.2f);
        movespeed = defaultmovespeed;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;

    }

    public IEnumerator DodgeCooldown()
    {

        yield return new WaitForSeconds(dodgecooldownTime);
        canDodge = true;

    }

    //Coroutine for the after image effect on dodges
    public IEnumerator DodgeAfterImageTimer()
    {
        for (int i = 0; i < dodgeimagenumber; i++)
        {
            Instantiate(DodgeEffect, currPosition, Quaternion.identity);
            yield return new WaitForSeconds(0.04f);

        }

    }

    public IEnumerator AttackCooldown(float attackcooldowntime)
    {
        canAttack = false;
        yield return new WaitForSeconds(attackcooldowntime);
        canAttack = true;
    }

    public IEnumerator AttackAnimationFlow(float animationlength)
    {
        animator.SetBool("Attack", true);
        yield return new WaitForSeconds(animationlength);
        animator.SetBool("Attack", false);
    }

    public IEnumerator TimeStunned(float stuntime)
    {
        animator.SetBool("Hurt", true);
        canMove = false;
        canDodge = false;
        canAttack = false;
        yield return new WaitForSeconds(stuntime);
        canMove = true;
        canDodge = true;
        canAttack = true;
        animator.SetBool("Hurt", false);
    }

    public IEnumerator KnockbackTime(float knockbacktime)
    {

        yield return new WaitForSeconds(knockbacktime);
        KnockbackReset();
    }

    //  cutanimator.SetTrigger("CUTINTRIGGER");

    // cutanimator.SetTrigger("CUTOUTRIGGER");

    public IEnumerator AbilityTimingA(Object obj, float abilitybuildup, float cool)
    {
        abilityAcooldownindicator.SetActive(true);
        canAbilityA = false;
        canMove = false;
        canDodge = false;
        canAttack = false;
        yield return new WaitForSeconds(abilitybuildup);
        Instantiate(obj, spawnPoint.position, spawnPoint.rotation);
        canMove = true;
        canDodge = true;
        canAttack = true;
        yield return new WaitForSeconds(cool);
        canAbilityA = true;
        abilityAcooldownindicator.SetActive(false);
    }

    public IEnumerator AbilityTimerA(float cool)
    {

        while (cool > 0)
        {
            //  Debug.Log("Countdown: " + currCountdownValue);
            abilityAtext.text = "" + cool;
            yield return new WaitForSeconds(1.0f);
            cool--;
            abilityAtext.text = "" + cool;
        }
    }

    public IEnumerator AbilityTimingB(Object obj, float abilitybuildup, float cool)
    {
        abilityBcooldownindicator.SetActive(true);
        canAbilityB = false;
        canMove = false;
        canDodge = false;
        canAttack = false;
        yield return new WaitForSeconds(abilitybuildup);
        Instantiate(obj, spawnPoint.position, spawnPoint.rotation);
        canMove = true;
        canDodge = true;
        canAttack = true;
        yield return new WaitForSeconds(cool);
        canAbilityB = true;
        abilityBcooldownindicator.SetActive(false);
    }

    public IEnumerator AbilityTimerB(float cool)
    {
        while (cool > 0)
        {
            //  Debug.Log("Countdown: " + currCountdownValue);
            abilityBtext.text = "" + cool;
            yield return new WaitForSeconds(1.0f);
            cool--;
            abilityBtext.text = "" + cool;
        }
    }

    public IEnumerator AbilityTimingC(Object obj, float abilitybuildup, float cool)
    {
        abilityCcooldownindicator.SetActive(true);
        canAbilityC = false;
        canMove = false;
        canDodge = false;
        canAttack = false;
        yield return new WaitForSeconds(abilitybuildup);
        Instantiate(obj, spawnPoint.position, spawnPoint.rotation);
        canMove = true;
        canDodge = true;
        canAttack = true;
        yield return new WaitForSeconds(cool);
        canAbilityC = true;
        abilityCcooldownindicator.SetActive(false);
    }

    public IEnumerator AbilityTimerC(float cool)
    {
        while (cool > 0)
        {
            //  Debug.Log("Countdown: " + currCountdownValue);
            abilityCtext.text = "" + cool;
            yield return new WaitForSeconds(1.0f);
            cool--;
            abilityCtext.text = "" + cool;
        }
    }

    private IEnumerator InteractTime()
    {
        canAbilityA = false;
        canAbilityB = false;
        canAbilityC = false;
        canMove = false;
        canDodge = false;
        canAttack = false;
        yield return new WaitForSeconds(0.2f);
        canAbilityA = true;
        canAbilityB = true;
        canAbilityC = true;
        canMove = true;
        canDodge = true;
        canAttack = true;
        animator.SetTrigger("IdleTrigger");

    }

    public IEnumerator UltBuild(float abilitybuildup, GameObject abobject)
    {
        cutanimator.SetTrigger("CUTINTRIGGER");
        yield return new WaitForSeconds(abilitybuildup);
        Instantiate(abobject, currPosition, Quaternion.identity);
        cutanimator.SetTrigger("CUTOUTRIGGER");
    }

    #endregion numerators



}
