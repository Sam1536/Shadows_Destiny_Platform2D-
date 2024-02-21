using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtomPuzzleLevel02 : MonoBehaviour
{
    private Animator anim;
    public Animator barrierAnim;

    public float radius;
    public LayerMask layer;


    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        OnCollision();
    }

    void OnPressed()
    {
        anim.SetBool("ispressed",true);
        barrierAnim.SetBool("down",true);
    }
    void OnExit()
    {
        anim.SetBool("ispressed",false);
        barrierAnim.SetBool("down",false);
    }

    void OnCollision()
    {

        Collider2D hit = Physics2D.OverlapCircle(transform.position, radius, layer);//cria o gizmo do ponto do attack do player

        if (hit != null)
        {
            OnPressed();
            hit = null;
        }
        else
        {
            OnExit();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }



    ////retorna enquanto um objeto está em colisão com outro
    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    if(collision.gameObject.CompareTag("Stone"))
    //    {
    //        OnPressed();
    //    }
    //}

    ////retorna quando um objeto sai de colisão com outro
    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Stone"))
    //    {
    //        OnExit();
    //    }
    //}
}
