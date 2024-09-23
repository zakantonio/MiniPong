using UnityEngine;

public class ExtraSpeedBehaviour : MonoBehaviour, BeingExtra
{

    [SerializeField] InputManager gameManager;
    [SerializeField] int hidingTime = 2;
    [SerializeField] int showingTime = 5;
    [SerializeField] int feedbackTime = 2;

    [SerializeField] GameObject feedbackText;

    float hidingTimer;
    float showingTimer;
    float feedbackTimer;

    // Start is called before the first frame update
    void Start()
    {
        Hide();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.IsPlaying())
        {
            if (gameObject.GetComponent<MeshRenderer>().enabled)
            {
                showingTimer -= Time.deltaTime;
                if (showingTimer <= 0)
                {
                    Hide();
                }
            }
            else
            {
                hidingTimer -= Time.deltaTime;
                if (hidingTimer <= 0)
                {
                    Spawn();
                }
            }

            if (feedbackText.activeSelf)
            {
                feedbackTimer -= Time.deltaTime;
                if (feedbackTimer <= 0)
                {
                    feedbackText.SetActive(false);
                }
            }
        }
        else
        {
            Hide();
            HideFeedback();
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out BallBehaviour ball))
        {
            ball.IncreaseSpeed();
            Hide();
            ShowFeedback();
        }
    }

    void ShowFeedback()
    {
        feedbackTimer = feedbackTime;
        feedbackText.SetActive(true);
    }

    void HideFeedback()
    {
        if (feedbackText.activeSelf == false) return;

        feedbackTimer = 0;
        feedbackText.SetActive(false);
    }

    public void Spawn()
    {

        float x = Utils.CoinFlip() * Random.Range(0, 5f); ;
        float y = 0f;
        float z = Utils.CoinFlip() * Random.Range(0, 5f);

        gameObject.transform.position = new Vector3(x, y, z);

        gameObject.GetComponent<MeshRenderer>().enabled = true;
        gameObject.GetComponent<Collider>().enabled = true;
        showingTimer = showingTime;

    }

    public void Hide()
    {

        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;
        hidingTimer = hidingTime;
    }
}
