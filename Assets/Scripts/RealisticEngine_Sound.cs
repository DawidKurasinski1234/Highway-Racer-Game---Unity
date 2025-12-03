using UnityEngine;

public class RealisticEngine : MonoBehaviour
{
    public AudioSource silnikAudio;

    [Header("Ustawienia Skrzyni Biegów")]
    public int iloscBiegow = 5;
    public int aktualnyBieg = 1;

    [Header("Obroty (Pitch)")]
    public float minPitch = 0.8f;      // DŸwiêk na postoju (Idle) - to jest Twój "inny dŸwiêk"!
    public float maxPitch = 2.5f;      // Moment zmiany biegu (Odciêcie)
    public float redukcjaPrzyZmianie = 0.7f; // O ile spada dŸwiêk przy wrzuceniu wy¿szego biegu

    [Header("Reakcja")]
    public float szybkoscWkrecania = 0.5f; // Jak szybko rosn¹ obroty
    public float szybkoscSpadania = 1.0f;  // Jak szybko spadaj¹ po puszczeniu gazu

    // Zmienna prywatna do œledzenia aktualnego tonu
    private float currentPitch;

    void Start()
    {
        if (silnikAudio == null)
            silnikAudio = GetComponent<AudioSource>();

        // Startujemy od "mruczenia"
        currentPitch = minPitch;
    }

    void Update()
    {
        if (GameManager.isGameOver == false)
        {
            // 1. CZY DODAJE GAZU? (Wciœniête W)
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                // Podnosimy obroty
                currentPitch += Time.deltaTime * szybkoscWkrecania;

                // LOGIKA ZMIANY BIEGU
                // Jeœli dŸwiêk jest za wysoki I mamy jeszcze biegi w zapasie...
                if (currentPitch >= maxPitch && aktualnyBieg < iloscBiegow)
                {
                    aktualnyBieg++; // Wbijamy wy¿szy bieg
                    currentPitch -= redukcjaPrzyZmianie; // Obroty spadaj¹ (efekt zmiany biegu)
                }
            }
            // 2. CZY PUŒCI£EM GAZ? (Hamowanie silnikiem)
            else
            {
                // Obroty spadaj¹
                currentPitch -= Time.deltaTime * szybkoscSpadania;

                // Redukcja biegów (Opcjonalne, ale fajne)
                // Jeœli obroty spad³y za nisko, redukujemy bieg, ¿eby silnik nie zgas³
                if (currentPitch < minPitch && aktualnyBieg > 1)
                {
                    aktualnyBieg--;
                    currentPitch += redukcjaPrzyZmianie / 2; // Lekki skok obrotów przy redukcji
                }
            }

            // 3. OGRANICZNIKI (Clamp)
            // Upewniamy siê, ¿e dŸwiêk nigdy nie zejdzie poni¿ej mruczenia (0.8)
            // i nie wejdzie powy¿ej wycia (3.0)
            currentPitch = Mathf.Clamp(currentPitch, minPitch, 3.0f);

            // 4. APLIKUJEMY DO G£OŒNIKA
            silnikAudio.pitch = currentPitch;
        }
        else
        {
            // Cisza po Game Over
            silnikAudio.volume = Mathf.Lerp(silnikAudio.volume, 0f, Time.deltaTime);
        }
    }
}