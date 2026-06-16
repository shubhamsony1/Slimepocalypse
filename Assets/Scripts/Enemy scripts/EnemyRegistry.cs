using System.Collections.Generic;
using UnityEngine;

public class EnemyRegistry : MonoBehaviour
{
    public static readonly List<Transform> Enemies = new List<Transform>();

    public static void Register(Transform enemy)
    {
        Enemies.Add(enemy);
    }

    public static void Unregister(Transform enemy)
    {
        Enemies.Remove(enemy);
    }
}