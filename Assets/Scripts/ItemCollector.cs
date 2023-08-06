using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class ItemCollector : MonoBehaviour
{
    private int cherries = 0;
    [SerializeField] private Text cherriesText;
    [SerializeField] private AudioSource collectSoundEffect; 
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cherry"))
        {
            collectSoundEffect.Play(); 
            Destroy(collision.gameObject);
            cherries ++;
            cherriesText.text = "Cheeries: " + cherries;
        }
    }


}
