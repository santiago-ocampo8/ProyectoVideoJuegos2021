using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Botiquin : MonoBehaviour
{
    private Animator animator;

    private void Start() {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player"))
        {
            animator.SetTrigger("Entrada");
            other.GetComponent<Vida>().Curar(25);
            Destroy(gameObject,2.2f);
        }
    }

}
