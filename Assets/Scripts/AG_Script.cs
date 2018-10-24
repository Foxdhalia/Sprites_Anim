using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AG_Script : MonoBehaviour {

    public Animator anim;
    bool b;

	void Update () {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            b = !b;
            anim.SetBool("IsJump", b);
        }
	}
}
