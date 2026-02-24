using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

[RequireComponent(typeof(AudioSource))]
public class SkanasEfekti : MonoBehaviour, IPointerClickHandler
{
    [Header("Poga skaņa")]
    public AudioClip skana;

    [Header("Dropdown skaņas")]
    public TMP_Dropdown dropdown;
    public AudioClip[] dropdownSkanas;

    [Range(0f, 1f)]
    public float skalums = 1f;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;

        if (dropdown != null)
            dropdown.onValueChanged.AddListener(OnDropdownChanged);
    }

    void OnDropdownChanged(int index)
    {
        if (dropdownSkanas != null && index < dropdownSkanas.Length)
            if (dropdownSkanas[index] != null)
                audioSource.PlayOneShot(dropdownSkanas[index], skalums);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (skana != null)
            audioSource.PlayOneShot(skana, skalums);
    }
}