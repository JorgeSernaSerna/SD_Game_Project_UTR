using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Enemy : MonoBehaviour
{
    // private void OnCollisionEnter2D(Collision2D other)
    // {
    //     if(other.collider.CompareTag("Player"))
    //     {
    //         other.collider.GetComponent<FightEnemy>().GetDamage(20);
    //     }
    // }
   private Animator animator;
   
   private void Start()
   {
        animator = GetComponent<Animator>();
   }
   public async Task killAsync()
    {
        await Task.Delay(1000);
        Destroy(gameObject);
    }
   private async void OnTriggerEnter2D(Collider2D other)
   {
        if(other.CompareTag("Player"))
        {
            GetComponent<CapsuleCollider2D>().size = new Vector2(0.0f, 0.0f);
            GetComponent<BoxCollider2D>().size = new Vector2(0.0f, 0.0f);
            await Task.Delay(500);
            animator.SetTrigger("hit");
            await Task.Delay(800);
            Destroy(gameObject);
        }
   }
  
    
    public bool damageNinja = false;


    // public void OnCollisionEnter2D(Collision2D other)
    // {
    //     if(damageNinja)
    //     {
    //         if(other.collider.CompareTag("Player"))
    //         {
    //             other.collider.GetComponent<FightEnemy>().GetDamage(20);
    //         } 
    //     }
        
    // }

}
