using UnityEngine;
using TMPro;

public class MainMenuRainbow : MonoBehaviour
{
    public float colorSpeed = 0.04f;  // very slow hue cycle
    public float bounceAmount = 0.05f;
    public float bounceSpeed = 3f;

    private TextMeshProUGUI text;
    private Vector3 originalScale;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        originalScale = transform.localScale;
    }

    void Update()
    {
        float hue = Mathf.Repeat(Time.time * colorSpeed, 1f);
        text.color = Color.HSVToRGB(hue, 1f, 1f);

        float scaleOffset = 1 + Mathf.Sin(Time.time * bounceSpeed) * bounceAmount;
        transform.localScale = originalScale * scaleOffset;
    }
}