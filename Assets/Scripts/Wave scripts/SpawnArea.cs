using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    [Header("Center")]
    public Transform centerTarget;

    [Header("Spawn Ring")]
    public float innerRadius = 10f;
    public float outerRadius = 25f;

    public Vector3 GetRandomSpawnPosition()
    {
        float angle = Random.Range(0f, Mathf.PI * 2f);

        float distance = Mathf.Sqrt(Random.Range(innerRadius * innerRadius,outerRadius * outerRadius));

        float x = Mathf.Cos(angle) * distance;
        float y = Mathf.Sin(angle) * distance;

        return centerTarget.position + new Vector3(x, y, 0f);
    }

    private void OnDrawGizmosSelected()
    {
        if (centerTarget == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(centerTarget.position, innerRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(centerTarget.position, outerRadius);
    }
    private void OnDrawGizmos()
    {
        if (centerTarget == null)
            return;

        Gizmos.color = Color.yellow;

        for (int i = 0; i < 100; i++)
        {
            Vector3 pos = GetRandomSpawnPosition();
            Gizmos.DrawSphere(pos, 0.2f);
        }
    }
}