using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaBarrera : MonoBehaviour
{
    [SerializeField] private GameObject titan;
    [SerializeField] private GameObject barrera;
    
    private Vida _vida;
    void Start()
    {
        _vida=titan.GetComponent<Vida>();        
    }

    void Update()
    {
        if (_vida.valor==0){
            barrera.SetActive(false);
        }
    }
}
