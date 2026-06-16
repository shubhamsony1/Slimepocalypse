using UnityEngine;

public class ShopButton : MonoBehaviour
{
    public GameObject turretPrefab;
    public GameObject ghostPrefab;
    public int cost = 100;

    public void Select()
    {
        PlacementManager.Instance.SelectTurret(
            turretPrefab,
            ghostPrefab,
            cost
        );
    }
}