using UnityEngine;
using UnityEngine.Rendering;

public class destructibleWalls : MonoBehaviour, IDamage
{
    [Header("Wall Properties")]
    [SerializeField] int HP;
    [SerializeField] GameObject intactMesh;

    [Header("Cube Settings")]
    [SerializeField] GameObject cubeFragmentPrefab;
    [SerializeField] int cubesAcross;
    [SerializeField] float spreadForce;
    [SerializeField] float cubeLifetime;
    [SerializeField] GameObject cube;

    bool destroyed = false;

    public void TakeDamage(int amount)
    {
        if (destroyed) return;

        HP -= amount;

        if (HP <= 0)
        {
            destroyed = true;
            BreakIntoCubes();
        }
    }

    void BreakIntoCubes()
    {
        Renderer rend = intactMesh.GetComponent<Renderer>();

        Bounds bounds = rend.bounds;

        intactMesh.SetActive(false);

        Vector3 size = bounds.size;
        Vector3 start = bounds.min;

        for (int x = 0;
            x < cubesAcross;
            x++)
        {
            for (int y = 0;
                y < cubesAcross;
                y++)
            {
                for (int z = 0;
                    z < cubesAcross;
                    z++)
                {
                    Vector3 pos = new Vector3(
                        start.x + size.x * (x + 0.5f) / cubesAcross,
                        start.y + size.y * (y + 0.5f) / cubesAcross,
                        start.z + size.z * (z + 0.5f) / cubesAcross);

                    GameObject cubeInstance = Instantiate(cubeFragmentPrefab, pos, Random.rotation);
                    Rigidbody rb = cube.GetComponent<Rigidbody>();
                    rb.isKinematic = false;

                    Debug.Log("Spawning Cube at: " + pos);

                    Vector3 forceDir = (cube.transform.position - bounds.center).normalized;
                    rb.AddForce(forceDir * spreadForce, ForceMode.Impulse);

                    Destroy(cubeInstance, cubeLifetime);
                }
            }
        }
        Destroy(gameObject, 0.05f);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
