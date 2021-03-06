using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ModoDeDisparo 
{
    SemiAuto,
    FullAuto
}

public class LogicaArma : MonoBehaviour
{
    protected Animator animator;
    protected AudioSource audioSource;
    public bool tiempoNoDisparo = false;
    public bool puedeDisparar = false;
    public bool recargando = false;

    [Header("Referencia de Objetos")]
    public ParticleSystem fuegoDeArma;
    public Camera camaraPrincipal;
    public Transform puntoDisparo;
    public GameObject efectoDañoPrefab;
    public GameObject Coordenadas;

    [Header("Referencia de Sonidos")]
    public AudioClip sonDisparo;
    public AudioClip sonSinBalas;
    public AudioClip sonCartuchoEntra;
    public AudioClip sonCartuchoSale;
    public AudioClip sonVacio;
    public AudioClip sonDesenfundar;

    [Header("Atributos del Arma")]
    public ModoDeDisparo modoDeDisparo = ModoDeDisparo.FullAuto;
    public float daño = 20f;
    public float ritmoDeDisparo = 0.3f;
    public int balasRestantes;
    public int balasEnCartucho;
    public int tamañoDeCartucho = 12;
    public int maximoDeBalas = 100;
    public bool estadoADS = false;
    public Vector3 disCadera;
    public Vector3 ADS;
    public float tiempoApuntar;
    public float zoom;
    public float normal;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        
        balasEnCartucho = tamañoDeCartucho;
        balasRestantes = maximoDeBalas;

        Invoke("HabilitarArma", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if(modoDeDisparo == ModoDeDisparo.FullAuto && Input.GetButton("Fire1"))
        {
            RevisarDisparo();
        }
        else if(modoDeDisparo == ModoDeDisparo.SemiAuto && Input.GetButtonDown("Fire1"))
        {
            RevisarDisparo();
        }

        if(Input.GetButtonDown("Reload"))
        {
            RevisarRecargar();
        }

        if(Input.GetMouseButton(1))
        {
            transform.localPosition = Vector3.Slerp(transform.localPosition, ADS, tiempoApuntar * Time.deltaTime);
            estadoADS = true;
            camaraPrincipal.fieldOfView = Mathf.Lerp(camaraPrincipal.fieldOfView, zoom, tiempoApuntar / Time.deltaTime);
        }

        if(Input.GetMouseButtonUp(1))
        {
            estadoADS = false;
        }

        if(estadoADS == false)
        {
            transform.localPosition = Vector3.Slerp(transform.localPosition, disCadera, tiempoApuntar * Time.deltaTime);
            camaraPrincipal.fieldOfView = Mathf.Lerp(camaraPrincipal.fieldOfView, normal, tiempoApuntar / Time.deltaTime);
        }
        
    }

    void HabilitarArma()
    {
        puedeDisparar = true;
    }

    void RevisarDisparo()
    {
        if(!puedeDisparar) return;
        if(tiempoNoDisparo) return;
        if(recargando) return;
        if(balasEnCartucho > 0)
        {
            Disparar();
        }
        else
        {
            SinBalas();
        }
    }

    void Disparar()
    {
        audioSource.PlayOneShot(sonDisparo);
        tiempoNoDisparo = true;
        fuegoDeArma.Stop();
        fuegoDeArma.Play();
        ReproducirAnimacionDisparo();
        balasEnCartucho--;
        StartCoroutine(ReiniciarTiempoNoDisparo());
        DisparoDirecto();
    }

    public void CrearEfectoDaño(Vector3 pos, Quaternion rot)
    {
        GameObject efectoDaño = Instantiate(efectoDañoPrefab, pos, rot);
        efectoDaño.transform.SetParent(Coordenadas.transform);
        Destroy(efectoDaño, 1f);
    }

    void DisparoDirecto()
    {
        RaycastHit hit;
        if(Physics.Raycast(puntoDisparo.position, puntoDisparo.forward, out hit))
        {
            if(hit.transform.CompareTag("Enemigo"))
            {
                Vida vida = hit.transform.GetComponent<Vida>();
                if(vida == null)
                {
                    throw new System.Exception("No se encontro el componente Vida del Enemigo");
                }
                else
                {
                    vida.RecibirDaño(daño);
                    CrearEfectoDaño(hit.point, hit.transform.rotation);
                }
            }
        }
    }

    public virtual void ReproducirAnimacionDisparo()
    {
        if (gameObject.name == "Police9mm")
        {
            if (balasEnCartucho > 1)
            {
                animator.CrossFadeInFixedTime("Fire", 0.1f);
            }
            else 
            {
                animator.CrossFadeInFixedTime("FireLast", 0.1f);
            }
        } 
        else 
        {
            animator.CrossFadeInFixedTime("Fire", 0.1f);
        }
    }

    void SinBalas()
    {
        audioSource.PlayOneShot(sonSinBalas);
        tiempoNoDisparo = true;
        StartCoroutine(ReiniciarTiempoNoDisparo());
    }

    IEnumerator ReiniciarTiempoNoDisparo()
    {
        yield return new WaitForSeconds(ritmoDeDisparo);
        tiempoNoDisparo = false;

    }

    public void RevisarRecargar() 
    {
        if (balasRestantes > 0 && balasEnCartucho < tamañoDeCartucho)
        {
            Recargar();
        }
    }

    void Recargar()
    {
        if (recargando) return;
        recargando = true;
        animator.CrossFadeInFixedTime("Reload", 0.1f);
    }

    void RecargarMuniciones()
    {
        int balasParaRecargar = tamañoDeCartucho - balasEnCartucho;
        int restarBalas = (balasRestantes >= balasParaRecargar) ? balasParaRecargar : balasRestantes;

        balasRestantes -= restarBalas;
        balasEnCartucho += balasParaRecargar;
    }
    
    public void DesenfundarOn()
    {
        audioSource.PlayOneShot(sonDesenfundar);
    }

    public void CartuchoEntraOn()
    {
        audioSource.PlayOneShot(sonCartuchoEntra);
        RecargarMuniciones();
    }

    public void CartuchoSaleOn()
    {
        audioSource.PlayOneShot(sonCartuchoSale);
        RecargarMuniciones();
    }

    public void VacioOn()
    {
        audioSource.PlayOneShot(sonVacio);
        Invoke("ReiniciarRecargar", 0.1f);
    }

    void ReiniciarRecargar()
    {
        recargando = false;
    }

    public void Municion()//int municion
    {
        balasRestantes = maximoDeBalas;
        // if((balasRestantes + municion) > maximoDeBalas)
        // {
        //     balasRestantes = maximoDeBalas;
        // }
        // else
        // {
        //     balasRestantes += municion;
        // }
    }
}
