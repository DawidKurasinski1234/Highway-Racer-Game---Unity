using UnityEngine;

public class DzwiekSilnika : MonoBehaviour
{
    public AudioSource silnikAudio;

    // Ustawienia brzmienia
    public float minPitch = 0.8f;   // DŸwiêk, gdy jedziesz spokojnie (puszczone W)
    public float maxPitch = 2.2f;   // DŸwiêk na pe³nym gazie (wciœniête W)
    public float szybkoscWkrecania = 2.0f; // Jak szybko silnik reaguje (im wiêcej, tym szybciej)

    void Start()
    {
        if (silnikAudio == null)
            silnikAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (GameManager.isGameOver == false)
        {
            // 1. Ustalamy cel: Niskie czy Wysokie obroty?
            float celPitch = minPitch;

            // Jeœli trzymasz W lub Strza³kê w Górê
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                celPitch = maxPitch;
            }

            // 2. P³ynne przejœcie (Lerp)
            // Zmieniamy obecny pitch w stronê celu z okreœlon¹ prêdkoœci¹
            silnikAudio.pitch = Mathf.Lerp(silnikAudio.pitch, celPitch, Time.deltaTime * szybkoscWkrecania);
        }
        else
        {
            // Cisza po œmierci
            silnikAudio.volume = Mathf.Lerp(silnikAudio.volume, 0f, Time.deltaTime);
            silnikAudio.pitch = Mathf.Lerp(silnikAudio.pitch, 0.5f, Time.deltaTime);
        }
    }
}