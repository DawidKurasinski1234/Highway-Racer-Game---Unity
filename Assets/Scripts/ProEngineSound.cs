using UnityEngine;

public class ProEngineSound : MonoBehaviour
{
    [Header("Twoje Pliki DüwiÍkowe")]
    public AudioClip clipLow;   // Niskie obroty (Idle)
    public AudioClip clipMid;   // årednie obroty
    public AudioClip clipHigh;  // Wysokie obroty (Limiter)

    [Header("Skrzynia BiegÛw")]
    public int iloscBiegow = 5;
    public float szybkoscWkrecania = 0.5f;
    public float szybkoscSpadania = 1.0f;
    public float redukcjaPrzyZmianie = 0.7f;

    // Prywatne ürÛd≥a düwiÍku (stworzymy je kodem)
    private AudioSource sourceLow;
    private AudioSource sourceMid;
    private AudioSource sourceHigh;

    private float currentRPM = 0f; // WartoúÊ od 0.0 do 3.0 (reprezentuje obroty)
    private int aktualnyBieg = 1;

    void Start()
    {
        // Tworzymy 3 wirtualne g≥oúniki na samochodzie
        sourceLow = UtworzZrodlo(clipLow);
        sourceMid = UtworzZrodlo(clipMid);
        sourceHigh = UtworzZrodlo(clipHigh);
    }

    // Pomocnicza funkcja do tworzenia g≥oúnikÛw
    AudioSource UtworzZrodlo(AudioClip clip)
    {
        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.clip = clip;
        source.loop = true;
        source.playOnAwake = true;
        source.volume = 0; // Na start cisza
        source.spatialBlend = 0.7f; // TrochÍ 3D, trochÍ 2D
        source.Play();
        return source;
    }

    void Update()
    {
        if (GameManager.isGameOver == false)
        {
            // --- 1. LOGIKA OBROT”W (Ta sama co wczeúniej) ---
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                currentRPM += Time.deltaTime * szybkoscWkrecania;

                // Zmiana biegu
                if (currentRPM >= 2.0f && aktualnyBieg < iloscBiegow) // 2.5 to prÛg zmiany
                {
                    aktualnyBieg++;
                    currentRPM -= redukcjaPrzyZmianie;
                }
            }
            else
            {
                currentRPM -= Time.deltaTime * szybkoscSpadania;

                // Redukcja
                if (currentRPM < 0.5f && aktualnyBieg > 1)
                {
                    aktualnyBieg--;
                    currentRPM += 0.5f;
                }
            }

            // Ograniczamy RPM (od 0 do 3)
            currentRPM = Mathf.Clamp(currentRPM, 0f, 3f);


            // --- 2. MIESZANIE DèWI K”W (Blending) ---

            // Pitch (WysokoúÊ) - nadal lekko zmieniamy pitch, øeby düwiÍk by≥ øywy
            // Ale w mniejszym zakresie (np. 0.8 do 1.2), bo g≥Ûwnπ robotÍ robiπ rÛøne klipy
            float pitchFactor = 0.5f + (currentRPM * 0.3f);
            sourceLow.pitch = pitchFactor;
            sourceMid.pitch = pitchFactor;
            sourceHigh.pitch = pitchFactor;

            // Volume (G≥oúnoúÊ) - to jest serce tego skryptu!

            // LOW: Gra g≥oúno przy 0 RPM, cichnie przy 1 RPM
            sourceLow.volume = 1f - Mathf.Clamp01(currentRPM);

            // MID: Cichy przy 0, g≥oúny przy 1.5, cichy przy 3
            if (currentRPM < 1.5f)
                sourceMid.volume = currentRPM; // Roúnie
            else
                sourceMid.volume = 1f - (currentRPM - 1.5f); // Maleje

            // HIGH: Cichy przy 1, g≥oúny przy 2.5+
            sourceHigh.volume = Mathf.Clamp01(currentRPM - 1.5f);
        }
        else
        {
            // Wyciszanie po úmierci
            sourceLow.volume = 0;
            sourceMid.volume = 0;
            sourceHigh.volume = 0;
        }
    }
}