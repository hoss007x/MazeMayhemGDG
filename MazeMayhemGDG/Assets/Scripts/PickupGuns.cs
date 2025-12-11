using UnityEngine;

public class PickupGuns : MonoBehaviour
{
    [SerializeField] gunStats gun;


    private void OnTriggerEnter(Collider other)
    {
        IPickup pick = other.GetComponent<IPickup>();

        if (pick != null)
        {
            gun.ammoCurr = gun.ammoMax;
            pick.getGunStats(gun);
            Destroy(gameObject);
        }
    }

}
