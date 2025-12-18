using System.Collections;
using UnityEngine;

public class Acid : MonoBehaviour
{
    [SerializeField] int damageAmount;
    [SerializeField] float damageRate;
    [SerializeField] float residualDamageTime;
    float time;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
        {
            return;
        }
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            IDamage dmg = other.GetComponent<IDamage>();
            dmg.TakeDamage(damageAmount);
        }

    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.isTrigger)
        {
            return;
        }

        IDamage dmg = other.GetComponent<IDamage>();
        StartCoroutine (residualDamage(dmg));
        time = 0;
    }
   
   
    IEnumerator residualDamage(IDamage rD)
    {
        
       for (time = 0; time < residualDamageTime;  time += Time.deltaTime)
        {
            time += Time.deltaTime;
            StartCoroutine(screenFlashAcid());
            rD.TakeDamage(damageAmount);
            yield return new WaitForSeconds(damageRate);
        }
       
     
    }

    IEnumerator screenFlashAcid()
    {
        GameManager.instance.playerAcidPanel.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        GameManager.instance.playerAcidPanel.SetActive(false);
    }

}
