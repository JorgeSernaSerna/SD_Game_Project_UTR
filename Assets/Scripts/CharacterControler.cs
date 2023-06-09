using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class CharacterControler : MonoBehaviour
{

    public float velocidad;
    public float fuerzaSalto;
    public LayerMask capaSuelo;
    public int saltosMaximos;



    private Rigidbody2D rigidbody; 
    private bool mirandoDerecha = true;
    private BoxCollider2D boxCollider;
    private int saltosRestantes;
    private Animator animator;
    

    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        saltosRestantes = saltosMaximos;
    }

    // Update is called once per frame
    void Update()
    {
        ProcesarMovimiento();
        ProcesarSalto();
        ProcesarGolpe();
    }


    bool EstaEnSuelo()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, new Vector2(boxCollider.bounds.size.x, boxCollider.bounds.size.y), 0f, Vector2.down, 0.2f, capaSuelo);
        return raycastHit.collider != null;
    }


    void ProcesarSalto()
    {

        if (EstaEnSuelo())
        {
            saltosRestantes = saltosMaximos;
            animator.SetBool("IsJumping", false);
        }
        else

        {

            animator.SetBool("IsJumping", true);
        }


        if (Input.GetKeyDown(KeyCode.Space) && saltosRestantes > 0)
        {
            saltosRestantes = saltosRestantes -1;
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0f);
            rigidbody.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
        }

        // if (saltosRestantes = saltosMaximos)
        // {
        //     animator.SetBool("IsJumping", true);
        // }

        // else

        // {

        //     animator.SetBool("IsJumping", false);
        // }

        
    }


    void ProcesarMovimiento()
    {
        //Logica del movimiento.
        float inputMovimiento = Input.GetAxis("Horizontal");

        if (inputMovimiento != 0f)
        {
            animator.SetBool("IsWalking", true); 
        }
        else 
        {
            animator.SetBool("IsWalking", false); 
        }
        

        rigidbody.velocity = new Vector2(inputMovimiento * velocidad, rigidbody.velocity.y);

        gestionarOrientacion(inputMovimiento); 
    }

    void gestionarOrientacion(float inputMovimiento)
    {
        //Si se cumple la condicion
        if ( (mirandoDerecha == true && inputMovimiento < 0) || (mirandoDerecha == false && inputMovimiento > 0))
        {
            mirandoDerecha = !mirandoDerecha;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
            //Ejecutar cambio de sprites
    }


    //GOLPE
    

    public void ProcesarGolpe()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            animator.SetBool("IsHiting", true); 
            GetComponent<CapsuleCollider2D>().size = new Vector2(2.2f, 3.520344f);
            GetComponent<CapsuleCollider2D>().offset = new Vector2(0.3f, 0.1148281f);
            GetComponent<CapsuleCollider2D>().isTrigger = true;
        }
        else
        {
            animator.SetBool("IsHiting", false);
            GetComponent<CapsuleCollider2D>().size = new Vector2(1.17f, 3.04f);
            GetComponent<CapsuleCollider2D>().offset = new Vector2(0.0f, 0.0f);
            GetComponent<CapsuleCollider2D>().isTrigger = false;
        }
    }

    private async void OnCollisionEnter2D(Collision2D other)
    {

        if(other.collider.CompareTag("Enemy") && animator.GetBool("IsHiting"))
        {
            //enemy.SetActive(true);  
            await other.collider.GetComponent<Enemy>().killAsync();
        }
        if(other.collider.CompareTag("Enemy") && !animator.GetBool("IsHiting"))
        {
            GetComponent<FightEnemy>().GetDamage(20);
        }

    }
    // private async void OnTriggerStay2D(Collider2D other)
    // {
    //     if(other.CompareTag("Enemy") && animator.GetBool("IsHiting"))
    //     {
    //         await other.GetComponent<Enemy>().killAsync();
    //     }
        
    // }
}
