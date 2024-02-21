using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour
{
    public Transform point;
    public Transform pointBehind;
    private Rigidbody2D rig;
    private Animator anim;
    private AudioSource audioGoblin;
    
    private Vector2 direction;
    public bool isFront;
   
    public int health;
    private bool isDead;

    public float stopDistance;
    public bool isRight;
    public float speed;
    public float maxVision;

    public AudioClip hitGoblin;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioGoblin = GetComponent<AudioSource>();
        
        if (isRight)//vira para direita
        {
            transform.eulerAngles = new Vector2(0, 0);
            direction = Vector2.right;
           

        }
        else//vira para esquerda 
        {
            transform.eulerAngles = new Vector2(0, 180);
            direction = Vector2.left;
           

        }
    }

    

    void FixedUpdate()
    {
        GetPlayer();
        OnMove();

       
    }

    void  OnMove()
    {
        if (isFront && !isDead)
        {
            anim.SetInteger("transition", 1);
            if (isRight)//vira para direita
            {
                transform.eulerAngles = new Vector2(0, 0);
                direction = Vector2.right;
                rig.velocity = new Vector2(speed, rig.velocity.y);

            }
            else//vira para esquerda 
            {
                transform.eulerAngles = new Vector2(0, 180);
                direction = Vector2.left;
                rig.velocity = new Vector2(-speed, rig.velocity.y);

            }
        }

        
    }


    public void OnHit()
    {
        audioGoblin.PlayOneShot(hitGoblin);
        anim.SetTrigger("hit");
        health--;

        if (health < 0)
        {
            isDead = true;
            speed = 0;
            anim.SetTrigger("dead");
            Destroy(gameObject, 1f);
        }
    }

    void GetPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(point.position, direction, maxVision);

        if (hit.collider != null && !isDead)
        {
            if (hit.transform.CompareTag("Player"))
            {
                isFront = true;
                float distance = Vector2.Distance(transform.position, hit.transform.position);

                if (distance <= stopDistance)// distancia para atacar
                {
                    isFront = false;
                    rig.velocity = Vector2.zero;

                    anim.SetInteger("transition", 2);

                    hit.transform.GetComponent<Player>().OnHit();
                }
            }
            else
            {
                isFront = false;
                rig.velocity = Vector2.zero;
                anim.SetInteger("transition",0);
            }
           
        }

        RaycastHit2D hitBehind = Physics2D.Raycast(pointBehind.position, -direction, maxVision);

        if (hitBehind.collider != null)
        {
            if (hitBehind.transform.CompareTag("Player"))
            {
                //o player esta na costa do inimigo
                //Debug.Log("estou vendo");
                isRight = !isRight;
                isFront = true;
            }
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(point.position,direction * maxVision);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(pointBehind.position, -direction * maxVision);
    }


  
}
