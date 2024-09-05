using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeatureCursor : MonoBehaviour
{
    [SerializeField] Transform _tfCursor;
    [Header("Hover")]
    [SerializeField] GameObject _goHover;
    [SerializeField] Image _imgHover;
    [SerializeField] Text _txtHoverTitle;
    [SerializeField] Text _txtHoverInfo;

    [Header("Drag")]
    bool _isDragging;
    [SerializeField] WeaponUI _weaponUI;
    [SerializeField] GameObject _goDrag;
    [SerializeField] Image _imgDrag;

    // Update is called once per frame
    void Update()
    {
        if(_goHover.activeInHierarchy || _goDrag.activeInHierarchy)
            _tfCursor.position = Input.mousePosition;
    }

    public void ActivateHover(Sprite sprite, string title, string info){
        if(_isDragging) return;
        _goHover.SetActive(true);
        _imgHover.sprite = sprite;
        _txtHoverTitle.text = title;
        _txtHoverInfo.text = info;
    }
    public void DeactivateHover(){
        _goHover.SetActive(false);
        _imgHover.sprite = null;
    }

    public void ActivateDrag(Sprite sprite, WeaponUI weapon){
        _goDrag.SetActive(true);

        _weaponUI = weapon;
        _imgDrag.sprite = sprite;
        _isDragging = true;
    }

    public void DeactivateDrag(){
        _goDrag.SetActive(false);
        _weaponUI = null;
        _isDragging = false;
    }

    public WeaponUI GetDraggedWeapon(){
        return _weaponUI;
    }
}
