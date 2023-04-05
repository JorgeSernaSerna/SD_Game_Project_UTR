using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FightEnemy : MonoBehaviour
{
    [SerializeField] int life;
    [SerializeField] int MaxLife = 100;
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    public GameObject heart4;
    public GameObject heart5;

    // Start is called before the first frame update
    void Start()
    {
        life = MaxLife;
        
    }
    void Update()
    {
        if(life <= 80)
        {
            Destroy(heart1);
        }
        if(life <= 60)
        {
            Destroy(heart2);
        }
        if(life <= 40)
        {
            Destroy(heart3);
        }
        if(life <= 20)
        {
            Destroy(heart4);
        }
        if(life <= 0)
        {
            Destroy(heart5);
        }
    
    }

    public void GetDamage(int damage)
    {
        life -= damage;
        if(life <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene(4);
        }
    }
}
