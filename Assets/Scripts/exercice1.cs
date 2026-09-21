using JetBrains.Annotations;
using NUnit.Framework;
using Unity.Collections.Tests.CoreCLR.TestJobs;
using UnityEngine;

public class exercice1 : MonoBehaviour
{
    public int age = 18;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Si le joueur majeur
        if (age >= 18)
            Debug.Log("Welcome to the game lil' fucker");

        //Sinon
        else
            Debug.Log("gtfo my face");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
