using System.Security.Cryptography;
using UnityEngine;


// ALEATOIRE = Random.Range

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
        //PLATEFORME
        for (int i = 0; i < 50; i++)
        {
            for (int j = 0; j < 50; j++)
            {
                float perlinY = Mathf.PerlinNoise(i * 0.05f, j * -0.05f) * 3;
                if (Random.Range(0, 10) != 0)
                {
                    Instantiate(terrain, new Vector3(i, perlinY, j), Quaternion.identity);

                }

            }
        }


        for (int i = 0; i <= 50; i++)
        {
            for (int j = 0; j < 50; j++)
            {
                //Si je suis sur un bord
                if (i == 0 || j == 0 || i == 49 || j == 49)
                {
                    Instantiate(terrain, new Vector3(i, 1, j), Quaternion.identity);
                    Instantiate(terrain, new Vector3(i, 2, j), Quaternion.identity);

                    if (i == 0 && j == 0 || i == 0 && j == 49 || i == 49 && j == 49 || i == 49 && j == 0)
                    {
                        for (int k = 0; k < 10; k++)
                        {
                            Instantiate(terrain, new Vector3(i, k, j), Quaternion.identity);
                        }
                    }
                }
            }
        }
    }
}

