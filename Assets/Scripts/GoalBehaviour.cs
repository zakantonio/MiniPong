using System.Linq;
using UnityEngine;

public class GoalBehaviour : MonoBehaviour
{

    [SerializeField] InputManager gameManager;

    [SerializeField] PlayerBehaviour player;

    Resettable[] resettables;
    void Start()
    {
        resettables = FindObjectsOfType<MonoBehaviour>().OfType<Resettable>().ToArray();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out BallBehaviour ball))
        {
            player.IncreaseScore();
            gameManager.OnGoal();
            foreach (Resettable r in resettables)
            {
                r.Reset();
            }
        }
    }
}
