using UnityEngine;
using UnityEngine.InputSystem;

public class PlacementManager : MonoBehaviour
{
    public static PlacementManager Instance;

    private Camera cam;

    public GameObject selectedTurretPrefab;
    public GameObject selectedGhostPrefab;
    public int selectedCost;

    private GameObject ghostTurret;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (selectedGhostPrefab == null)
            return;

        UpdateGhostPosition();

        HandlePlacement();

        HandleCancel();
    }

    public void SelectTurret(
        GameObject turretPrefab,
        GameObject ghostPrefab,
        int cost)
    {
        selectedTurretPrefab = turretPrefab;
        selectedGhostPrefab = ghostPrefab;
        selectedCost = cost;

        CreateGhost();
    }

    void CreateGhost()
    {
        if (ghostTurret != null)
        {
            Destroy(ghostTurret);
        }

        ghostTurret = Instantiate(
            selectedGhostPrefab
        );
    }

    void UpdateGhostPosition()
    {
        Vector2 mouse =
            Mouse.current.position.ReadValue();

        Vector3 mousePos =
            Camera.main.ScreenToWorldPoint(
                new Vector3(
                    mouse.x,
                    mouse.y,
                    -Camera.main.transform.position.z
                )
            );

        mousePos.z = 0;

        ghostTurret.transform.position =
            mousePos;
    }

    void HandlePlacement()
    {
        if (!Mouse.current.leftButton.wasPressedThisFrame)
            return;

        PlacementValidator validator =
            ghostTurret.GetComponent<PlacementValidator>();

        if (validator == null)
            return;

        if (!validator.CanPlace)
            return;

        if (!CurrencyManager.Instance.SpendCoins(
                selectedCost))
        {
            Debug.Log("Not enough money");
            return;
        }

        Instantiate(
            selectedTurretPrefab,
            ghostTurret.transform.position,
            Quaternion.identity
        );
    }

    void HandleCancel()
    {
        if (!Mouse.current.rightButton.wasPressedThisFrame)
            return;

        CancelPlacement();
    }

    void CancelPlacement()
    {
        selectedTurretPrefab = null;
        selectedGhostPrefab = null;
        selectedCost = 0;

        if (ghostTurret != null)
        {
            Destroy(ghostTurret);
        }
    }
}