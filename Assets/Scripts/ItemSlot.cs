using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    // data의 위치에 관한 것.
    [SerializeField] Transform _tfTarget;
    public virtual void OnDrop(PointerEventData eventData){
        WeaponUI w = Manager.instance.cursor.GetDraggedWeapon();
        if(w == null) return;

        int newPos = transform.GetSiblingIndex();
        if(_tfTarget.gameObject.Equals(w.parent.gameObject)){
            // 인벤토리 내에서 이동
            // 물리적 이동
            w.transform.localPosition = transform.localPosition;
            // 데이터 이동
            Manager.instance.inventory.Edit(w.curPosition, newPos, w.data.id);
        }
        else{
            // 거래 등 다른 방식으로 이동
        }
    }
}
