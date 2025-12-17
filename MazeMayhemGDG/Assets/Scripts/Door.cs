using TMPro;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] TMP_Text Text;
    
    [SerializeField] GameObject TextSpawner;
    [SerializeField] int keysRequired;
    [SerializeField] GameObject wall;

    
    string message;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Text.gameObject.SetActive(false);
        message = "sorry you don't have enough keys.\n Keys Required: " + keysRequired;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && GameManager.instance.Keys < keysRequired)
        { 
            Text.text = message;
            Text.gameObject.SetActive(true);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Text.gameObject.SetActive(false);
    }
}
