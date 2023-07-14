using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField]
    private float duration = 1.5f;
    [SerializeField]
    private AnimationCurve animCurve;
    [SerializeField]
    private float heightY = 4f;
    [SerializeField]
    private GameObject bombShadow;
    [SerializeField]
    private GameObject bombImpact;

    public IEnumerator ProjectileCurveRoutine(Vector3 startPosition, Vector3 endPosition)
    {
        GameObject spawnedShadow = ObjectPool.SpawnObject(
            bombShadow,
            transform.position + new Vector3(0, -0.3f, 0),
            Quaternion.identity);

        Vector3 bombStartPosition = spawnedShadow.transform.position;
        StartCoroutine(MoveBombShadowRoutine(spawnedShadow, bombStartPosition, endPosition));
        float timePassed = 0f;

        while (timePassed < duration)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed / duration;
            float heightT = animCurve.Evaluate(linearT);
            float height = Mathf.Lerp(0f, heightY, heightT);

            transform.position =
                Vector2.Lerp(startPosition, endPosition, linearT) + new Vector2(0f, height);

            yield return null;
        }

        ObjectPool.SpawnObject(bombImpact, transform.position, Quaternion.identity);
        ObjectPool.ReturnObjectToPool(this.gameObject);    
}

    private IEnumerator MoveBombShadowRoutine(
        GameObject bombShadow,
        Vector3 startPosition,
        Vector3 endPosition)
    {
        float timePassed = 0f;

        while (timePassed < duration)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed / duration;

            bombShadow.transform.position = Vector2.Lerp(startPosition, endPosition, linearT);

            yield return null;
        }

        ObjectPool.ReturnObjectToPool(bombShadow);
    }
}