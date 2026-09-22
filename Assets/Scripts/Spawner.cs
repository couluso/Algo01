using Unity.Collections.Tests.CoreCLR.TestJobs;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    //Faire aparaître un mob quand je loueur appuit sur E.*
    public KeyCode spawn = KeyCode.E;

    public KeyCode colles = KeyCode.F;

    public float espace = 0;

    public GameObject terrain;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < 100; i++)
        {
            Instantiate(terrain, new Vector3(i, 0, i), Quaternion.identity);
        }

    }

    // Update is called once per frame
    void Update()
    {
    }
}
