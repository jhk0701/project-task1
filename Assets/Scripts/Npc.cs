using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour, IInteractable
{
    [SerializeField] Animator _anim;
    int _idle;
    
    [SerializeField] KeyCode keyToInteract = KeyCode.G;
    [SerializeField] GameObject goGuide;

    void Start()
    {
        InvokeRepeating("SetRandomIdle", 5f, 5f);
    }

    void Update()
    {
        if (goGuide && 
            goGuide.activeInHierarchy && 
            Input.GetKeyUp(keyToInteract))
            Interact();
    }
    
    void SetRandomIdle(){
        _idle = Random.Range(0, 3);
        _anim.SetInteger("Idle", _idle);
        _anim.SetTrigger("Change");
    }



    public virtual void Interact(){
    }

    public void ActivateKey(){
        if(keyToInteract == KeyCode.None) return;
        goGuide.SetActive(true);
    }
    public void DeactivateKey(){
        if(keyToInteract == KeyCode.None) return;
        goGuide.SetActive(false);
    }



    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals("Player"))
            ActivateKey();
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag.Equals("Player"))
            DeactivateKey();
    }
}
