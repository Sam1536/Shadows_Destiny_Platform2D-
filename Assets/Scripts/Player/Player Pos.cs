using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPos : MonoBehaviour
{
    private Transform player;

    public static PlayerPos instancePlay;


    // Start is called before the first frame update
    void Start()
    {
        instancePlay = this;

        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (player != null)
        {

            CheckPoint();
        }
    }

    public void CheckPoint()
    {
            Vector3 PlayerPos = transform.position;
            PlayerPos.z = 0;

            player.position = PlayerPos;
        
    }

}
