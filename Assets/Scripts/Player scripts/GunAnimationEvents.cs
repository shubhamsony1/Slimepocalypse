using UnityEngine;

public class GunAnimationEvents : MonoBehaviour
{
    private Turret turret;

    private void Awake()
    {
        turret = GetComponentInParent<Turret>();
    }

    public void FireBullet()
    {
        if (turret != null)
        {
            turret.FireBullet();
        }
    }
}