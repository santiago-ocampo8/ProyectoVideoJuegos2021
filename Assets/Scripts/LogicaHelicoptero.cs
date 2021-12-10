using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicaHelicoptero : MonoBehaviour
{
    GameObject jugador;
    // Start is called before the first frame update
    void Start()
    {
        jugador = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        double distancia =Vector3.Distance(this.transform.position,this.jugador.transform.position);

        if(distancia <= 10)
        {
            Invoke("reiniciar", 1.5f);
        }
    }

     void reiniciar()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible=true;
        SceneManager.LoadScene(2);
    }

}
