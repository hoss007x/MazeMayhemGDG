using UnityEngine;

public class destructibleWalls : MonoBehaviour, IDamage
{
    [SerializeField] int HP = 50;

    public void TakeDamage(int amount)
    {
        HP -= amount;
        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
