using UnityEngine;

public class WallBehaviour : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out BallBehaviour ball))
        {
            ball.BounceWall();
        }
    }
}
