using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;
public class CanvasPause : MonoBehaviour
{

    [SerializeField] private GameObject menuPausa;

    private FirstPersonController playerScript1;


    private LogicaArma armasScript;
    private BalanceoArma armasScript2;


    void Start(){
        playerScript1=GameObject.Find("Player").GetComponent<FirstPersonController>();
        
        armasScript=GameObject.Find("UMP45").GetComponent<LogicaArma>();
        armasScript2=GameObject.Find("UMP45").GetComponent<BalanceoArma>();
    }
    void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            if (Time.timeScale == 1)
            {
                playerScript1.enabled=false;
  
                armasScript.enabled=false;
                armasScript2.enabled=false;
                Cursor.visible=true;
                Cursor.lockState = CursorLockMode.None;
                menuPausa.SetActive(true);
                Time.timeScale = 0;
            }
            else if (Time.timeScale == 0)
            {
                Reanudar();
            }
        }

    }
    public void Reanudar()
    {
        playerScript1.enabled=true;
        armasScript.enabled=true;
        armasScript2.enabled=true;
        Cursor.visible=false;
        Cursor.lockState = CursorLockMode.Locked;
        menuPausa.SetActive(false);
        Time.timeScale = 1;
    }



}
