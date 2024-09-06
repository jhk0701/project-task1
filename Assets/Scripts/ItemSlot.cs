using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    // data�� ��ġ�� ���� ��.
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
            // �κ��丮 ������ �̵�
            // ������ �̵�
            if(!Manager.instance.inventory.Edit(w.curPosition, newPos, w.data.id)) return;
            w.curPosition = newPos;

            // ������ �̵�
            w.transform.localPosition = transform.localPosition;
        }
    }

    void OnTrade(){
        WeaponUI w = Manager.instance.cursor.GetDraggedWeapon();
        if(w == null) return;

        int newPos = transform.GetSiblingIndex();
        if (_tfTarget.gameObject.Equals(w.parent.gameObject)){
            // �κ��丮 ������ �̵�
            bool isPlayerSide = Manager.instance.trade.IsPlayerSide(_tfTarget.gameObject);
            // ������ �̵�
            if(Manager.instance.trade.Move(isPlayerSide, w.curPosition, newPos, w.data.id))
            {
                Debug.Log("can't move item.");
                return;
            }
            w.curPosition = newPos;
            // ������ �̵�
            w.transform.localPosition = transform.localPosition;
        }
        else {
            // �ŷ�
            if (Manager.instance.trade.IsPlayerSide(_tfTarget.gameObject)){
                // ���� => �÷��̾� : ����
                if (!Manager.instance.trade.Buy(w.curPosition, newPos, w.data.id)) 
                {
                    Debug.Log("can't buy item.");
                    return;
                }
            }
            else {
                // �÷��̾� => ���� : �Ǹ�
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
