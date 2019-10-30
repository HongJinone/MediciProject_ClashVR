using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPController : MonoBehaviour
{
    [Header ("Health")]
    public float startHealth = 100;
    public float currentHealth;
    public UnityEngine.UI.Image healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startHealth;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetDamage(float v)
    {
        currentHealth -= v;
    }
}
