using System.Collections;
using UnityEngine;

public class MovingPlatformsX : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float dist;
    [SerializeField] float resetTime;

    Vector3 startPos;
    Vector3 leftPos;

    float time;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
        leftPos = startPos + Vector3.left * dist;

        StartCoroutine(MoveRoutine());
    }
    IEnumerator MoveRoutine()
    {
        while (true)
        {
            yield return MoveToPos(startPos, leftPos);

            yield return MoveToPos(leftPos, startPos);
        }
    }
    IEnumerator MoveToPos(Vector3 startPos, Vector3 targetPos)
    {
        time = 0;
        while (time < 1)
        {
            time += Time.deltaTime * speed;
            transform.position = Vector3.Lerp(startPos, targetPos, time);
            yield return null;
        }
        transform.position = targetPos;
        yield return new WaitForSeconds(resetTime);
    }
}
