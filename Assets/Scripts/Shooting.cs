using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    //Primer paso: Generar la flecha en el sitio correcto y darle un impulso
    //Tranform el origen, controlar el input Input.GetMouseButton(0)
    //Segundo paso: El impulso depende de cuanto mantenemos el click
    //Numero de flechas, tiempo de recarga etc...
    public Transform spawnPoint;
    public GameObject arrow;
    public float throwForce;

    public float timeToShoot = 5.0f;

    float time = 0;
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            time += Time.deltaTime;

        }
        if (Input.GetMouseButtonUp(0))
        {
            if (time > timeToShoot)
            {
                time = timeToShoot;
            }
            var go = Instantiate(arrow, spawnPoint.position, spawnPoint.transform.rotation);
            go.AddComponent<Rigidbody>().AddForce(spawnPoint.forward * throwForce * time);
            time = 0;
        }
    }
}
