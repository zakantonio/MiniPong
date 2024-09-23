using System;
using TMPro;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour, Resettable
{

    [SerializeField] GameObject bounceOrigin;

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI playerText;

    int score = 0;
    int speed = 5;

    Vector3 position;

    void Start()
    {
        position = gameObject.transform.position;
    }

    public void SetPlayerName(String name)
    {
        playerText.text = name;
    }

    public void IncreaseScore()
    {
        scoreText.text = (++score).ToString();
    }
    public void Move(float direction)
    {
        transform.Translate(0f, 0f, direction * Time.deltaTime * speed);

        transform.position = new Vector3(
                transform.position.x,
                transform.position.y,
                Mathf.Clamp(transform.position.z, -4, 4)
            );
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out BallBehaviour ball))
        {
            ball.BouncePlayerAdavanced(bounceOrigin.transform.position);
        }
    }
    public void Reset()
    {
        gameObject.transform.position = position;
    }

    public void ResetScore()
    {
        score = 0;
        scoreText.text = score.ToString();
    }
}
