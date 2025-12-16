using System.Collections;
using UnityEngine;

public class FlipPlatformX : MonoBehaviour
{
    [SerializeField] float resetTime;
    [SerializeField] bool oneHundredEighty;

    Quaternion startRot;
    Quaternion flippedRot;

    float speed;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startRot = transform.rotation;
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
        
        yield return new WaitForSeconds(resetTime);

    }

}
