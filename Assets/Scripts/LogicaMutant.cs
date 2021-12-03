using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LogicaMutant : MonoBehaviour
{
    Vector3 Fin;
    Vector3 Posini;
    NavMeshAgent Agente; 
    int flag;
    Vector3 Navegacion;
    bool bandera;

    // Start is called before the first frame update
    void Start()
    {
        this.bandera=true;
        this.Posini=gameObject.transform.position;
        this.Fin=transform.Find("Fin").gameObject.transform.position;
        
        this.Agente=gameObject.GetComponent<NavMeshAgent>();
        this.flag=1;
        this.Navegacion=this.Fin;
    }

    // Update is called once per frame
    void Update()
    {
        // va a realiza el patrol mientras la bandera sea verdadra
        if(bandera) 
        {
            double distancia =Vector3.Distance(this.transform.position,this.Navegacion);
            seguir(this.Navegacion);  

            if(distancia<=1.0)
            {
                switch(flag)
                {
                    case 1://voy origen
                    this.Navegacion=this.Posini;
                    this.flag=2;
                    break;

                    case 2://voy fin
                    this.Navegacion=this.Fin;
                    this.flag=1;
                    break;

                }
                
            }
        }
    }

    void seguir(Vector3 objetivo)
    {
      this.Agente.destination=objetivo;
    }

    public void setBandera(bool bandera)
    {
        this.bandera=bandera;
    }

    public bool getBandera()
    {
        return bandera;
    }
}
