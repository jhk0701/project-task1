using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    // data의 위치에 관한 것.
    [SerializeField] Transform _tfTarget;
    public virtual void OnDrop(PointerEventData eventData){
        if(Manager.instance.trade.IsTrading())
            OnTrade();
        else
            OnInventory();
        
    }

    void OnInventory(){
        WeaponUI w = Manager.instance.cursor.GetDraggedWeapon();
        if(w == null) return;

        int newPos = transform.GetSiblingIndex();
        if (_tfTarget.gameObject.Equals(w.parent.gameObject)){
            // 인벤토리 내에서 이동
            // 데이터 이동
            if(!Manager.instance.inventory.Edit(w.curPosition, newPos, w.data.id)) return;
            w.curPosition = newPos;

            // 물리적 이동
            w.transform.localPosition = transform.localPosition;
        }
    }

    void OnTrade(){
        WeaponUI w = Manager.instance.cursor.GetDraggedWeapon();
        if(w == null) return;

        int newPos = transform.GetSiblingIndex();
        if (_tfTarget.gameObject.Equals(w.parent.gameObject)){
            // 인벤토리 내에서 이동
            bool isPlayerSide = Manager.instance.trade.IsPlayerSide(_tfTarget.gameObject);
            // 데이터 이동
            if(Manager.instance.trade.Move(isPlayerSide, w.curPosition, newPos, w.data.id))
            {
                Debug.Log("can't move item.");
                return;
            }
            w.curPosition = newPos;
            // 물리적 이동
            w.transform.localPosition = transform.localPosition;
        }
        else {
            // 거래
            if (Manager.instance.trade.IsPlayerSide(_tfTarget.gameObject)){
                // 상인 => 플레이어 : 구매
                if (!Manager.instance.trade.Buy(w.curPosition, newPos, w.data.id)) 
                {
                    Debug.Log("can't buy item.");
                    return;
                }
            }
            else {
                // 플레이어 => 상인 : 판매
                if (!Manager.instance.trade.Sell(w.curPosition, newPos, w.data.id)) 
                {
                    Debug.Log("can't sell item.");
                    return;
                }
            }
            w.curPosition = newPos;

            w.parent = _tfTarget;
            w.transform.SetParent(_tfTarget);
            w.transform.localPosition = transform.localPosition;
        }
    }
}
