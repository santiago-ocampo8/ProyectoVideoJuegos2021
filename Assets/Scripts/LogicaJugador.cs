using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicaJugador : MonoBehaviour
{
    public Vida vida;
    public bool Vida0 = false;
    [SerializeField]
    private Animator animatorPerder;
    public Puntaje puntaje;

    // Start is called before the first frame update
    void Start()
    {
        vida = GetComponent<Vida>();
        puntaje.valor = 0;
    }

    // Update is called once per frame
    void Update()
    {
        revisarVida();
    }

    void revisarVida()
    {
        if (Vida0) return;
        if(vida.valor <= 0)
        {
            Vida0 = true;
            Invoke("reiniciarJuego", 4f);
        }
    }

    void reiniciarJuego()
    {
        //SceneManager.LoadSceneAsync(0);
        //SceneManager.UnloadScene(SceneManager.GetActiveScene().name);
        //SceneManager.LoadScene(0,LoadSceneMode.Single);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible=true;
        // foreach(GameObject g in SceneManager.GetActiveScene().GetRootGameObjects())
        // {
        //     g.SetActive (false);
                                 
        // }
        //Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(0);
        // foreach(GameObject g in SceneManager.GetSceneByName("Menu").GetRootGameObjects())
        // {
        //     g.SetActive (true);
                                     
        // }
        
        
        puntaje.valor = 0;
        AudioListener.volume = 1f;
    }
}
