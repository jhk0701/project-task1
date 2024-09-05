using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DataDefinition;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class WeaponUI : 
    MonoBehaviour, 
    IPointerEnterHandler, 
    IPointerExitHandler, 
    IPointerDownHandler,
    IDragHandler,
    IBeginDragHandler, 
    IEndDragHandler
{
    [SerializeField] Weapon _data;
    [SerializeField] Transform _parent;
    [SerializeField] Image _img;
    
    public void Init(int id, Transform parent){ 
        _data = Manager.instance.dataItem.GetWeapon(id);
        _img.sprite = _data.sprite;

        _parent = parent;
    }
    
    public Transform GetParent(){
        return _parent;
    }

    public void OnPointerEnter(PointerEventData eventData){
        string info = string.Format("Power : {0}\nPrice : {1}", _data.power, _data.price);
        Manager.instance.cursor.ActivateHover(_img.sprite, _data.name, info);
    }

    public void OnPointerExit(PointerEventData eventData){
        Manager.instance.cursor.DeactivateHover();
    }

    public void OnPointerDown(PointerEventData eventData){
        Manager.instance.cursor.DeactivateHover();
    }
    
    public void OnBeginDrag(PointerEventData eventData){
        Manager.instance.cursor.ActivateDrag(_img.sprite, this);
    }

    public void OnDrag(PointerEventData eventData){ return; }

    public void OnEndDrag(PointerEventData eventData){
        Manager.instance.cursor.DeactivateDrag();
    }
}
