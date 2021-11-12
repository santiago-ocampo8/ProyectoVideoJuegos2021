using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorEnemigos : MonoBehaviour
{
    public GameObject zombiePrefab;
    public Transform[] puntosGeneracion;

    public GameObject Coordenadas;
    public float tiempoGeneracion = 5f;

    GameObject zombie;

    // Start is called before the first frame update
    void Start()
    {
        puntosGeneracion = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            puntosGeneracion[i] = transform.GetChild(i);
        }

        StartCoroutine(AparecerEnemigo());
    }

    IEnumerator AparecerEnemigo()
    {
        while(true)
        {
            for (int i = 0; i < puntosGeneracion.Length; i++)
            {
                Transform pGenereacion = puntosGeneracion[i];
                zombie = Instantiate(zombiePrefab, pGenereacion.position, pGenereacion.rotation);
                zombie.transform.SetParent(Coordenadas.transform);
            }
            yield return new WaitForSeconds(tiempoGeneracion);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
