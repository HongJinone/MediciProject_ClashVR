using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP_7 : MonoBehaviour
{
    [Header ("Health")]
    public float startHealth = 100;
    public float currentHealth;
    public UnityEngine.UI.Image healthBar;
    public bool isDead;
    public GameObject myParent;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startHealth;   
    }

    // Update is called once per frame
    void Update()
    {
        //만약 체력이 0 이하이면 죽는다.
        if(currentHealth<=0)
        {
            Die();
        }
    }

    public void GetDamage(float v)
    {
        Debug.Log("데미지를 입었다");
        currentHealth -= v;
    }
    void Die()
    {
        isDead=true;    //유닛, 터렛 쪽에서 이 값을 보고 움직임을 정지할 것임.
        //소멸한다. 애니메이션.
        Destroy(myParent);
    }
}
