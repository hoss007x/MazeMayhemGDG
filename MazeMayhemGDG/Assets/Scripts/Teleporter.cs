using System.Collections;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform player;
    public Transform receiver;

    Vector3 porPos;
    Vector3 playerOffset;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Vector3 portalToPlayer = player.position - transform.position;
            float dotProduct = Vector3.Dot(transform.up, portalToPlayer);

            if (dotProduct < 0f)
            {
                porPos = receiver.position;
                float rotationDiff = -Quaternion.Angle(transform.rotation, receiver.rotation);
                rotationDiff += 180;
                player.Rotate(Vector3.up, rotationDiff);
                playerOffset = new Vector3(porPos.x, porPos.y, porPos.z + 3);
                Vector3 positionOffset = Quaternion.Euler(0, rotationDiff, 0) * portalToPlayer;
                player.position = playerOffset + positionOffset;

                player.GetComponent<CharacterController>().enabled = false;
                player.GetComponent<CharacterController>().enabled = true;
            }
        }
    }
}
