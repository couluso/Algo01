using Unity.Collections.Tests.CoreCLR.TestJobs;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    //Faire aparaître un mob quand je loueur appuit sur E.*
    public KeyCode spawn = KeyCode.E;

    public KeyCode colles = KeyCode.F;

    public GameObject mob;
    public GameObject cubes;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Faire apparaître un mob :
        //Instantiate(mob);


    }

    // Update is called once per frame
    void Update()
    {
        //Faire apparaître un mob, à la position 0, à la rotation 0
        if (Input.GetKeyDown(spawn))
            Instantiate(mob, Vector3.zero , Quaternion.identity);

        if (Input.GetKeyDown(colles))
            Instantiate(cubes, Vector3.zero, Quaternion.identity);

    }
}
