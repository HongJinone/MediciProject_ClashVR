using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager_C : MonoBehaviour
{
    //public GameObject parent;
    public int fp;
    public Text countFPText;
    //터렛의 터렛스크립트에 접근해야 함
    public Turret_C turretL;
    public Turret_C turretR;
    Arrow_C turretBullet;
    private void Start() 
    {
        //터렛의 HP 스크립트에 접근해야 함
        HP_C turretHP_R= turretR.GetComponentInChildren<HP_C>();
    }
    
    //업데이트 말고 값이 들어오면 출력하게 할 수 있나? 왜 Start로는 되지 않았지?
    private void Update() {
        SetCountText();
    }

    void SetCountText()
    {
        countFPText.text=fp.ToString();
    }

    //포탑의 공격력 증가
    public void PutTowerLAtkDmg()
    {
        Debug.Log("GetTowerLAtkDmg");
        if(fp>=500)
        {
            turretL.SetDamage(20);
            fp-=500;
        } else MyDebug.Log("열의가 부족합니다.");
    }
    //포탑의 체력 증가
    public void PutTowerLHP()
    {
        Debug.Log("GetTowerLHP");
        if(fp>=250)
        {
            HP_C turretHP_L= turretL.hp;
            turretHP_L.SetHealth(500);
            fp-=250;
        } else MyDebug.Log("열의가 부족합니다.");
    }

    public void PutTowerRAtkDmg()
    {
        if(fp>=500)
        {
            turretR.SetDamage(20);
            fp-=500;
        } else MyDebug.Log("열의가 부족합니다.");
    }
    //포탑의 체력 증가
    public void PutTowerRHP()
    {
        if(fp>=250)
        {
            HP_C turretHP_R= turretR.GetComponentInChildren<HP_C>();
            turretHP_R.SetHealth(500);
            fp-=250;
        } else MyDebug.Log("열의가 부족합니다.");
    }
    //포탑 재생성

}
