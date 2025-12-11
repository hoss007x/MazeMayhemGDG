using UnityEngine;

[CreateAssetMenu]

public class gunStats : ScriptableObject
{
    public GameObject gunModel;

    [Range(1, 10)][SerializeField] public int ShootDamage;
    [Range(3, 1000)][SerializeField] public int ShootDistances;
    [Range(0.1f, 3)][SerializeField] public float ShootRate;

    public int ammoCurr;
    [Range(5, 50)] public int ammoMax;

    public ParticleSystem hitEffect;
    public AudioClip[] shootsound;
    [Range(0, 1)] public float shootSoundVol;
}
