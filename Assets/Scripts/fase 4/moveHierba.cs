using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class moveHierba : MonoBehaviour
{
    private Animator anim;
    private CircleCollider2D colisor;
    public TextoParaTextPro dialogo;
    public itemColetavel foiPego1;
    public GameObject persona1, persona2;
    public Transform[] alvo; 
    public vinha obstaculo;
    private NavMeshAgent agente;   
    public int index, primeiraVista;   
    public bool pare, trocaPersona;

    private string chaveVista;
    private string chaveIndex;
    private string chavePosX;
    private string chavePosY;
    private string chavePosZ;
    private string chaveDesativado;

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        agente.updateRotation = false;
        agente.updateUpAxis = false;

        chaveVista = "Hierba_primeiraVista_" + gameObject.name;
        chaveIndex = "Hierba_index_" + gameObject.name;
        chavePosX = "Hierba_posX_" + gameObject.name;
        chavePosY = "Hierba_posY_" + gameObject.name;
        chavePosZ = "Hierba_posZ_" + gameObject.name;
        chaveDesativado = "Hierba_Desativado_" + gameObject.name;

        if (PlayerPrefs.GetInt(chaveDesativado, 0) == 1)
        {
            gameObject.SetActive(false);
            return;
        }

        // Carrega os valores salvos
        primeiraVista = PlayerPrefs.GetInt(chaveVista, 0);
        index = PlayerPrefs.GetInt(chaveIndex, 0);

        float x = PlayerPrefs.GetFloat(chavePosX, transform.position.x);
        float y = PlayerPrefs.GetFloat(chavePosY, transform.position.y);
        float z = PlayerPrefs.GetFloat(chavePosZ, transform.position.z);
        transform.position = new Vector3(x, y, z);

        anim = GetComponent<Animator>();
        colisor = GetComponent<CircleCollider2D>();
        dialogo = FindFirstObjectByType<TextoParaTextPro>();
        pare = false;
        trocaPersona = false;
    }

    void Update()
    {
        if(trocaPersona == true && primeiraVista == 1)
        {
            if (dialogo.i == 3 || dialogo.i == 9 || dialogo.i == 18)
            {
                persona1.SetActive(true);
                persona2.SetActive(false);
            }
            else if(dialogo.i == 0 || dialogo.i == 6 || dialogo.i == 12 || dialogo.i == 21)
            {
                persona1.SetActive(false);
                persona2.SetActive(true);
            }

            if (dialogo.i == 21) 
            {
                trocaPersona = false;
            }
        } 
        else if(trocaPersona == true && primeiraVista == 2)
        {
            if (dialogo.i == 3)
            {
                persona1.SetActive(true);
                persona2.SetActive(false);
            }
            else if(dialogo.i == 0 || dialogo.i == 6)
            {
                persona1.SetActive(false);
                persona2.SetActive(true);
            }

            if (dialogo.i == 9) 
            {
                trocaPersona = false;
            }
        } 
        else if(trocaPersona == true && primeiraVista == 3)
        {
            if (dialogo.i == 3)
            {
                persona1.SetActive(true);
                persona2.SetActive(false);
            }
            else if(dialogo.i == 0 || dialogo.i == 6)
            {
                persona1.SetActive(false);
                persona2.SetActive(true);
            }

            if (dialogo.i == 6) 
            {
                trocaPersona = false;
            }
        }
        else if(trocaPersona == true && primeiraVista == 4)
        {
            if(dialogo.i == 0)
            {
                persona1.SetActive(false);
                persona2.SetActive(true);
                trocaPersona = false;
            }
        }
        else if(trocaPersona == true && primeiraVista == 5)
        {
            if (dialogo.i == 3 || dialogo.i == 9 || dialogo.i == 15)
            {
                persona1.SetActive(true);
                persona2.SetActive(false);
            }
            else if(dialogo.i == 0 || dialogo.i == 6 || dialogo.i == 12 || dialogo.i == 18)
            {
                persona1.SetActive(false);
                persona2.SetActive(true);
            }

            if (dialogo.i == 24) 
            {
                trocaPersona = false;
                StartCoroutine(EsperarEDesativar());
            }
        }

        if (primeiraVista == 1 && pare == false)
        {
            agente.SetDestination(alvo[index].position);

            if (index == 0 || index == 5)
            {
                anim.Play("frente");
                anim.SetInteger("vertical", -1);
            }
            else if( index > 1 && index < 3 )
            {
                anim.Play("costa");
                anim.SetInteger("horizontal", 0);
                anim.SetInteger("vertical", 1);
            }
            else if (index == 1 || index == 3)
            {
                anim.Play("esquerda");
                anim.SetInteger("vertical", 0);
                anim.SetInteger("horizontal", -1);
            }

            if (agente.remainingDistance <= agente.stoppingDistance && !agente.pathPending)
            {       
                if(index < 5 )
                {
                    index += 1;
                } 
                else
                {
                    parado();
                }
                PlayerPrefs.SetInt(chaveIndex, index);
                SalvarPosicao();
            }
        }
        else if (primeiraVista == 2)
        {
            if (agente.remainingDistance <= agente.stoppingDistance && !agente.pathPending)
            {       
                anim.Play("deBoas");
            }
        }
        else if (primeiraVista == 3)
        {
            agente.SetDestination(alvo[index].position);

            if (index == 8)
            {
                anim.SetInteger("vertical", 0);
                anim.SetInteger("horizontal", 1);
            }
            else 
            {
                anim.Play("frente");
                anim.SetInteger("horizontal", 0);
                anim.SetInteger("vertical", -1);
            }

            if (agente.remainingDistance <= agente.stoppingDistance && !agente.pathPending)
            {       
                if(index < 10 )
                {
                    index += 1;
                } 
                else
                {
                    parado();
                }
                PlayerPrefs.SetInt(chaveIndex, index);
                SalvarPosicao();
            }
        }
        else if (primeiraVista == 4 && pare == false)
        {
            agente.SetDestination(alvo[index].position);

            if (index >= 10 && index <= 11 || index == 17)
            {
                anim.Play("frente");
                anim.SetInteger("vertical", -1);
            }
            else if (index == 12 || index == 14 || index == 16 || index == 18)
            {
                anim.Play("esquerda");
                anim.SetInteger("vertical", 0);
                anim.SetInteger("horizontal", -1);
            }
            else 
            {
                anim.Play("costa");
                anim.SetInteger("vertical", 1);
                anim.SetInteger("horizontal", 0);
            }

            if (agente.remainingDistance <= agente.stoppingDistance && !agente.pathPending)
            {       
                if(index < 20 )
                {
                    index += 1;
                } 
                else
                {
                    parado();
                }
                PlayerPrefs.SetInt(chaveIndex, index);
                SalvarPosicao();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (primeiraVista == 0)
            {
                parado();
                dialogo.comecar = true;
                trocaPersona = true;
                dialogo.nomeDoArquivo = "Hierba";
                colisor.radius = 2f;
                primeiraVista += 1;
                PlayerPrefs.SetInt(chaveVista, primeiraVista);
                SalvarPosicao();
            }
            else if (primeiraVista == 1 && index == 5)
            {
                parado();
                dialogo.comecar = true;
                trocaPersona = true;
                dialogo.nomeDoArquivo = "Hierba2";
                colisor.radius = 5f;
                primeiraVista += 1;
                PlayerPrefs.SetInt(chaveVista, primeiraVista);
                SalvarPosicao();
            }
            else if (primeiraVista == 2 && obstaculo.desbloqueada == true)
            {
                primeiraVista += 1;
                index += 1;
                PlayerPrefs.SetInt(chaveVista, primeiraVista);
                PlayerPrefs.SetInt(chaveIndex, index);
                SalvarPosicao();

                dialogo.comecar = true;
                trocaPersona = true;
                dialogo.nomeDoArquivo = "Hierba3";
                obstaculo.desbloqueada = false;
            }
            else if (primeiraVista == 3 && index == 10)
            {
                parado();
                dialogo.comecar = true;
                trocaPersona = true;
                dialogo.nomeDoArquivo = "Hierba4";
                colisor.radius = 2f;
                primeiraVista += 1;
                PlayerPrefs.SetInt(chaveVista, primeiraVista);
                SalvarPosicao();
            }
            else if (primeiraVista == 4 && index == 20)
            {
                parado();
                dialogo.comecar = true;
                trocaPersona = true;
                dialogo.nomeDoArquivo = "Hierba5";
                colisor.radius = 1f;
                primeiraVista += 1;
                PlayerPrefs.SetInt(chaveVista, primeiraVista);
                SalvarPosicao();
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
        anim.Play("deBoas");
    }

    private void SalvarPosicao()
    {
        Vector3 pos = transform.position;
        PlayerPrefs.SetFloat(chavePosX, pos.x);
        PlayerPrefs.SetFloat(chavePosY, pos.y);
        PlayerPrefs.SetFloat(chavePosZ, pos.z);
        PlayerPrefs.Save();
    }
    
    IEnumerator EsperarEDesativar()
    {
        yield return new WaitForSeconds(1f);
        PlayerPrefs.SetInt(chaveDesativado, 1);
        PlayerPrefs.Save();
        gameObject.SetActive(false);
    }
}
