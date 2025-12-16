using System.Collections;
using UnityEngine;

public class FlipPlatformX : MonoBehaviour
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
        flippedRot = startRot * Quaternion.Euler(degree, 0, 0);

=======
        if (oneHundredEighty)
        {
            flippedRot = startRot * Quaternion.Euler(180, 0, 0);
            speed =250;
        }
        else
        {
            flippedRot = startRot * Quaternion.Euler(90, 0, 0);
            speed = 50;
        }
>>>>>>> 6d2dff04f4039c2e79427fdd9b293c83cb55d88f
        StartCoroutine(RotateRoutine());

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
<<<<<<< HEAD
        transform.rotation = target;
=======
        
>>>>>>> 6d2dff04f4039c2e79427fdd9b293c83cb55d88f
        yield return new WaitForSeconds(resetTime);

    }

}
