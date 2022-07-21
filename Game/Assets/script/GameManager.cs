using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    #endregion

    public List<Eatable> onMapObjects;

    public Transform poison;
    public Transform food;
    //public Transform powerup;
    public Text text;
    private int score = 0;

    private void Start()
    {
        text.text = score.ToString();
    }

    private void Update()
    {
        
    }

    public void AddScore()
    {
        score++;
        text.text = score.ToString();
    }

    public void RemoveScore()
    {
        if (score < 1)
        {
            return;
        }
        score--;
        text.text = score.ToString();
    }

    public Vector2 GenerateSpawnPoint(BoxCollider2D gridArea)
    {
        Bounds bounds = gridArea.bounds;
        Vector2 position;
        position.x = Random.Range(bounds.min.x, bounds.max.x);
        position.y = Random.Range(bounds.min.y, bounds.max.y);
        if (!ValidatePosition(position))
            GenerateSpawnPoint(gridArea);
        return position;
            
    }

    private bool ValidatePosition(Vector2 aPosition)
    {
        foreach(Eatable mapObject in onMapObjects)
        {
            Vector2 bPosition = new Vector2(mapObject.transform.position.x, mapObject.transform.position.y);
            float distance = CheckDistance(aPosition, bPosition);
            if(distance < 2.5f)
            {
                return false;
            }
        }
        return true;
    }

    private float CheckDistance(Vector2 a, Vector2 b)
    {
        return Vector2.Distance(a, b);
    }

    public void GenerateFood()
    {

    }

}
