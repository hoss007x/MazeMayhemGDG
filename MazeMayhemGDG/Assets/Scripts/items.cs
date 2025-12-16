using UnityEngine;

public class items : MonoBehaviour
{
    enum itemType { healing, faster, stronger }
    [SerializeField] itemType type;

    [SerializeField] int healAmount;
    [SerializeField] int damageIncrease;
    [SerializeField] int speedIncrease;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // different sound effects need here for different items collected 
    }

    // Update is called once per frame
    void Update()
    {
        RotateItem();
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

    void RotateItem()
    {
        Vector3 rotSpeed = new Vector3 (0,0,0);
        if (type == itemType.faster)
        {
            rotSpeed = new Vector3 (45, 45, 0);
        }
        else if(type == itemType.healing)
        {
            rotSpeed = new Vector3(0, 90, 0);
        }
        else if(type == itemType.stronger)
        {
            rotSpeed = new Vector3(0, 90, 0);
        }
        transform.Rotate(rotSpeed * Time.deltaTime); 
    }

}
