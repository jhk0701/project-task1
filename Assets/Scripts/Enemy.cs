using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Unit
{
    // not using now
    float pHp {
        get { return healthPoint;}
        set {
            if (state.Equals(State.Dead))
                return;

            healthPoint = value;
            if(healthPoint < 0f)
            {
                healthPoint = 0f;
                OnDead();
            }
        }
    }
    [SerializeField] Animator _anim;
    [SerializeField] NavMeshAgent _agent;

    [SerializeField] Unit _target;
    float _remainDistance;
    [Header("Attack")]
    [SerializeField] UnitHit _hit;

    void OnEnable()
    {
        if(!_anim) _anim.GetComponent<Animator>();
        if(!_agent) _agent.GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _hit.Init(this, 10f);

        state = State.Idle;
        pHp = 50f;

        SetState(StateIdle());
    }

    void ClearAnim(){
        _anim.SetBool("IsRun", false);
        _anim.SetBool("IsWalk", false);
    }


    Coroutine CorState;
    WaitForSeconds waitASec = new WaitForSeconds(1f);
    WaitForSeconds waitHalfOfASec = new WaitForSeconds(0.5f);

    void SetState(IEnumerator cor){
        if(CorState != null)
            StopCoroutine(CorState);

        CorState = StartCoroutine(cor);
    }

    IEnumerator StateIdle(){
        ClearAnim();
        state = State.Idle;
        _agent.SetDestination(transform.position);

        while(true){
            int r = Random.Range(0, 101);
            if(r <= 90)
                _anim.SetInteger("Idle", r % 3);
            else
            {
                SetState(StateWander());
                break;
            }
            yield return waitASec;
        }
    }

    IEnumerator StateWander(){
        Vector3 dest = transform.position;
        dest.x += Random.Range(-5f, 5f);
        dest.z += Random.Range(-5f, 5f);

        _agent.SetDestination(dest);
        _agent.speed = 1f;
        _anim.SetBool("IsWalk", true);
        
        _remainDistance = (dest - transform.position).magnitude;
        while(_remainDistance > 0.5f){
            yield return waitHalfOfASec;
            _remainDistance = (dest - transform.position).magnitude;
        }
        
        SetState(StateIdle());
    }

    void Engage(Unit newTarget){
        if(state.Equals(State.Engage)) return;
        
        state = State.Engage;
        // Debug.Log("Start Battle.");
        ClearAnim();
        _target = newTarget;
        
        SetState(StateFollow());
    }

    IEnumerator StateFollow(){
        _agent.SetDestination(_target.transform.position);
        _agent.speed = 2f;
        _anim.SetBool("IsRun", true);
        
        _remainDistance = (_target.transform.position - transform.position).magnitude;

        while(_remainDistance > 2.5f){

            yield return waitHalfOfASec;

            _agent.SetDestination(_target.transform.position);
            _remainDistance = (_target.transform.position - transform.position).magnitude;
            
            if(_remainDistance >= 10f)
                SetState(StateIdle());
        }
        
        SetState(StateAttack());
    }

    IEnumerator StateAttack(){
        ClearAnim();
        _remainDistance = (_target.transform.position - transform.position).magnitude;

        while(!_target.state.Equals(State.Dead)){
            if(_remainDistance > 2.5f)
                SetState(StateFollow());
            else
                Attack();

            yield return waitASec;

            _remainDistance = (_target.transform.position - transform.position).magnitude;
        }

        SetState(StateIdle());
    }

    protected override void Attack(){
        _anim.SetTrigger("Attack");
    }

    public override void HitEvent(int id = 0)
    {
        _hit.gameObject.SetActive(true);
    }

    public override void Damage(float val, Unit subject) {
        // pHp -= val;
        Hitted();
    }
    
    void Hitted(){
        int r = Random.Range(0, 5);
        _anim.SetInteger("HitVar", r);
        _anim.SetTrigger("Hit");
    }

    protected override void OnDead()
    {
       state = State.Dead;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player")){
            Engage(other.gameObject.GetComponent<Unit>());
        }
    }
    
}
