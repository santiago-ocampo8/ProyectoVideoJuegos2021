using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PantallaVida : MonoBehaviour
{
    public GameObject salud;
    public GameObject gameOver;
    public Vida vida;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float R = vida.valor / 100;
        this.salud.GetComponent<Image>().fillAmount = R;

        if(vida.valor <= 0)
        {
            this.gameOver.SetActive(true);
        }
    }
}
