using System.Collections;
using System.Collections.Generic;
using DataDefinition;
using UnityEngine;
using UnityEngine.UI;

public class Player : Unit
{
    [SerializeField] Animator _anim;
    string _name;
    [SerializeField] Text _txtName;

    [SerializeField] Transform _tfChar;
    [SerializeField] Transform _tfCamera;

    [Header("Weapon")]
    [SerializeField] Transform _tfWeapon;
    [SerializeField] GameObject _weaponInst;
    public Dictionary<int, int> equip;

    [Header("Status")]
    [SerializeField] float _spd = 1f;
    [SerializeField] float _rotateSpd = 1f;
    [SerializeField] float _camRotateSpd = 1.5f;
    [Header("Attack")]
    [SerializeField] List<UnitHit> _hits;

    float _inputV, _inputH;
    float _inputMouseX, _inputMouseY;
    float _inputRotate;
    bool _inputIsRun;

    void Start()
    {
        SetEquipment();
    }

    void Update() {
        if(_name.Equals("") || Manager.instance.trade.IsTrading())
            return;

        if(Input.GetAxis("Fire1") != 0f)
            Attack();

        _inputV = Input.GetAxis("Vertical");
        _anim.SetFloat("Vertical", _inputV);

        _inputH = Input.GetAxis("Horizontal");
        _anim.SetFloat("Horizontal", _inputH);

        // _inputIsRun = Input.GetKey(KeyCode.LeftShift);
        // if(_anim.GetBool("IsRun") != _inputIsRun)
        //     _anim.SetBool("IsRun", _inputIsRun);

        _inputMouseX = Input.GetAxis("Mouse X");

        if(Input.GetKey(KeyCode.Q))
            _inputRotate = -1f * 100f;
        else if(Input.GetKey(KeyCode.E))
            _inputRotate = 1f * 100f;
        else
            _inputRotate = 0f;

        _tfCamera.Rotate(Vector3.up * _inputMouseX * _camRotateSpd);

        Vector3 move = (_tfCamera.forward * _inputV + _tfCamera.right * _inputH) * _spd * Time.deltaTime;
        
        if(move.magnitude != 0)
        {
            transform.Translate(move, Space.World);
            _tfChar.localEulerAngles = _tfCamera.localEulerAngles;
        }
        
        // transform.Translate(_tfCamera.forward * _inputV * _spd * Time.deltaTime, Space.World);
        // transform.Translate(_tfCamera.right * _inputH * _spd * Time.deltaTime, Space.World);
        
    }

    public string GetName(){
        return _name;
    }

    public void SetName(string name){
        _name = name;
        _txtName.text = _name;
    }

    
    public void SetEquipment(){
        equip = Manager.instance.playerInfo.equipment;
        
        if(_weaponInst != null)
            Destroy(_weaponInst);
        
        Weapon w = Manager.instance.dataItem.GetWeapon(equip[(int)TypeItem.Weapon]);
        _weaponInst = Instantiate(w.pref, _tfWeapon);
        _weaponInst.transform.localPosition = Vector3.zero;
        _weaponInst.transform.localEulerAngles = Vector3.zero;
    }

    public void ChangeEquipment(TypeItem type, int id){
        equip[(int)type] = id;
        
        if(_weaponInst != null)
            Destroy(_weaponInst);

        Weapon w = Manager.instance.dataItem.GetWeapon(equip[(int)type]);
        _weaponInst = Instantiate(w.pref, _tfWeapon);
        _weaponInst.transform.localPosition = Vector3.zero;
        _weaponInst.transform.localEulerAngles = Vector3.zero;
    }

    public override void Damage(float val, Unit subject)
    {
        Hitted();
    }

    protected override void OnDead()
    {
        state = State.Dead;
    }

    void Hitted(){
        int r = Random.Range(0, 5);
        _anim.SetInteger("HitVar", r);
        _anim.SetTrigger("Hit");
    }

    protected override void Attack()
    {
        
        _anim.SetTrigger("Attack");
    }

    public override void HitEvent(int id = 0)
    {
        if(id > _hits.Count - 1) return;

        float damage = Manager.instance.dataItem.GetWeapon(equip[(int)TypeItem.Weapon]).power;
        _hits[id].SetDamage(damage);
        _hits[id].gameObject.SetActive(true);
    }
}
