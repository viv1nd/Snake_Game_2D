using UnityEngine;

public class Eatable : MonoBehaviour
{
    public BoxCollider2D gridArea;

    private void Start()
    {
        PositionPlayer();
    }

    public void PositionPlayer()
    {

        Vector2 pos = GameManager.Instance.GenerateSpawnPoint(gridArea);
        transform.position = new Vector3(
            Mathf.Round(pos.x), Mathf.Round(pos.y), 0.0f);
    }

    public virtual void OnDigest() 
    {
        PositionPlayer();
    }
}

    
