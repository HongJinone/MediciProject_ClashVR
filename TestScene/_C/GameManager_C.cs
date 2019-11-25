using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//게임매니저는 게임의 진행을 맡는다.
//컴퓨터의 유닛 생성, 유닛 강화, 타워 체력 갱신을 맡는다.
//1. 플레이 버튼을 누르면 게임이 시작된다.(3, 4를 실행한다)
//2. 설정 버튼을 누르면 게임이 중지된다.
//3. 타워가 부숴졌다면 일정 시간이 지나면 재생성한다.
//4. 컴퓨터 유닛은 일정 시간이 지나면 재생성한다.(일단 중앙만)

public class GameManager_C : MonoBehaviour
{
    public bool roundIsPlaying;
    //public bool gameIsStopped;
    public int roundWave;

    public GameObject[] unitPrefabs; //근접 0, 법사 1, 궁수 2
    public Transform[] spawnPositions; //5개(근접: 0,2,4, 법사 2, 궁수 1,3)
    
    private void Start()
    {
        CoroutineStartUI();
    }
    //플레이 버튼을 누르면 코루틴이 실행됨
    public void CoroutineStartUI() 
    {
        StartCoroutine(RoundStart());
    }
    public void MenuUI()
    {
        Debug.Log("레벨 재시작 버튼");
        Debug.Log("레벨 선택 버튼");
        Debug.Log("방 나가기 버튼");

    }
    
    //플레이 버튼으로 호출되는 매서드
    IEnumerator RoundStart()
    {
        Debug.Log("Coroutine: RoundStart");
        yield return new WaitForSeconds (5f);
        roundWave ++;
        roundIsPlaying=true;  
        //플레이어 열의 제공
        Debug.Log("GameManager: 플레이어 열의 + a");
        //컴퓨터, 중앙에 유닛 생성
        StartCoroutine(SummonUnitsInMiddle());
        StartCoroutine(SummonUnitsInLeft());
        StartCoroutine(SummonUnitsInRight());
        //30초 후 RoundEnd      
        yield return new WaitForSeconds (25f);
        yield return RoundEnd();
    }

    IEnumerator RoundEnd()
    {
        Debug.Log("Coroutine: RoundEnd");
        //4. 컴퓨터 유닛은 일정 시간이 지나면 재생성한다.(일단 중앙만)
        //5. 타워가 남아있으면 체력과 공격력을 강화한다.
        //타워가 남아있지 않으면 열의를 깎는다.
        yield return new WaitForSeconds (5f);
        yield return RoundStart();
    }

    IEnumerator SummonUnitsInMiddle()
    {
        SummonMeleeUnits();
        yield return new WaitForSeconds (1f);
        SummonMageUnits();
        yield return new WaitForSeconds (1f);
        SummonRangeUnits();
        yield return null;
    }
    void SummonMeleeUnits()
    {
        Instantiate (unitPrefabs[0], spawnPositions[0].position, Quaternion.identity); //밀리 3개
        //Instantiate (unitPrefabs[0], spawnPositions[2].position, Quaternion.identity); 
        Instantiate (unitPrefabs[0], spawnPositions[4].position, Quaternion.identity); 
    }
    void SummonMageUnits()
    {
        Instantiate (unitPrefabs[1], spawnPositions[2].position, Quaternion.identity); //법사 1개
    }
    void SummonRangeUnits()
    {
        Instantiate (unitPrefabs[2], spawnPositions[1].position, Quaternion.identity); //궁수 2개
        Instantiate (unitPrefabs[2], spawnPositions[3].position, Quaternion.identity); 
    }
    IEnumerator SummonUnitsInLeft()
    {
        SummonMeleeUnits_L();
        yield return new WaitForSeconds (1f);
        SummonMageUnits_L();
        yield return new WaitForSeconds (1f);
        SummonRangeUnits_L();
        yield return null;

    }

    void SummonMeleeUnits_L()
    {
        Instantiate (unitPrefabs[0], spawnPositions[6].position, Quaternion.identity); //밀리
        Instantiate (unitPrefabs[0], spawnPositions[8].position, Quaternion.identity); 
    }
    void SummonMageUnits_L()
    {
        Instantiate (unitPrefabs[1], spawnPositions[7].position, Quaternion.identity);
    }
    void SummonRangeUnits_L()
    {
        Instantiate (unitPrefabs[2], spawnPositions[7].position, Quaternion.identity);
    }   
    IEnumerator SummonUnitsInRight()
    {
        SummonMeleeUnits_R();
        yield return new WaitForSeconds (1f);
        SummonMageUnits_R();
        yield return new WaitForSeconds (1f);
        SummonRangeUnits_R();
        yield return null;
    }
    void SummonMeleeUnits_R()
    {
        Instantiate (unitPrefabs[0], spawnPositions[11].position, Quaternion.identity); //밀리 2개
        Instantiate (unitPrefabs[0], spawnPositions[13].position, Quaternion.identity); 
    }
    void SummonMageUnits_R()
    {
        Instantiate (unitPrefabs[1], spawnPositions[12].position, Quaternion.identity);
    }
    void SummonRangeUnits_R()
    {
        Instantiate (unitPrefabs[2], spawnPositions[12].position, Quaternion.identity);
    } 
}
