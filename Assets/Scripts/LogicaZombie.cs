using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LogicaZombie : MonoBehaviour
{
    private GameObject tarjet;

    private AudioSource audio;
    private NavMeshAgent Agente;

    private Vida vida;
    private Animator animator;
    //private Collider collider;
    private Vida vidaJugador;
    private LogicaJugador logicaJugador;
    public bool Vida0 = false;
    public bool estaAtacando = false;
    public float speed = 1.0f;
    public float angularSpeed = 120;
    public float daño = 25;
    public bool mirando;

    public bool sumarPuntos;
    public GameObject puntajePantalla;

    // Start is called before the first frame update
    void Start()
    {

        audio = gameObject.GetComponent<AudioSource>();
        tarjet = GameObject.Find("Player");
        vidaJugador = tarjet.GetComponent<Vida>();

        if (vidaJugador == null)
        {
            throw new System.Exception("EL objeto Jugador no tiene componente Vida");
        }

        logicaJugador = tarjet.GetComponent<LogicaJugador>();

        if (logicaJugador == null)
        {
            throw new System.Exception("EL objeto Jugador no tiene componente LogicaJugador");
        }

        Agente = GetComponent<NavMeshAgent>();
        vida = GetComponent<Vida>();
        animator = GetComponent<Animator>();
        //collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        revisarVida();
        perseguir();
        revisarAtaque();
        estaFrenteJugador();
    }

    void estaFrenteJugador()
    {
        Vector3 adelante = transform.forward;
        Vector3 tarjeTJugador = (GameObject.Find("Player").transform.position - transform.position).normalized;

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
        Agente.destination = tarjet.transform.position;
    }

    void revisarAtaque()
    {
        if(Vida0) return;
        if(estaAtacando) return;
        if(logicaJugador.Vida0) return;
        float distanciaBlanco = Vector3.Distance(tarjet.transform.position, transform.position);

        if(distanciaBlanco <= 2.0 && mirando)
        {
            atacar();
        }
    }

    void atacar()
    {
        audio.Play();
        vidaJugador.RecibirDaño(daño);
        Agente.speed = 0;
        Agente.angularSpeed = 0;
        estaAtacando = true;
        animator.SetTrigger("DebeAtacar");
        Invoke("reiniciarAtaque", 1.2f);
    }

    void reiniciarAtaque()
    {
        estaAtacando = false;
        Agente.speed = speed;
        Agente.angularSpeed = angularSpeed;
    }
}
