using UnityEngine;

public class items : MonoBehaviour
{
    enum itemType { healing, faster, stronger }
    [SerializeField] itemType type;

    [SerializeField] int healAmount;
    [SerializeField] int damageIncrease;
    [SerializeField] int speedIncrease;
    [SerializeField] int buffTimer;

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
        { return; }

        ITypesOfItems buff = other.GetComponent<ITypesOfItems>();

        if (buff != null && type == itemType.healing)
        {
            buff.healing(healAmount);
        }
        if (buff != null && type == itemType.faster)
        {
            buff.faster(speedIncrease);
        }
        if (buff != null && type == itemType.stronger)
        {
            buff.stronger(damageIncrease);
        }
        if (buff != null)    
        {
            Destroy(gameObject);
        }
    }

}
