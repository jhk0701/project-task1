using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DataDefinition;
using System.Linq;

public class FeatureTrade : MonoBehaviour
{
    [SerializeField] GameObject _pnlTrade;
    NpcMerchant _merchant;

    [Space(10f)]
    // position - item id
    Dictionary<int, int> _playerWeapons = new Dictionary<int, int>();
    Dictionary<int, int> _merchantWeapons = new Dictionary<int, int>();
    [SerializeField] GameObject _prefWeapon;
    [SerializeField] List<WeaponUI> _weaponInsts;

    [Space(10f)]
    [SerializeField] Text _txtPlayerTitle;
    [SerializeField] Text _txtMerchantTitle;
    [SerializeField] Text _txtPlayerGold;
    [SerializeField] Text _txtMerchantGold;
    int _playerGold = 0;
    int _merchantGold = 0;
    int pPlayerGold {
        get {return _playerGold;}
        set {
            _playerGold = value;
            _txtPlayerGold.text = $"{_playerGold} G";
        }
    }
    int pMerchantGold {
        get {return _merchantGold;}
        set{
            _merchantGold = value;
            _txtMerchantGold.text = $"{_merchantGold} G";
        }
    }
    
    [Space(10f)]
    [SerializeField] Transform _tfPlayerBack;
    [SerializeField] Transform _tfPlayerContainer;
    [SerializeField] Transform _tfMerchantBack;
    [SerializeField] Transform _tfMerchantContainer;

    public void OpenTrade(NpcMerchant merchant){
        if(_pnlTrade.activeInHierarchy) return;
        Manager.instance.inventory.CloseInventory();

        _pnlTrade.SetActive(true);
        _merchant = merchant;
        _txtMerchantTitle.text = merchant.GetName();
        _txtPlayerTitle.text = Manager.instance.player.GetName();

        _merchantWeapons = merchant.sellingItem;
        _playerWeapons = Manager.instance.playerInfo.ownedWeapons;
        
        pPlayerGold = Manager.instance.playerInfo.gold;
        pMerchantGold = merchant.gold;

        Set();
    }

    public void CloseTrade(){
        _pnlTrade.SetActive(false);

        Manager.instance.playerInfo.ownedWeapons = _playerWeapons;
        _merchant.sellingItem = _merchantWeapons;

        
        Manager.instance.playerInfo.gold = pPlayerGold;
        _merchant.gold = pMerchantGold;

        Clear();
    }

    public bool IsTrading(){
        return _pnlTrade.activeInHierarchy;
    }

    void Set(){
        List<int> keys = _playerWeapons.Keys.ToList();
        for (int i = 0; i < keys.Count; i++)
        {
            int pos = keys[i];
            if(_playerWeapons[pos] > 0)
            {
                // tfBackground.GetChild(i);
                GameObject inst = Instantiate(_prefWeapon, _tfPlayerContainer);
                inst.transform.localPosition = _tfPlayerBack.GetChild(pos).transform.localPosition;
                
                WeaponUI ui = inst.GetComponent<WeaponUI>();
                ui.Init(_playerWeapons[pos], pos, _tfPlayerContainer);

                _weaponInsts.Add(ui);
            }
        }

        keys.Clear();
        keys = _merchantWeapons.Keys.ToList();

        for (int i = 0; i < keys.Count; i++)
        {
            int pos = keys[i];
            if(_merchantWeapons[pos] > 0)
            {
                GameObject inst = Instantiate(_prefWeapon, _tfMerchantContainer);
                inst.transform.localPosition = _tfMerchantBack.GetChild(pos).transform.localPosition;
                
                WeaponUI ui = inst.GetComponent<WeaponUI>();
                ui.Init(_merchantWeapons[pos], pos, _tfMerchantContainer);

                _weaponInsts.Add(ui);
            }
        }
    }

    void Clear(){
        GameObject destroy = new GameObject();
        for (int i = 0; i < _weaponInsts.Count; i++)
        {
            if(_weaponInsts[i] != null)
                _weaponInsts[i].transform.SetParent(destroy.transform);
        }

        Destroy(destroy);
        _weaponInsts.Clear();
    }

    public bool IsPlayerSide(GameObject go){
        return go.Equals(_tfPlayerContainer.gameObject);
    }

    public bool Buy(int curPos, int newPos, int id){
        if (_playerWeapons.ContainsKey(newPos) && 
            _playerWeapons[newPos] > 0) 
            return false;

        int price = Manager.instance.dataItem.GetWeapon(id).price;
        if(pPlayerGold < price) 
            return false;
        
        _merchantWeapons[curPos] = 0;
        if(_playerWeapons.ContainsKey(newPos))
            _playerWeapons[newPos] = id;
        else
            _playerWeapons.Add(newPos, id);

        pPlayerGold -= price;
        pMerchantGold += price;

        return true;
    }

    public bool Sell(int curPos, int newPos, int id){
        if (_merchantWeapons.ContainsKey(newPos) &&
            _merchantWeapons[newPos] > 0) 
            return false;

        int price = Manager.instance.dataItem.GetWeapon(id).price;
        if(pMerchantGold < price) 
            return false;
        
        _playerWeapons[curPos] = 0;

        if(_merchantWeapons.ContainsKey(newPos))
            _merchantWeapons[newPos] = id;
        else
            _merchantWeapons.Add(newPos, id);

        pPlayerGold += price;
        pMerchantGold -= price;

        return true;
    }

    public bool Move(bool isPlayer, int curPos, int newPos, int id){
        if(isPlayer){
            if (_playerWeapons.ContainsKey(newPos) && 
                _playerWeapons[newPos] > 0)
                return false;
            
            _playerWeapons[curPos] = 0;
            if(_playerWeapons.ContainsKey(newPos))
                _playerWeapons[newPos] = id;
            else
                _playerWeapons.Add(newPos, id);
        }
        else{
            if (_merchantWeapons.ContainsKey(newPos) && 
                _merchantWeapons[newPos] > 0)
                return false;
            
                _merchantWeapons[curPos] = 0;
                if(_merchantWeapons.ContainsKey(newPos))
                    _merchantWeapons[newPos] = id;
                else
                    _merchantWeapons.Add(newPos, id);
        }

        return true;
    }
}