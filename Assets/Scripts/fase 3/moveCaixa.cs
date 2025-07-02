using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCaixa : MonoBehaviour
{
    private float velocidade = 1f;
    private float inputHorizontal;
    public bool jogadorNoTrigger = false;
    public bool podeEmpurrar = false;
    private Animator anim;
    private transforma personagem;
    private GameObject kerolyne;
    private Rigidbody2D rb;
    private RigidbodyConstraints2D constraintsOriginais;
    public GameObject player;    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        constraintsOriginais = rb.constraints;
        personagem = player.GetComponent<transforma>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jogadorNoTrigger = true;           
            kerolyne = other.gameObject;
            anim = kerolyne.GetComponent<Animator>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jogadorNoTrigger = false;
        }
    }

    void Update()
    {
        inputHorizontal = Input.GetAxis("Horizontal");

        podeEmpurrar = jogadorNoTrigger && personagem.indiceAtual % 2 == 0 && Input.GetKey(KeyCode.C);

        if (podeEmpurrar && Mathf.Abs(inputHorizontal) > 0.1f)
        {
            rb.constraints = constraintsOriginais & ~RigidbodyConstraints2D.FreezePositionX;

            if (kerolyne.transform.position.x > transform.position.x)
            {
                anim.SetBool("empurra", true);
                anim.SetBool("empurra2", false);
            }
            else 
            {               
                anim.SetBool("empurra2", true);
                anim.SetBool("empurra", false);
            }
        }
        else if (personagem.indiceAtual % 2 == 1 )
        {
            rb.constraints = constraintsOriginais;
        }
        else if (anim != null)
        {
            anim.SetBool("empurra", false);
            anim.SetBool("empurra2", false);
        }
    }

    void FixedUpdate()
    {
        if (podeEmpurrar && Mathf.Abs(inputHorizontal) > 0.1f)
        {
            Vector2 movimento = new Vector2(inputHorizontal, 0f);
            Vector2 novaPosicao = rb.position + movimento * velocidade * Time.deltaTime;
            rb.MovePosition(novaPosicao);
        }
        else
        {
            rb.velocity = new Vector2(0f, 0f);
        }
    }
}
