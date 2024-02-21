using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Rigidbody2D rig;
    public Animator anim;
    public Transform point;
    private PlayerAudio playerAudio;

    public LayerMask enemyLayer;

    private Health healthSystem;

    public float recoveryTime;
    public float radius;
    public float speed;
    public float jumpForce;
    private bool isJump;
    private bool doubleJump;
    private bool isAttack;
    private bool recovery;

    [Header("UI")]
    public Text scoreText;

    public GameObject gameOver;
  

    public static Player instancePlayer;
    private void Awake()
    {
        if (instancePlayer == null)
        {
            instancePlayer = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instancePlayer != this)
        {
            Destroy(instancePlayer.gameObject);
            instancePlayer = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        playerAudio = GetComponent<PlayerAudio>();
        healthSystem = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        JumpPlayer();
        AttackPlayer();
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        // se não pressionar nada, retorna 0. se pressionar direita,retorna 1. se esquerda, retorna -1
       float movement = Input.GetAxis("Horizontal");

        rig.velocity = new Vector2(movement * speed, rig.velocity.y);

        if(movement > 0)
        {
            if (!isJump && !isAttack)
            {
                anim.SetInteger("Transition", 1);
            }
          
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        if (movement < 0)
        {
            if (!isJump && !isAttack)
            {
                anim.SetInteger("Transition", 1);
            }
           
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        if (movement == 0 && !isJump && !isAttack)
        {
            anim.SetInteger("Transition", 0);
        }
       
    } 

    void JumpPlayer()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (!isJump)
            {
                anim.SetInteger("Transition", 2);
                rig.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                isJump = true;
                doubleJump = true;
                playerAudio.PlayAudio(playerAudio.jumpSound);
            }
            else if (doubleJump)
            {
                anim.SetInteger("Transition", 2);
                rig.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                doubleJump = false;
                playerAudio.PlayAudio(playerAudio.jumpSound);
            } 
            
        }
       
    }

    void AttackPlayer()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            isAttack = true;
            anim.SetInteger("Transition", 3);
            Collider2D hit = Physics2D.OverlapCircle(point.position, radius, enemyLayer);//cria o gizmo do ponto do attack do player
            playerAudio.PlayAudio(playerAudio.attackSound);
            
            if (hit != null)
            {

                if (hit.GetComponent<Slime>())
                {
                    hit.GetComponent<Slime>().OnHit();
                }
                if (hit.GetComponent<Goblin>())
                {
                    hit.GetComponent<Goblin>().OnHit();
                }


            }

            StartCoroutine(OnAttack());
        }
    }

    public void OnHit()
    {

        if (!recovery)
        {
            playerAudio.PlayAudio(playerAudio.hitSound);
            anim.SetTrigger("hit");
            healthSystem.health--;

            if (healthSystem.health < 0)
            {
                recovery = true;
                speed = 0;
                anim.SetTrigger("dead");
               
                //Game over aqui
                GamerController.instance.ShowGameOver();
            }
            else
            {
                StartCoroutine(Recover());
            }
        }
        
    }

    IEnumerator Recover()
    {
        recovery = true;
        yield return new WaitForSeconds(recoveryTime);
        recovery = false;
    }

    IEnumerator OnAttack()
    {
        yield return new WaitForSeconds(.33f);
        isAttack = false;
    }

    private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(point.position, radius);
        }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if(collision.gameObject.layer == 6 || collision.gameObject.layer == 11)
        {
            isJump = false;
        }

        if (collision.gameObject.layer == 10)
        {
            PlayerPos.instancePlay.CheckPoint();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            OnHit();
        }

        if (collision.CompareTag("Coin"))
        {
            playerAudio.PlayAudio(playerAudio.coinSound);
            collision.GetComponent<Animator>().SetTrigger("hit");
            GamerController.instance.GetCoin();
            Destroy(collision.gameObject, .5f);
        }

       
        
    }


}
