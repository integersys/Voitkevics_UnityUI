using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Universāls fona mūzikas atskaņotājs.
/// Pievieno šo skriptu tukšam GameObject un norēdī to kā "DontDestroyOnLoad".
/// </summary>
public class BackgroundMusicManager : MonoBehaviour
{
    public static BackgroundMusicManager Instance { get; private set; }

    // ──────────────────────────────────────────────
    // Inspector konfigurācija
    // ──────────────────────────────────────────────

    [Header("Audio Source")]
    [SerializeField] private AudioSource audioSource;

    [Header("Transition")]
    [Tooltip("Laiks sekundēs, cik ilgi notiek fade starp dziesmām.")]
    [SerializeField] private float fadeDuration = 1.5f;

    [Header("Scene Music Configuration")]
    [SerializeField] private List<SceneMusicConfig> sceneMusicConfigs = new();

    // ──────────────────────────────────────────────
    // Iekšējais stāvoklis
    // ──────────────────────────────────────────────

    private SceneMusicConfig _currentConfig;
    private List<AudioClip> _currentPlaylist = new();
    private int _currentIndex = -1;
    private Coroutine _fadeCoroutine;
    private Coroutine _playCoroutine;
    private string _lastScene = "";

    // ──────────────────────────────────────────────
    // Unity lifecycle
    // ──────────────────────────────────────────────

    private void Awake()
    {
        // Singleton – viens eksemplārs starp visām scenām
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        // Startējot ielādē pašreizējo scenu
        LoadMusicForScene(SceneManager.GetActiveScene().name);
    }

    // ──────────────────────────────────────────────
    // Scene notikums
    // ──────────────────────────────────────────────

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == _lastScene) return; // nav mainījusies scena
        LoadMusicForScene(scene.name);
    }

    // ──────────────────────────────────────────────
    // Mūzikas ielāde pēc scenas
    // ──────────────────────────────────────────────

    private void LoadMusicForScene(string sceneName)
    {
        _lastScene = sceneName;

        SceneMusicConfig config = sceneMusicConfigs.Find(c => c.sceneName == sceneName);

        if (config == null)
        {
            Debug.Log($"[MusicManager] Nav konfigurācijas scenai '{sceneName}'. Mūzika apstājas.");
            StopMusic();
            return;
        }

        // Ja tā pati konfigurācija un mūzika jau spēlē – netraucēt
        if (config == _currentConfig && audioSource.isPlaying) return;

        _currentConfig = config;

        // Sagatavo atskaņošanas sarakstu
        _currentPlaylist = new List<AudioClip>(config.clips);
        _currentIndex = -1;

        if (_currentPlaylist.Count == 0)
        {
            Debug.LogWarning($"[MusicManager] Scenai '{sceneName}' nav pievienotu dziesmu!");
            return;
        }

        if (config.shuffleMode)
            ShufflePlaylist();

        StartPlayback();
    }

    // ──────────────────────────────────────────────
    // Atskaņošana
    // ──────────────────────────────────────────────

    private void StartPlayback()
    {
        if (_playCoroutine != null) StopCoroutine(_playCoroutine);
        _playCoroutine = StartCoroutine(PlaylistCoroutine());
    }

    private IEnumerator PlaylistCoroutine()
    {
        while (true)
        {
            _currentIndex++;

            // Ja iziet cauri visām dziesmām
            if (_currentIndex >= _currentPlaylist.Count)
            {
                _currentIndex = 0;

                // Shuffle режīmā – pārjauc vēlreiz (bet neļauj sākt ar to pašu)
                if (_currentConfig.shuffleMode)
                    ShufflePlaylist(_currentPlaylist.Count > 1 ? _currentPlaylist[^1] : null);
            }

            AudioClip nextClip = _currentPlaylist[_currentIndex];

            yield return StartCoroutine(FadeTo(nextClip));

            // Gaida līdz dziesma beidzas
            yield return new WaitUntil(() =>
                !audioSource.isPlaying ||
                audioSource.clip != nextClip);
        }
    }

    // ──────────────────────────────────────────────
    // Fade efekts
    // ──────────────────────────────────────────────

    private IEnumerator FadeTo(AudioClip newClip)
    {
        float startVolume = audioSource.volume;

        // Fade OUT
        if (audioSource.isPlaying)
        {
            float t = 0f;
            while (t < fadeDuration)
            {
                t += Time.deltaTime;
                audioSource.volume = Mathf.Lerp(startVolume, 0f, t / fadeDuration);
                yield return null;
            }
        }

        // Nomaina dziesmu
        audioSource.Stop();
        audioSource.clip = newClip;
        audioSource.volume = 0f;
        audioSource.Play();

        Debug.Log($"[MusicManager] Atskaņo: {newClip.name}");

        // Fade IN
        float t2 = 0f;
        while (t2 < fadeDuration)
        {
            t2 += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0f, _currentConfig.volume, t2 / fadeDuration);
            yield return null;
        }

        audioSource.volume = _currentConfig.volume;
    }

    // ──────────────────────────────────────────────
    // Publiskās metodes (vari izsaukt no jebkura skripta)
    // ──────────────────────────────────────────────

    /// <summary>Aptur mūziku ar fade efektu.</summary>
    public void StopMusic()
    {
        if (_playCoroutine != null)
        {
            StopCoroutine(_playCoroutine);
            _playCoroutine = null;
        }
        if (_fadeCoroutine != null)
        {
            StopCoroutine(_fadeCoroutine);
            _fadeCoroutine = null;
        }
        StartCoroutine(FadeOut());
    }

    /// <summary>Lec uz nākamo dziesmu manuāli.</summary>
    public void NextSong()
    {
        if (_playCoroutine != null) StopCoroutine(_playCoroutine);
        _playCoroutine = StartCoroutine(SkipCoroutine());
    }

    /// <summary>Pagaidu klusēšana / atsāk.</summary>
    public void TogglePause()
    {
        if (audioSource.isPlaying) audioSource.Pause();
        else audioSource.UnPause();
    }

    /// <summary>Iestata skaļumu (0–1).</summary>
    public void SetVolume(float volume)
    {
        audioSource.volume = Mathf.Clamp01(volume);
        if (_currentConfig != null) _currentConfig.volume = audioSource.volume;
    }

    // ──────────────────────────────────────────────
    // Palīgmetodes
    // ──────────────────────────────────────────────

    private IEnumerator SkipCoroutine()
    {
        _currentIndex++; // Nākamā dziesma – PlaylistCoroutine sāks no kur pārtraukts
        if (_currentIndex >= _currentPlaylist.Count)
            _currentIndex = -1; // Atiestatīs uz 0 ciklā

        AudioClip next = _currentPlaylist[
            Mathf.Clamp(_currentIndex, 0, _currentPlaylist.Count - 1)];

        yield return StartCoroutine(FadeTo(next));
        _playCoroutine = StartCoroutine(PlaylistCoroutine());
    }

    private IEnumerator FadeOut()
    {
        float startVolume = audioSource.volume;
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, t / fadeDuration);
            yield return null;
        }
        audioSource.Stop();
        audioSource.volume = 0f;
    }

    /// <summary>
    /// Fisher-Yates shuffle. Ja tiek padots lastClip,
    /// nodrošina, ka tas nebūs pirmais pēc jaukšanas.
    /// </summary>
    private void ShufflePlaylist(AudioClip lastClip = null)
    {
        for (int i = _currentPlaylist.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (_currentPlaylist[i], _currentPlaylist[j]) = (_currentPlaylist[j], _currentPlaylist[i]);
        }

        // Ja pirmā dziesma ir tā pati, kas pēdējā – noliek to uz beigām
        if (lastClip != null && _currentPlaylist.Count > 1 && _currentPlaylist[0] == lastClip)
        {
            _currentPlaylist.RemoveAt(0);
            _currentPlaylist.Add(lastClip);
        }
    }
}

// ──────────────────────────────────────────────────────────
// Datu struktūra – konfigurācija vienai scenai
// ──────────────────────────────────────────────────────────

[System.Serializable]
public class SceneMusicConfig
{
    [Tooltip("Jābūt tieši tādam pašam kā scenas nosaukums Build Settings.")]
    public string sceneName = "";

    [Tooltip("AudioClip saraksts šai scenai.")]
    public List<AudioClip> clips = new();

    [Tooltip("Ja ieslēgts – dziesmas tiek jauktas. Ja izslēgts – spēlē secīgi.")]
    public bool shuffleMode = false;

    [Range(0f, 1f)]
    public float volume = 1f;
}