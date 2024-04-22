using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimCtrl : MonoBehaviour
{
    public Animator anim;
    PlayerMove playermove;

    // Start is called before the first frame update
    void Start()
    {
        playermove = GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        WalkAnimUpdate();
    }

    void WalkAnimUpdate()
    {
        if (playermove.Iswalk == true)
            anim.SetBool("Walk", true);
        else
            anim.SetBool("Walk", false);
    }
}
