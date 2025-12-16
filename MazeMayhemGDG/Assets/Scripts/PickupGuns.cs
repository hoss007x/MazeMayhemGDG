using UnityEngine;

public class PickupGuns : MonoBehaviour
{
    [SerializeField] gunStats gun;


    private void OnTriggerEnter(Collider other)
    {
        IPickup pick = other.GetComponent<IPickup>();

        if (pick != null)
        {
<<<<<<< HEAD
            gun.ammoCurr = gun.ammoMax;
=======
            gun.ammoCurr = gun.magSize;
>>>>>>> 6d2dff04f4039c2e79427fdd9b293c83cb55d88f
            pick.getGunStats(gun);
            Destroy(gameObject);
        }
    }

}
