using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform Player;
    public Vector3 offset;
    void Start()
    {
        offset = transform.position - Player.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
     transform.position = Player.position + offset;
    }
    
}
