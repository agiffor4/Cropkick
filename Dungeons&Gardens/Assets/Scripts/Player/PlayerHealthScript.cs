using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthScript : MonoBehaviour
{
    PlayerControllerScript pcscript;
    public float currHealth;
    public float maxHealth;

    private float healthforbar;
    public bool hitInvulstate;
    public HealthbarScript healthBar;
    private GameManagerScript gmscript;
    private PlayerInventoryScript invscript;
    [SerializeField] private GameObject droppedCrops;
    //public int thisplayerid;

    private float dropcountinstance;

    // Start is called before the first frame update
    void Start()
    {
        pcscript = GetComponent<PlayerControllerScript>();
        gmscript = GameObject.FindWithTag("GameManager").GetComponent<GameManagerScript>();
        invscript = gameObject.GetComponent<PlayerInventoryScript>();
        hitInvulstate = false;
        currHealth = maxHealth;
       // healthBar.SetSize(.4f);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void updateHealth(float damagevalue, float hitInvultime, float hitstun, float knockback, int teamnumber)
    {


        if (teamnumber != pcscript.playerNumber)
        {
            //pcscript.stuntime = hitstun;
            //pcscript.knockbackdistance = knockback;
            pcscript.KnockedBack(knockback, hitstun/2);
            StartCoroutine(pcscript.TimeStunned(hitstun));

            if (hitInvulstate == false){
               
                currHealth -= damagevalue;
            if (currHealth <= 0)
            {
                currHealth = 0;
                PlayerDeath();
            }
            healthforbar = currHealth / maxHealth;
            healthBar.SetSize(healthforbar);
            hitInvulstate = true;
                gmscript.PlayAudio(gmscript.Damage[Random.Range(0, gmscript.Damage.Length)]);
                gmscript.Shake(0.2f, 0.2f);
                StartCoroutine(InvulTime(hitInvultime));
        }
        else
        {
                return;
            //print("No damage");
        }

        }
    }

    public void PlayerDeath()
    {
        gmscript.PlayAudioOverlap(gmscript.Death);
        dropcountinstance = Mathf.Ceil(invscript.InvCropCount / 4);
        invscript.InvCropCount -= dropcountinstance;
        DropCropsOnDeath();
        invscript.updatetextrespawn();
        DisableSelf();
        StartCoroutine(RespawnTimer());
    }

    public void PlayerRespawn()
    {
        if (gameObject.tag == "Player")
        {
            gmscript.Player1RespawnReset();
       //     gameObject.GetComponent<PlayerControllerScript>().enabled = true;
        }
        else if (gameObject.tag == "Player2")
        {
            gmscript.Player2RespawnReset();
    //        gameObject.GetComponent<PlayerControllerScript>().enabled = true;
        }
        gmscript.PlayAudioOverlap(gmscript.Respawn);
        EnableSelf();
    }
    public void resetHealth()
    {
        currHealth = maxHealth;
        healthforbar = currHealth / maxHealth;
        healthBar.SetSize(healthforbar);
    }

    public IEnumerator InvulTime(float hitInvultime)
    {
        gmscript.PlayAudioOverlap(gmscript.Damage[Random.Range(0, gmscript.Damage.Length)]);
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(hitInvultime);
        hitInvulstate = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void DropCropsOnDeath()
    {
        if (invscript.InvCropCount >= dropcountinstance)
        {
            for (int i = 0; i < dropcountinstance; i++)
            {        
                Instantiate(droppedCrops, gameObject.transform.position + new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0), Quaternion.identity);
            }
        }
        invscript.updatetextrespawn();
    }

    public void DisableSelf()
    {
        gameObject.GetComponent<PlayerControllerScript>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void EnableSelf()
    {
        gameObject.GetComponent<PlayerControllerScript>().enabled = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    public IEnumerator RespawnTimer()
    {
        yield return new WaitForSeconds(gmscript.respawntime);
        PlayerRespawn();
    }


}
