using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerCollision : MonoBehaviour
{
    public Movement movement;
    public GameManager gameManager;
    public Score score;

    private void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "Obstacle")
        {
            movement.enabled = false;
            score.StopCount();
            gameManager.GameOver();
        }
    }

}
