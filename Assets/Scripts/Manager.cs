using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public static Manager instance;

    public Player player;

    [Header("캐릭터 닉네임")]
    [SerializeField] GameObject pnlInputName;
    [SerializeField] InputField ifName;

    void Awake()
    {
        if (!instance)
            instance = this;
    }

    void Start()
    {
        pnlInputName.SetActive(true);
    }

    public void OnClickBtnConfirmName(){
        if(ifName.text.Length == 0f)
        {
            Debug.Log("최소 한글자는 입력해주세요.");
            return;
        }

        pnlInputName.SetActive(false);
        StartGame();
    }

    void StartGame(){
        player.enabled = true;
        player.SetName(ifName.text);
    }
}
