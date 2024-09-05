using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    // data의 위치에 관한 것.
    [SerializeField] Transform _tfTarget;
    public virtual void OnDrop(PointerEventData eventData){
        Debug.Log("Item drop");
        int sIndex = transform.GetSiblingIndex();
        WeaponUI w = Manager.instance.cursor.GetDraggedWeapon();
        
        if(_tfTarget.gameObject.Equals(w.GetParent())){
            // 인벤토리 내에서 이동

        }
        else{
            // 거래 등 다른 방식으로 이동
        }
    }
}
