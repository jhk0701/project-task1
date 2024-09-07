using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour {
    
    protected float healthPoint;
    public enum State : int {
        Idle = 0,
        Dead = 1,
        Engage = 2
    }

    public State state;

    public abstract void Damage(float val, Unit subject);
    protected abstract void OnDead();

    protected abstract void Attack();
    public abstract void HitEvent(int id = 0);
}
