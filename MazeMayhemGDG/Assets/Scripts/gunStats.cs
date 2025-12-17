using UnityEngine;

[CreateAssetMenu]

public class gunStats : ScriptableObject
{
    public GameObject gunModel;
    [Range(1, 10)][SerializeField] public int ShootDamage;
    [Range(3, 1000)][SerializeField] public int ShootDistances;
    [Range(0.1f, 3)][SerializeField] public float ShootRate;
    [Range(0, 100)][SerializeField] public int ammoMax;
    [Range(1, 100)][SerializeField] public int magSize;
    [Range(1, 100)][SerializeField] public int maxAmmmoAmount;


    public int ammoCurr;
    public ParticleSystem hitEffect;
    public AudioClip[] shootsound;
    [Range(0, 1)] public float shootSoundVol;
}
