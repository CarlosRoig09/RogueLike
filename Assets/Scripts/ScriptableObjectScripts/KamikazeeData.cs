using UnityEngine;

[CreateAssetMenu(fileName = "KamikazeeData", menuName = "KamikazeeData")]
public class KamikazeeData : EnemyData
{
    public float durationOfExplosion;
    public float explosionDamage;
    public float explosionImpulse;
    public GameObject explosion;
}
