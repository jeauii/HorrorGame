using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class EffectControl : NetworkBehaviour
{
    private Animator animator;
    private AudioSource[] audios;
    private AudioSource Footsteps;
    private AudioSource Background;
    private float dist;
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        audios = GetComponents<AudioSource>();
        Footsteps = audios[0];
        Background = audios[1];
        animator = GetComponent<Animator>();
    }
    private int checkDirection (float distTemp) 
    {
        if (distTemp < dist) 
        {
            dist = distTemp;
            return 1;
        } 
        else if (distTemp > dist) 
        { 
            dist = distTemp;
            return -1;
        }
        else
            return 0;
    }

    private void backgroundMusicDim()
    {
        if(Background.isPlaying)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.position);
            int condition = checkDirection(distanceToTarget);
            if(distanceToTarget <= 7)
            {
                if( condition == 1)
                {
                    Background.volume -= (1/(distanceToTarget)) * 0.01f;
                }   
                else if(condition == -1)
                {
                    Background.volume += (1/(distanceToTarget)) * 0.01f;
                }
            }
            else 
                Background.volume = 0.171f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (hasAuthority)
        {
            backgroundMusicDim();
            bool Left = false;
            bool Right = false;
            bool Fwd = false;
            bool Bck = false;

            if (Input.GetKey(KeyCode.A))
            {
                Left = true;
                animator.SetBool("walkLeft", true);
            }
            else if (Input.GetKeyUp(KeyCode.A))
            {
                animator.SetBool("walkLeft", false);
            }

            if (Input.GetKey(KeyCode.D))
            {
                Right = true;
                animator.SetBool("walkRight", true);
            }
            else if (Input.GetKeyUp(KeyCode.D))
            {
                animator.SetBool("walkRight", false);
            }

            if (Input.GetKey(KeyCode.S))
            {
                Footsteps.volume = 0.1f;
                Bck = true;
                animator.SetBool("walkBack", true);
            }
            else if (Input.GetKeyUp(KeyCode.S))
            {
                animator.SetBool("walkBack", false);
            }

            if (Input.GetKey(KeyCode.W))
            {
                Fwd = true;
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    Footsteps.volume = 0.1f;
                    animator.SetBool("runCondition", false);
                    animator.SetBool("walkForward", true);
                }
                else if (Input.GetKeyUp(KeyCode.LeftShift))
                {
                    animator.SetBool("walkForward", false);
                }
                else
                {
                    Footsteps.volume = 0.5f;
                    animator.SetBool("runCondition", true);
                }
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift) || (Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.W)))
            {
                Footsteps.volume = 0.5f;
                animator.SetBool("walkForward", false);
            }
            else if (Input.GetKeyUp(KeyCode.W))
            {
                animator.SetBool("runCondition", false);
            }

            float xAxis = Input.GetAxis("Horizontal");
            float zAxis = Input.GetAxis("Vertical");

            if ((xAxis != 0 || zAxis != 0) && !Footsteps.isPlaying)
            {
                Footsteps.Play();
            }

            if (Left == false && Right == false && Fwd == false && Bck == false)
            {
                Footsteps.Stop();
            }
        }
        
    }
}

