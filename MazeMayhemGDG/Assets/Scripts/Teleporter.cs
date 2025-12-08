using System.Collections;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform player;
    public Transform receiver;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Vector3 portalToPlayer = player.position - transform.position;
            float dotProduct = Vector3.Dot(transform.up, portalToPlayer);

            if (dotProduct < 0f)
            {
                float rotationDiff = -Quaternion.Angle(transform.rotation, receiver.rotation);
                rotationDiff += 180;
                player.Rotate(Vector3.up, rotationDiff);

                Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;
                player.position = receiver.position + positionOffset;

                player.GetComponent<CharacterController>().enabled = false;
                player.GetComponent<CharacterController>().enabled = true;
            }
        }
    }
}
