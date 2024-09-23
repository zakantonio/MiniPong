using UnityEngine;

public class BallBehaviour : MonoBehaviour, Resettable
{
    [SerializeField] GameManager gameManager;
    public bool isMoving = false;
    float speed = 0f;
    Vector3 direction = new Vector3(0f, 0f, 0f);
    Vector3 position;

    // Start is called before the first frame update
    void Start()
    {
        position = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // traslo la pallina in una certa direzione, a una certa velocità al secondo
        gameObject.transform.Translate(speed * Time.deltaTime * direction.normalized);
    }

    public void IncreaseSpeed()
    {
        speed *= 1.1f;
    }

    public bool TryShoot()
    {
        if (isMoving) return false;

        speed = 5f;
        isMoving = true;
        RandomDirection();
        return true;
    }

    public void BouncePlayer()
    {
        direction.x *= -1;
        IncreaseSpeed();
    }

    public void BouncePlayerAdavanced(Vector3 bounceOrigin)
    {
        Vector3 newDir = transform.position - bounceOrigin;
        direction = newDir.normalized;
        speed *= 1.1f;
    }

    public void BounceWall()
    {
        direction.z *= -1;
    }

    public void Reset()
    {
        speed = 0;
        isMoving = false;
        direction = new Vector3(0, 0, 0);

        gameObject.transform.position = position;
    }

    void RandomDirection()
    {

        float x = Utils.CoinFlip() * 1;
        float y = 0f;
        float z = Utils.CoinFlip() * Random.Range(0.3f, 0.7f);

        direction = new Vector3(x, y, z);
    }
}
