using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    [SerializeField] Animator _anim;
    int _idle;

    void Start()
    {
        InvokeRepeating("SetRandomIdle", 3f, 3f);
    }

    void SetRandomIdle(){
        _idle = Random.Range(0, 3);
        _anim.SetInteger("Idle", _idle);
        _anim.SetTrigger("Change");
    }
}
