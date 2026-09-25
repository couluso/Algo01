using TMPro;
using UnityEngine;


public class TriggerOrbe : MonoBehaviour
{
    public int coins = 0;
    public GameObject texte;

    private void OnTriggerEnter(Collider Orbe)
    {
        coins++;
        Orbe.gameObject.SetActive(false);
        texte.GetComponent<TextMeshProUGUI>().text = "Orbes collectées : " + coins;
    }
}
