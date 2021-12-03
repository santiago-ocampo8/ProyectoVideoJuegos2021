using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Municion : MonoBehaviour
{
    private Animator animator;
    public ControladorArmas controladorArmas;

    private void Start() {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player"))
        {
            animator.SetTrigger("Entrada");
            //other.GetComponent<Vida>().Curar(25);
            controladorArmas.GetComponent<ControladorArmas>().RecargarMunicion();
            Destroy(gameObject,2.2f);
        }
    }
    
}
