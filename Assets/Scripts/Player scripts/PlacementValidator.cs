using UnityEngine;

public class PlacementValidator : MonoBehaviour
{
    [Header("Placement")]
    public LayerMask towerLayer;

    public bool CanPlace { get; private set; }

    private SpriteRenderer[] spriteRenderers;
    private Color[] originalColors;

    private BoxCollider2D boxCollider;

    private void Awake()
    {
        spriteRenderers =
            GetComponentsInChildren<SpriteRenderer>();

        originalColors =
            new Color[spriteRenderers.Length];

        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            originalColors[i] =
                spriteRenderers[i].color;
        }

        boxCollider =
            GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        CheckPlacement();
        UpdateColor();
    }

    void CheckPlacement()
    {
        if (boxCollider == null)
        {
            CanPlace = false;
            return;
        }

        Collider2D[] hits =
            Physics2D.OverlapBoxAll(
                boxCollider.bounds.center,
                boxCollider.bounds.size,
                0f,
                towerLayer
            );

        CanPlace = true;

        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject != gameObject)
            {
                CanPlace = false;
                break;
            }
        }
    }

    void UpdateColor()
    {
        foreach (SpriteRenderer sr in spriteRenderers)
        {
            sr.color =
                CanPlace
                ? Color.white
                : Color.red;
        }
    }

    private void OnDrawGizmosSelected()
    {
        BoxCollider2D box =
            GetComponent<BoxCollider2D>();

        if (box == null)
            return;

        Gizmos.color =
            CanPlace ? Color.green : Color.red;

        Gizmos.DrawWireCube(
            box.bounds.center,
            box.bounds.size
        );
    }
}