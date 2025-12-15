using System.Collections;
using UnityEngine;

public class FlipPlatformY : MonoBehaviour
{
    [Range(50, 1000)] [SerializeField] float speed;
    [SerializeField] float resetTime;
    [SerializeField] float degree;

    Quaternion startRot;
    Quaternion flippedRot;

    float time;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startRot = transform.rotation;
        flippedRot = startRot * Quaternion.Euler(0,degree,0);

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
        transform.rotation = target;
        yield return new WaitForSeconds(resetTime);

    }

}
