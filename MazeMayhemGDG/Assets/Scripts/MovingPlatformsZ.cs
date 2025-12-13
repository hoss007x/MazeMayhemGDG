using System.Collections;
using UnityEngine;

public class MovingPlatformsZ : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float dist;
    [SerializeField] float resetTime;

    Vector3 startPos;
    Vector3 fwdPos;

    float time;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
        fwdPos = startPos + Vector3.forward * dist;

        StartCoroutine(MoveRoutine());
    }
    IEnumerator MoveRoutine()
    {
        while (true)
        {
            yield return MoveToPos(startPos, fwdPos);

            yield return MoveToPos(fwdPos, startPos);
        }
    }
    IEnumerator MoveToPos(Vector3 start, Vector3 target)
    {
        time = 0;
        while (time < 1)
        {
            time += Time.deltaTime * speed;
            transform.position = Vector3.Lerp(start, target, time);
            yield return null;
        }
        transform.position = target;
        yield return new WaitForSeconds(resetTime);
    }


}
