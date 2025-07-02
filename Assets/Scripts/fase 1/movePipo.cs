using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class movePipo : MonoBehaviour
{
    public Transform[] alvo; 
    public int index, primeiraVista;
    private NavMeshAgent agente;    
    private Animator anim;
    private CircleCollider2D colisor;
    private Rigidbody2D rb;
    public TextoParaTextPro dialogo;
    public itemColetavel foiPego1, foiPego2;
    public bool pare, trocaPersona;
    public GameObject persona1, persona2;

    private string chaveIndex = "movePipo_index";
    private string chavePrimeiraVista = "movePipo_primeiraVista";

    private string chavePosX = "movePipo_posX";
    private string chavePosY = "movePipo_posY";
    private string chavePosZ = "movePipo_posZ";

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        agente.updateRotation = false;
        agente.updateUpAxis = false;

        // Carregar valores salvos ou usar padrão 0
        index = PlayerPrefs.GetInt(chaveIndex, 0);
        primeiraVista = PlayerPrefs.GetInt(chavePrimeiraVista, 0);

        // Carregar posição salva ou usar posição atual
        float x = PlayerPrefs.GetFloat(chavePosX, transform.position.x);
        float y = PlayerPrefs.GetFloat(chavePosY, transform.position.y);
        float z = PlayerPrefs.GetFloat(chavePosZ, transform.position.z);
        transform.position = new Vector3(x, y, z);

        anim = GetComponent<Animator>();
        colisor = GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        colisor.radius = 0.5f;
        dialogo = FindFirstObjectByType<TextoParaTextPro>();
        pare = false;
        trocaPersona = false;
    }

    void Update()
    {
        if(primeiraVista == 0)
        {
            agente.SetDestination(alvo[index].position);
            // Verifica se chegou ao destino
            if (agente.remainingDistance <= agente.stoppingDistance && !agente.pathPending)
            {      
                index = (index + 1) % 2; 
                if (index == 0)
                {
                    anim.Play("Esquerda");
                    anim.SetInteger("horizontal", -1);
                }
                else
                {
                    anim.Play("Direita");
                    anim.SetInteger("horizontal", 1);
                }
                SalvarEstado();
            }
        }
        else if (primeiraVista == 1 && pare == false)
        {
            agente.SetDestination(alvo[index].position);
            if (index == 1)
            {
                anim.Play("Direita");
                anim.SetInteger("horizontal", 1);
            }
            // Verifica se chegou ao destino
            if (agente.remainingDistance <= agente.stoppingDistance && !agente.pathPending)
            {       
                if(index < 15 )
                {
                    if( index > 0 && index < 9 )
                    {
                        anim.Play("Direita");
                        anim.SetInteger("horizontal", 1);
                        anim.SetInteger("vertical", 0);
                    }
                    else if (index > 8 && index < 14)
                    {
                        anim.Play("Frente");
                        anim.SetInteger("vertical", -1);
                        anim.SetInteger("horizontal", 0);
                    }
                    index ++ ;
                    SalvarEstado();
                } 
                else
                {
                    parado();
                }
            }
        }
        else if (primeiraVista == 3 && pare == false)
        {
            agente.SetDestination(alvo[index].position);

            // Verifica se chegou ao destino
            if (agente.remainingDistance <= agente.stoppingDistance && !agente.pathPending)
            {       
                if(index < 18 )
                {
                    index ++ ;
                    if( index == 17)
                    {
                        anim.Play("Frente");
                        anim.SetInteger("vertical", -1);
                        anim.SetInteger("horizontal", 0);                        
                    }
                    else if (index == 16 || index == 18)
                    {
                        anim.Play("Direita");
                        anim.SetInteger("horizontal", 1);
                        anim.SetInteger("vertical", 0);
                    }   
                    SalvarEstado();                 
                }
                else
                {
                    parado();
                    index ++ ;
                }
            }
        } 
        else
        {
            parado();
        }

        if(trocaPersona == true && primeiraVista == 1)
        {
            if (dialogo.i == 3 || dialogo.i == 9)
            {
                persona1.SetActive(true);
                persona2.SetActive(false);
            }
            else if(dialogo.i == 0 || dialogo.i == 6 || dialogo.i == 12)
            {
                persona1.SetActive(false);
                persona2.SetActive(true);
            }

            if (dialogo.i == 12) 
            {
                trocaPersona = false;
            }
        } 

        if(trocaPersona == true && primeiraVista == 2)
        {
            if (dialogo.i == 3)
            {
                persona1.SetActive(true);
                persona2.SetActive(false);
                trocaPersona = false;
            }
            else if(dialogo.i == 0)
            {
                persona1.SetActive(false);
                persona2.SetActive(true);
            }
        } 

        if(trocaPersona == true && primeiraVista == 3)
        {
            if (dialogo.i == 0 || dialogo.i == 6 || dialogo.i == 12 || dialogo.i == 18 || dialogo.i == 24)
            {
                persona1.SetActive(true);
                persona2.SetActive(false);
            }
            else if(dialogo.i == 3 || dialogo.i == 9 || dialogo.i == 15 || dialogo.i == 21 || dialogo.i == 27)
            {
                persona1.SetActive(false);
                persona2.SetActive(true);
            }
            
            if (dialogo.i == 27)
            {
                trocaPersona = false;
            }
        } 

        if(trocaPersona == true && primeiraVista == 4)
        {
            if(dialogo.i == 0)
            {
                persona1.SetActive(false);
                persona2.SetActive(true);
                trocaPersona = false;
            }
        } 

        if(foiPego2.coletado == true)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {  
            if(primeiraVista < 1)
            {
                primeiraVista += 1;
                SalvarEstado();
                parado();
                index = 1;
                SalvarEstado();

                trocaPersona = true;
                dialogo.comecar = true;
                dialogo.nomeDoArquivo = "Pipo1";
                colisor.radius = 2f;
            }
            else if (primeiraVista < 2 && index > 14 )
            {
                primeiraVista += 1;
                SalvarEstado();
                parado();
                trocaPersona = true;
                dialogo.comecar = true;
                dialogo.nomeDoArquivo = "Pipo2";
                colisor.radius = 1f;
                rb.constraints |= RigidbodyConstraints2D.FreezePositionX;
                rb.constraints |= RigidbodyConstraints2D.FreezePositionY;
            }
            else if (foiPego1.coletado == true && primeiraVista < 3)
            {
                primeiraVista += 1;
                SalvarEstado();
                parado();
                trocaPersona = true;
                dialogo.comecar = true;
                dialogo.nomeDoArquivo = "Pipo3";
                colisor.radius = 2f;
                rb.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
                rb.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
            }
            else if (primeiraVista < 4 && index > 18 )
            {
                primeiraVista += 1;
                SalvarEstado();
                parado();
                trocaPersona = true;
                dialogo.comecar = true;
                dialogo.nomeDoArquivo = "Pipo4";
                agente.isStopped = true;
                rb.constraints |= RigidbodyConstraints2D.FreezePositionX;
                rb.constraints |= RigidbodyConstraints2D.FreezePositionY;
            }
            else 
            {
                agente.isStopped = false;
                pare = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(index < 19)
            {
                pare = true;
                parado();
            }   
        }
    }

    private void parado()
    {
        agente.isStopped = true;
        anim.Play("Deboas");
    }

    // Salva index, primeiraVista e posição do personagem
    private void SalvarEstado()
    {
        PlayerPrefs.SetInt(chaveIndex, index);
        PlayerPrefs.SetInt(chavePrimeiraVista, primeiraVista);

        Vector3 pos = transform.position;
        PlayerPrefs.SetFloat(chavePosX, pos.x);
        PlayerPrefs.SetFloat(chavePosY, pos.y);
        PlayerPrefs.SetFloat(chavePosZ, pos.z);

        PlayerPrefs.Save();
    }
}
