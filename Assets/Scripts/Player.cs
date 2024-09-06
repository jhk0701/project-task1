using System.Collections;
using System.Collections.Generic;
using DataDefinition;
using UnityEngine;
using UnityEngine.UI;

public class Player : Unit
{
    [SerializeField] string _name;
    [SerializeField] Text _txtName;
    [SerializeField] Animator _anim;
    [Header("weapon")]
    public Dictionary<int, int> equip;
    [SerializeField] Transform _tfWeapon;
    [SerializeField] GameObject _weaponInst;
    float _inputV;
    float _inputH;
    float _inputMouseX;
    float _inputMouseY;
    bool _inputIsRun;

    void Start()
    {
        SetEquipment();
    }

    void Update() {
        _inputV = Input.GetAxis("Vertical");
        _anim.SetFloat("Vertical", _inputV);

        _inputH = Input.GetAxis("Horizontal");
        _anim.SetFloat("Horizontal", _inputH);

        _inputIsRun = Input.GetKey(KeyCode.LeftShift);
        if(_anim.GetBool("IsRun") != _inputIsRun)
            _anim.SetBool("IsRun", _inputIsRun);

        _inputMouseX = Input.GetAxis("Mouse X");
        _inputMouseY = Input.GetAxis("Mouse Y");
        transform.Rotate(Vector3.up * _inputMouseX);
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

    public override void OnDead()
    {
        state = State.Dead;
    }

    void Hitted(){
        int r = Random.Range(0, 5);
        _anim.SetInteger("HitVar", r);
        _anim.SetTrigger("Hit");
    }
}
