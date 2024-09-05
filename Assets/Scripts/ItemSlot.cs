using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    // data�� ��ġ�� ���� ��.
    [SerializeField] Transform _tfTarget;
    public virtual void OnDrop(PointerEventData eventData){
        WeaponUI w = Manager.instance.cursor.GetDraggedWeapon();
        if(w == null) return;

        int newPos = transform.GetSiblingIndex();
        if(_tfTarget.gameObject.Equals(w.parent.gameObject)){
            // �κ��丮 ������ �̵�
            // ������ �̵�
            w.transform.localPosition = transform.localPosition;
            // ������ �̵�
            Manager.instance.inventory.Edit(w.curPosition, newPos, w.data.id);
        }
        else{
            // �ŷ� �� �ٸ� ������� �̵�
        }
    }
}
