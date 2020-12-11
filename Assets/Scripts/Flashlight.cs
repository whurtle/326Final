using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public Enemy enemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        enemy.OnTriggerEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        enemy.OnTriggerExit(other);
    }
}
