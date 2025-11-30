using UnityEngine;

public class Movement : MonoBehaviour
{
    public float predkoscBoczna = 10.0f;
    public float plynnoscSkretu = 5.0f;
    public float katPochylenia = 10.0f;

    public Transform model3D;
    public Transform rightWheel;
    public Transform leftWheel;
    public float katSkretu = 30f; 

    void Update()
    {
        if (GameManager.isGameOver == false)
        {
            float ruchX = Input.GetAxis("Horizontal");

            // RUCH (To zostaje na g³ównym obiekcie - Rodzicu)
            Vector3 ruch = new Vector3(ruchX, 0, 0);
            transform.Translate(ruch * predkoscBoczna * Time.deltaTime);

            // OGRANICZENIE (Clamp) - te¿ na Rodzicu
            Vector3 pozycja = transform.position;
            pozycja.x = Mathf.Clamp(pozycja.x, -9f, 9f);
            transform.position = pozycja;

            // ROTACJA (Dzieje siê TYLKO na modelu 3D!)
            if (model3D != null)
            {
                // Obliczamy przechy³
                Quaternion docelowaRotacja = Quaternion.Euler(0, ruchX * 10f, -ruchX * katPochylenia);

                // U¿ywamy 'localRotation' zamiast 'rotation'
                model3D.localRotation = Quaternion.Lerp(model3D.localRotation, docelowaRotacja, Time.deltaTime * plynnoscSkretu);
            }
            if (rightWheel != null && leftWheel != null)
            {
                // Obliczamy k¹t skrêtu (Input * Maksymalny K¹t)
                float aktualnyKat = ruchX * katSkretu;

                // Ustawiamy obrót.
                // Oœ Y (druga liczba) odpowiada za skrêcanie lewo/prawo.
                // Quaternion.Euler(X, Y, Z)
                Quaternion rotacjaKol = Quaternion.Euler(0, aktualnyKat, 0);

                // Przypisujemy do kó³ (u¿ywamy localRotation, ¿eby skrêca³y wzglêdem auta)
                leftWheel.localRotation = rotacjaKol;
                rightWheel.localRotation = rotacjaKol;
            }
        }
    }
}