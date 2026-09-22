using JetBrains.Annotations;
using NUnit.Framework;
using UnityEngine;

public class exercice1 : MonoBehaviour
{
    public KeyCode spawn = KeyCode.E;
    public GameObject substance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(spawn))
            Instantiate(substance, new Vector3(25, 2, 25), Quaternion.identity);
    }
}
