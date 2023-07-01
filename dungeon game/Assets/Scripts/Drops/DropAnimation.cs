using UnityEngine;
using System.Collections;

public class DropAnimation : MonoBehaviour
{
    [SerializeField]
    private float heightY = 1.5f;
    [SerializeField]
    private float popDuration = 1f;
    private Vector3 popVector;


    public IEnumerator AnimCurveSpawnRoutine(AnimationCurve animCurve, Transform dropTransform)
    {
        Vector2 startPoint = dropTransform.position;
        float randomX = dropTransform.position.x + Random.Range(-2f, 2f);
        float randomY = dropTransform.position.y + Random.Range(-1f, 1f);

        Vector2 endPoint = new Vector2(randomX, randomY);

        float timePassed = 0f;

        while (timePassed < popDuration)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed / popDuration;
            float heightT = animCurve.Evaluate(linearT);
            float height = Mathf.Lerp(0f, heightY, heightT);

            popVector = Vector2.Lerp(startPoint, endPoint, linearT) + new Vector2(0f, height);
            dropTransform.position = popVector;
            yield return null;
        }
    }
}
