using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] string _name;
    [SerializeField] Text _txtName;
    [SerializeField] Animator _anim;

    float _inputV;
    float _inputH;
    float _inputMouseX;
    float _inputMouseY;
    bool _inputIsRun;

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

    public void SetName(string name){
        _name = name;
        _txtName.text = _name;
    }
}
