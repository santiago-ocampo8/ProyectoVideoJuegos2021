using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{

    NavMeshAgent Agente;
    // Start is called before the first frame update
    void Start()
    {
     this.Agente=gameObject.GetComponent<NavMeshAgent>();  
    }

    // Update is called once per frame
    void Update()
    {
        seguir(GameObject.Find("Player").transform.position);
    }

    void seguir(Vector3 objetivo){
        this.Agente.destination=objetivo;
    }
}
