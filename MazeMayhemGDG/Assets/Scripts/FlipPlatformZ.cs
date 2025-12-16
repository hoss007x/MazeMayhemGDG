using System.Collections;
using UnityEngine;

public class FlipPlatformZ : MonoBehaviour
{
<<<<<<< HEAD
    [Range(50, 1000)][SerializeField] float speed;
    [SerializeField] float resetTime;
    [SerializeField] float degree;
=======
    [SerializeField] float resetTime;
    [SerializeField] bool oneHundredEighty;
>>>>>>> 6d2dff04f4039c2e79427fdd9b293c83cb55d88f

    Quaternion startRot;
    Quaternion flippedRot;

<<<<<<< HEAD
    float time;
=======
    float speed;

   
>>>>>>> 6d2dff04f4039c2e79427fdd9b293c83cb55d88f

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startRot = transform.rotation;
<<<<<<< HEAD
        flippedRot = startRot * Quaternion.Euler(0, 0, degree);

        StartCoroutine(RotateRoutine());

=======
        if (oneHundredEighty)
        {
            flippedRot = startRot * Quaternion.Euler(0, 0, 180);
            speed = 250;
        }
        else
        {
            flippedRot = startRot * Quaternion.Euler(0, 0, 90);
            speed = 50;
        }
        StartCoroutine(RotateRoutine());
>>>>>>> 6d2dff04f4039c2e79427fdd9b293c83cb55d88f
    }
    IEnumerator RotateRoutine()
    {
        while (true)
        {
            yield return RotateToRot(flippedRot);

            yield return RotateToRot(startRot);

        }
    }
    IEnumerator RotateToRot(Quaternion target)
    {

        while (Quaternion.Angle(transform.rotation, target) > 0.01f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, target, speed * Time.deltaTime);
            yield return null;

        }
        transform.rotation = target;
        yield return new WaitForSeconds(resetTime);

    }
}
