using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    // data�� ��ġ�� ���� ��.
    [SerializeField] Transform _tfTarget;
    public virtual void OnDrop(PointerEventData eventData){
        Debug.Log("Item drop");
        int sIndex = transform.GetSiblingIndex();
        WeaponUI w = Manager.instance.cursor.GetDraggedWeapon();
        
        if(_tfTarget.gameObject.Equals(w.GetParent())){
            // �κ��丮 ������ �̵�

        }
        else{
            // �ŷ� �� �ٸ� ������� �̵�
        }
    }
}
