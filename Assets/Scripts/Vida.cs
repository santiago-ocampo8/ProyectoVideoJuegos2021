using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida : MonoBehaviour
{
    public float valor = 100;
    public Vida padreRef;
    public float multiplicadorDaño = 1.0f;
    public GameObject textFlotantePrefab;
    public float dañoTotal;

    float maximoVida;

    // Start is called before the first frame update
    void Start()
    {
        maximoVida = valor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RecibirDaño(float daño)
    {
        daño *= multiplicadorDaño;
        if(padreRef != null)
        {
            padreRef.RecibirDaño(daño);
            return;
        }

        valor -= daño;
        dañoTotal = daño;
        if(valor >= 0) MostrarTextoFlotante();
        if(valor < 0)
        {
            valor = 0;
            MostrarTextoFlotante();
        }
    }

    void MostrarTextoFlotante()
    {
        var go = Instantiate(textFlotantePrefab,transform.position,Quaternion.identity, transform);
        go.GetComponent<TextMesh>().text = dañoTotal.ToString();
        //go.GetComponent<TextMesh>().text = valor.ToString();
    }

    public void Curar(float curacion)
    {
        if((valor + curacion) > maximoVida)
        {
            valor = maximoVida;
        }
        else
        {
            valor += curacion;
        }
    }
}
