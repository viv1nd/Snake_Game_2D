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
    [SerializeField] [Range(0f,10f)] private float speed;
    public float GetSpeed()
    {
        return speed;
    }

    public Text scoreText;
    
    public float gametime = 0;
    public Text timerText;
    private int score = 0;

    [SerializeField] private float startSpeed = 1.1f;
    [SerializeField] private float powerupSpeed = 0.7f;


    private void Start()
    {
        scoreText.text = score.ToString();
        speed = startSpeed;


    }

    private void Update()
    {
        gametime = gametime + Time.deltaTime;
        timerText.text = Mathf.RoundToInt(gametime).ToString();
    }


    public void AddScore()
    {
        score++;
        scoreText.text = score.ToString();
    }

    public void ResetScore()
    {
        score = 0;
        scoreText.text = score.ToString();
    }

    public void RemoveScore()
    {
        if (score < 1)
        {
            return;
        }
        score--;
        scoreText.text = score.ToString();
    }

    public void speedup()
    {
        speed = startSpeed;
    }

    public void SpeedDown()
    {
        speed = powerupSpeed;
    }

    public Vector2 GenerateSpawnPoint(BoxCollider2D gridArea)
    {
        Bounds bounds = gridArea.bounds;
        Vector2 position;
        position.x = Random.Range(bounds.min.x, bounds.max.x);
        position.y = Random.Range(bounds.min.y, bounds.max.y);
        if (!ValidatePosition(position))
             position = GenerateSpawnPoint(gridArea);
        return position;
            
    }

    private bool ValidatePosition(Vector2 aPosition)
    {
        Debug.Log("validating points"); 
        foreach (Eatable mapObject in onMapObjects)
        {

            Vector2 bPosition = new Vector2(mapObject.transform.position.x, mapObject.transform.position.y);
            Debug.Log("validating point a:" + aPosition + " and pointb: " + bPosition);
            float distance = CheckDistance(aPosition, bPosition);
            Debug.Log(distance);
            if(distance < 6.5f)
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
