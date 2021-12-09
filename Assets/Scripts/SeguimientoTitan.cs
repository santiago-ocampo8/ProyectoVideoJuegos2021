using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SeguimientoTitan : MonoBehaviour
{
    public GameObject Enemigo;
    bool vision;
    NavMeshAgent Agente;
    Vector3 posIni;
    private Vida vida;
    private Vida vidaJugador;
    private LogicaJugador logicaJugador;
    private Animator animator;
    public bool Vida0 = false;
    public bool estaAtacando = false;
    public float daño = 25;
    public bool mirando;
    public bool sumarPuntos;
    public GameObject puntajePantalla;

    // Start is called before the first frame update
    void Start()
    {
        this.posIni = this.transform.position;
        this.Agente = this.GetComponent<NavMeshAgent>();
        vidaJugador = Enemigo.GetComponent<Vida>();
        logicaJugador = Enemigo.GetComponent<LogicaJugador>();
        vida = GetComponent<Vida>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        revisarVida();
        //perseguir();
        revisarAtaque();
        estaFrenteJugador();

        NavMeshHit hit;
        vision = NavMesh.Raycast(this.transform.position, this.Enemigo.transform.position, out hit, NavMesh.AllAreas);

        if (hit.distance < 30f && (!vision))
        {
            //me deberia seguir
            this.gameObject.GetComponent<LogicaTitan>().setBandera(false);
            //this.Agente.speed = 2f;
            this.seguir(this.Enemigo.transform.position);
            //this.gameObject.GetComponent<Animator>().SetTrigger("DebeAtacar");
        }
        else
        {
            if(!this.gameObject.GetComponent<LogicaTitan>().getBandera())
            {
                //this.gameObject.GetComponent<Animator>().SetTrigger("DebeAtacar");
                //this.Agente.speed = 2f;
                this.seguir(this.posIni);
                double distancia = Vector3.Distance(this.transform.position, this.posIni);

                if (distancia <= 1.0)
                {
                    this.gameObject.GetComponent<LogicaTitan>().setBandera(true);
                }
            }
        }
    }

    void estaFrenteJugador()
    {
        Vector3 adelante = transform.forward;
        Vector3 tarjeTJugador = (Enemigo.transform.position - transform.position).normalized;

        if (Vector3.Dot(adelante, tarjeTJugador) < 0.6f)
        {
            mirando = false;
        }
        else
        {
            mirando = true;
        }
    }

    void revisarVida()
    {
        if(Vida0) return;
        if(vida.valor <= 0)
        {
            sumarPuntos = true;
            if(sumarPuntos)
            {
                puntajePantalla.GetComponent<Puntaje>().valor += 20;
                sumarPuntos = false;
            }
            Vida0 = true;
            Agente.isStopped = true;
            GetComponent<Collider>().enabled = false;
            animator.CrossFadeInFixedTime("Z_Vida0", 0.1f);
            Destroy(gameObject, 3f);
        }
    }

    void perseguir()
    {
        if(Vida0) return;
        if(logicaJugador.Vida0) return;
        Agente.destination = Enemigo.transform.position;
    }

    void seguir(Vector3 objetivo)
    {
        if(Vida0) return;
        if(logicaJugador.Vida0) return;
        this.Agente.destination = objetivo;
    }

    void revisarAtaque()
    {
        if(Vida0) return;
        if(estaAtacando) return;
        if(logicaJugador.Vida0) return;
        float distanciaBlanco = Vector3.Distance(Enemigo.transform.position, transform.position);

        if(distanciaBlanco <= 4.5f && mirando)
        {
            //this.Agente.speed = 0.1f;
            this.Agente.isStopped = true;
            atacar();
        }
        if(distanciaBlanco > 8f && mirando)
        {
            this.Agente.isStopped = false;
        }
        if(distanciaBlanco > 6f && !(mirando))
        {
            this.Agente.isStopped = false;
        }
    }

    void atacar()
    {
        vidaJugador.RecibirDaño(daño);
        estaAtacando = true;
        animator.SetTrigger("DebeAtacar");
        Invoke("reiniciarAtaque", 1.2f);
    }

    void reiniciarAtaque()
    {
        estaAtacando = false;
    }
}
