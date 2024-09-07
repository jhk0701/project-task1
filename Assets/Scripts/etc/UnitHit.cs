using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHit : MonoBehaviour
{    
    Unit _subject;
    float _damage;
    void OnEnable()
    {
        Invoke("Disable", 1f);
    }

    public void Init(Unit u, float d){
        _subject = u;
        _damage = d;
    }

    public void SetDamage(float d){
        _damage = d;
    }

    void Disable(){
        gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other) {
        Unit u = other.gameObject.GetComponent<Unit>();
        if(u != null && !u.Equals(_subject))        
            u.Damage(_damage * Random.Range(0.9f, 1.1f), _subject);
    }
}
