using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    private Rigidbody2D rig;
    public Transform point;
    private Animator anim;
    private AudioSource audioSlime;

    public LayerMask layer;

    public float health;
    public float speed;
    public float radius;

    public AudioClip hitSlime; 
   



    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        rig.velocity = new Vector2(speed, rig.velocity.y);
        OnCollisionSlime();
    }

    void OnCollisionSlime()
    {

        Collider2D hit = Physics2D.OverlapCircle(point.position, radius, layer);//cria o gizmo do ponto do attack do player

        if (hit != null)
        {
            //só é chamado quando o inimigo bater em um objeto que tenha a layer selecionado 
            speed = -speed;
            //Debug.Log("batteuuuu");

            if(transform.eulerAngles.y == 0)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
    }

    public void OnHit()
    {
        anim.SetTrigger("hit");
        health--;

        if (health < 0)
        {
            speed = 0;
            anim.SetTrigger("dead");
            Destroy(gameObject,1f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(point.position, radius);
    }

   


}