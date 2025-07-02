using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class esperar : MonoBehaviour
{
    public GameObject borboleta;
    private NavMeshAgent agente;   
    private CircleCollider2D colisor;
    
    void Start()
    {
        colisor = GetComponent<CircleCollider2D>();
        agente = borboleta.GetComponent<NavMeshAgent>();
    }
    // Update is called once per frame
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {            
           agente.isStopped = false;
        }
    }
}
