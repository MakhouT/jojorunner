using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerashaker : MonoBehaviour
{
    public Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void shake()
    {
   
        anim.SetTrigger("Shake");

    }
}
