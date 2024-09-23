using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    enum GameState
    {
        Start,
        Idle,
        Playing
    }

    private static Color transparent = new Color(1f, 1f, 1f, 0.2f);

    GameState gameState;

    [SerializeField] BallBehaviour ball;
    [SerializeField] PlayerBehaviour player1;
    [SerializeField] PlayerBehaviour player2;

    [SerializeField] TMP_InputField inputPlayer1;
    [SerializeField] TMP_InputField inputPlayer2;
    [SerializeField] GameObject textPressToStart;

    [SerializeField] GameObject panelStart;


    // Start is called before the first frame update
    void Start()
    {
        gameState = GameState.Start;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState == GameState.Start)
        {
            if (AreValidInputs())
            {

                float ratio = Mathf.Abs(Mathf.Sin(Time.time * 2f));

                textPressToStart.GetComponent<TextMeshProUGUI>().color = Color.Lerp(Color.white, transparent, ratio);
            }
            else
            {
                textPressToStart.GetComponent<TextMeshProUGUI>().color = transparent;
            }

            if (Input.GetKey(KeyCode.Return))
            {
                if (AreValidInputs())
                {
                    panelStart.SetActive(false);
                    gameState = GameState.Idle;
                    player1.SetPlayerName(inputPlayer1.text);
                    player2.SetPlayerName(inputPlayer2.text);
                }
            }
        }


        if (gameState == GameState.Idle)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                if (ball.TryShoot())
                {
                    gameState = GameState.Playing;
                }
            }

            if (Input.GetKey(KeyCode.N))
            {
                NewGame();
            }
        }

        if (gameState == GameState.Playing)
        {
            if (Input.GetKey(KeyCode.W))
            {
                player1.Move(1);
            }
            if (Input.GetKey(KeyCode.S))
            {
                player1.Move(-1);
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                player2.Move(1);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                player2.Move(-1);
            }
        }

    }

    bool AreValidInputs()
    {
        return inputPlayer1.text.Length >= 2
        && inputPlayer1.text.Length < 12
        && inputPlayer2.text.Length >= 2
        && inputPlayer2.text.Length < 12;
    }

    public void OnGoal()
    {
        gameState = GameState.Idle;
    }

    public void NewGame()
    {
        gameState = GameState.Start;
        panelStart.SetActive(true);

        inputPlayer1.text = "";
        inputPlayer2.text = "";

        player1.ResetScore();
        player2.ResetScore();
    }

    public bool IsPlaying() => gameState == GameState.Playing;
}
