using System.Collections;
using UnityEngine;

public class MovingPlatformsY : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float distance;
    [SerializeField] float resetTime;

    Vector3 startPos;
    Vector3 downPos;

  

    float time;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
        downPos = startPos + Vector3.down * distance;

        StartCoroutine(MoveRoutine());
        
    }
    IEnumerator MoveRoutine()
    {
        while (true)
        {
            yield return MoveToPosition(startPos, downPos);

            yield return MoveToPosition(downPos, startPos);
        }
    }
    IEnumerator MoveToPosition(Vector3 start,Vector3 targetPos)
    {
        time = 0;
        while (time < 1 )
        {
            time += Time.deltaTime * speed;
            transform.position = Vector3.Lerp(start, targetPos, time);
            yield return null;
        }

        transform.position = targetPos;
        yield return new WaitForSeconds(resetTime);
    }
}
 
