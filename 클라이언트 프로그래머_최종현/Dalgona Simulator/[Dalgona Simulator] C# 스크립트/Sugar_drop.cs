using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sugar_drop : MonoBehaviour
{
    public GameObject sugar_white;
    public GameObject brown_mole;
    public GameObject swtch;
    public GameObject withBakingSoda;
    public GameObject effect;
    int sugarCount = 0;
    int bakingCount = 0;
    bool isWhite = false;
    bool isBakingsoda = false;

    void Update()
    {
        if (!isWhite && sugarCount == 5)
        {
            sugar_white.SetActive(true);
            isWhite = true;
        }

        if (!isBakingsoda && bakingCount == 3)
        {
            sugar_white.SetActive(false);
            withBakingSoda.SetActive(true);
            isBakingsoda = true;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SugarCase")
        {
            sugarCount++;
            effect.SetActive(true);
        }

        if (!swtch.activeInHierarchy && brown_mole.activeInHierarchy && other.gameObject.tag == "BakingSodaCase")
        {
            bakingCount++;
            effect.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        effect.SetActive(false);
    }

}
