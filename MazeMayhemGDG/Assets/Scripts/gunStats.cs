using UnityEngine;

[CreateAssetMenu]

public class gunStats : ScriptableObject
{
    public GameObject gunModel;
<<<<<<< HEAD

    [Range(1, 10)][SerializeField] public int ShootDamage;
    [Range(3, 1000)][SerializeField] public int ShootDistances;
    [Range(0.1f, 3)][SerializeField] public float ShootRate;

    public int ammoCurr;
    [Range(5, 50)] public int ammoMax;

=======
    [Range(1, 10)][SerializeField] public int ShootDamage;
    [Range(3, 1000)][SerializeField] public int ShootDistances;
    [Range(0.1f, 3)][SerializeField] public float ShootRate;
    [Range(0, 100)][SerializeField] public int ammoMax;
    [Range(1, 100)][SerializeField] public int magSize;


    public int ammoCurr;
>>>>>>> 6d2dff04f4039c2e79427fdd9b293c83cb55d88f
    public ParticleSystem hitEffect;
    public AudioClip[] shootsound;
    [Range(0, 1)] public float shootSoundVol;
}
