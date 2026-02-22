using UnityEngine;
using UnityEngine.UI;

public class StartPoga : MonoBehaviour
{
    public Button startButton;

    void Start()
    {
        startButton.onClick.AddListener(() =>
        {
            BubbleTransition.Instance.GoToScene("Game");
        });
    }
}