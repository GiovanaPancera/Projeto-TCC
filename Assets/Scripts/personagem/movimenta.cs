using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimenta : MonoBehaviour
{
    private float velocidade;
    public int pulos = 1;
    public bool movendoVertical, movendoHorizontal;
    private transforma personagem;
    private Animator anim;
    private Rigidbody2D corpo;    
    private Vector2 direcao;
    private Vector3 posicao;
    private GameObject fase, player;
    private AudioSource som;

    void Start()
    {
        anim = GetComponent<Animator>();
        corpo = GetComponent<Rigidbody2D>(); 
        som =  GetComponent<AudioSource>();
        fase = GameObject.Find("Fase");  
        player = GameObject.Find("Personagem");
        personagem = player.GetComponent<transforma>();      
        anim.SetInteger("horizontal",0); 
        anim.SetInteger("vertical",0);  
        movendoVertical = false;
        movendoHorizontal = false;
        corpo.gravityScale = 0f; 
        pulos = 1;

        if(personagem.indiceAtual % 2 == 0)
        {
            velocidade = 1.2f; 
        }
        else
        {
            velocidade = 1.5f;
        }
    }
    
    void Update()
    {
        if(fase.tag == "TopDown")
        {
            float inputX = Input.GetAxisRaw("Horizontal");
            float inputY = Input.GetAxisRaw("Vertical");

            direcao = new Vector2(inputX, inputY).normalized;

            corpo.gravityScale = 0f;

            // Atualiza a animação com base no último movimento dominante
            if (Mathf.Abs(inputX) > Mathf.Abs(inputY))
            {
                movendoHorizontal = true;
                movendoVertical = false;

                if (inputX > 0)
                {
                    anim.Play("direita");
                    anim.SetInteger("horizontal", 1);
                    anim.SetInteger("vertical", 0);
                }
                else if (inputX < 0)
                {
                    anim.Play("esquerda");
                    anim.SetInteger("horizontal", -1);
                    anim.SetInteger("vertical", 0);
                }
                else
                {
                    anim.SetInteger("horizontal", 0);
                }
            }
            else if (Mathf.Abs(inputY) > 0)
            {
                movendoHorizontal = false;
                movendoVertical = true;

                if (inputY > 0)
                {
                    anim.Play("costa");
                    anim.SetInteger("vertical", 1);
                    anim.SetInteger("horizontal", 0);
                }
                else if (inputY < 0)
                {
                    anim.Play("frente");
                    anim.SetInteger("vertical", -1);
                    anim.SetInteger("horizontal", 0);
                }
            }
            else
            {
                anim.SetInteger("horizontal", 0);
                anim.SetInteger("vertical", 0);
            }
        }


        if(fase.tag == "Plataforma")
        {    
            corpo.gravityScale = 1f;
            posicao = transform.position;

            if(Input.GetAxis("Horizontal") != 0)
            {           
                posicao.x += velocidade * Input.GetAxis("Horizontal") * Time.deltaTime;
                movendoHorizontal = true;
                movendoVertical = false;

                if (Input.GetAxis("Horizontal") > 0)
                {
                    anim.SetInteger("vertical",0);
                    anim.SetInteger("horizontal", 1);
                }
                else
                {
                    anim.SetInteger("vertical",0);
                    anim.SetInteger("horizontal", -1);
                }
            }
            else
            {
                anim.SetInteger("vertical",0);
                anim.SetInteger("horizontal",0);
                movendoHorizontal = false;
            }

            transform.position = posicao;
            pulo(); 
        }   
    }

    void FixedUpdate()
    {
        if (fase.tag == "TopDown")
        {
            Vector2 novaPos = corpo.position + direcao * velocidade * Time.fixedDeltaTime;
            corpo.MovePosition(novaPos);
        }            
        
    }

    private void pulo()
    {
       if (Input.GetKeyDown(KeyCode.Space) && pulos > 0)
        {   
            som.Play();
            if(personagem.indiceAtual % 2 == 0)
            {
                corpo.velocity = new Vector2(corpo.velocity.x, 3f); // força vertical 
            }
            else
            {
                corpo.velocity = new Vector2(corpo.velocity.x, 4f); // força vertical 
            }
            anim.SetInteger("horizontal", 0);
            anim.SetInteger("vertical", 0);
            anim.SetBool("pulo", true);
            pulos -= 1;
        }
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    { 
        if (collision.gameObject.CompareTag("Chao"))
        {
            anim.SetBool("pulo", false);
            pulos = 1;
        }
    }
}