using JetBrains.Annotations;
using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Serp : MonoBehaviour
{
    public GameObject A;
    public GameObject B;
    public GameObject C;
    public GameObject Favoris;
    public GameObject Choix;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Favoris = A;

        for (int i = 0; i < 10000; i++)
        {
            // Choisir un au hasard (A=0, B=1 et C=2)
            int rng = Random.Range(0, 2);
            if (rng == 0 && Favoris != A)
                Choix = A;
            else if (rng == 1 && Favoris != B)
                Choix = B;
            else 
                Choix = C;

            Debug.Log("Favoris = " + Favoris + ", " + "Choix = " + Choix);

            Vector3 position = Vector3.Lerp(Favoris.transform.position, Choix.transform.position, 0.5f);
            Favoris = Instantiate(Choix, position, Quaternion.identity);


        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
