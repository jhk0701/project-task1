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
    public Weapon data;
    public int curPosition;
    public Transform parent;
    [SerializeField] Image _img;
    
    public void Init(int id, int pos, Transform p){ 
        data = Manager.instance.dataItem.GetWeapon(id);
        _img.sprite = data.sprite;

        parent = p;
        curPosition = pos;
    }

    public void Use(){
        if(Manager.instance.trade.IsTrading()) return;

        Manager.instance.player.ChangeEquipment(data.type, data.id);
    }
    
    public void OnPointerEnter(PointerEventData eventData){
        string info = string.Format("Power : {0}\nPrice : {1}", data.power, data.price);
        Manager.instance.cursor.ActivateHover(_img.sprite, data.name, info);
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
