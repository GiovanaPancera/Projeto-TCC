using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class moveBorboleta : MonoBehaviour
{
    public Transform[] alvo; 
    public int index;
    private Vector3 posicao;
    private NavMeshAgent agente;    
    private Animator anim;
    private CircleCollider2D colisor;
    private string chaveIndex;
    private string chavePosX;
    private string chavePosY;
    private string chavePosZ;

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        agente.updateRotation = false;
        agente.updateUpAxis = false;

        // Inicializa as chaves com base no nome do GameObject
        chaveIndex = "moveBorboleta_index_" + gameObject.name;
        chavePosX = "moveBorboleta_posX_" + gameObject.name;
        chavePosY = "moveBorboleta_posY_" + gameObject.name;
        chavePosZ = "moveBorboleta_posZ_" + gameObject.name;

        // Carrega o index salvo ou começa do zero
        index = PlayerPrefs.GetInt(chaveIndex, 0);

        // Carrega a posição salva (ou usa a posição atual)
        float x = PlayerPrefs.GetFloat(chavePosX, transform.position.x);
        float y = PlayerPrefs.GetFloat(chavePosY, transform.position.y);
        float z = PlayerPrefs.GetFloat(chavePosZ, transform.position.z);
        posicao = new Vector3(x, y, z);
        transform.position = posicao;

        anim = GetComponent<Animator>();
        colisor = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        if(index < alvo.Length)
        {
            agente.SetDestination(alvo[index].position);

            if (index == 0)
            {
                anim.Play("descendo");
                anim.SetInteger("vertical", -1);
            }

            // Verifica se chegou ao destino
            if (agente.remainingDistance <= agente.stoppingDistance && !agente.pathPending)
            {    
                if(index == 1)
                {
                    anim.Play("borboleta");
                    anim.SetInteger("vertical", 1);
                    agente.isStopped = true;
                }  

                index += 1;

                // Atualiza a posição atual
                posicao = transform.position;

                // Salva o index e posição atualizados
                SalvarEstado();

                if (index != alvo.Length)
                {
                    anim.Play("descendo");
                    anim.SetInteger("vertical", -1);
                }
                else
                {
                    anim.Play("borboleta");
                    anim.SetInteger("vertical", 1);
                }
            }
        }
    }

    private void SalvarEstado()
    {
        PlayerPrefs.SetInt(chaveIndex, index);
        PlayerPrefs.SetFloat(chavePosX, posicao.x);
        PlayerPrefs.SetFloat(chavePosY, posicao.y);
        PlayerPrefs.SetFloat(chavePosZ, posicao.z);
        PlayerPrefs.Save();
    }
}
