using System.Collections;
using System.Collections.Generic;
using DataDefinition;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] string _name;
    [SerializeField] Text _txtName;
    [SerializeField] Animator _anim;
    [Header("weapon")]
    public Equipment equip;
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

    public void SetEquipment(){
        equip = Manager.instance.playerInfo.equipment;
        
        if(_weaponInst)
            Destroy(_weaponInst);
        
        Weapon w = Manager.instance.dataItem.GetWeapon(equip.weaponId);
        _weaponInst = Instantiate(w.pref, _tfWeapon);
        _weaponInst.transform.localPosition = Vector3.zero;
        _weaponInst.transform.localEulerAngles = Vector3.zero;
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
}
