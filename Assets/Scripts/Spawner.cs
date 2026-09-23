using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UnityEngine;


// ALEATOIRE = Random.Range

public class NewMonoBehaviourScript : MonoBehaviour
{
    public GameObject terrain;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //PLATEFORME
        for (int i = 0; i < 60; i++)
        {
            for (int j = 0; j < 60; j++)
            {
                if (Random.Range(0, 10) != 0)
                {
                    Instantiate(terrain, new Vector3(i, 0, j), Quaternion.identity);

                }

            }
        }


        for (int i = 0; i <= 60; i++)
        {
            for (int j = 0; j < 60; j++)
            {
                //Si je suis sur un bord
                if (i == 0 || j == 0 || i == 59 || j == 59)
                {
                    Instantiate(terrain, new Vector3(i, 1, j), Quaternion.identity);
                    Instantiate(terrain, new Vector3(i, 2, j), Quaternion.identity);

                    if (i == 0 && j == 0 || i == 0 && j == 59 || i == 59 && j == 59 || i == 59 && j == 0)
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

