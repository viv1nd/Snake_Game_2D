using System.Collections.Generic;
using UnityEngine;
using System.Collections;


public class Snake : MonoBehaviour
{
    private Vector3 _direction = Vector3.down;

    public List<Transform> _snakeparts = new List<Transform>();

    
    public Transform snakePrefab;

    private int _intialsnakesize = 0;

    [SerializeField] private Transform tailParent = null;

    float currentTime = 0;
    bool spacePressed = false;

    private void Start()
    {
        generateSnake();
    }

    private void Update()
    {
        if (spacePressed)
        {
            if (GameManager.Instance.gametime - currentTime > 5f)
            {
                GameManager.Instance.SpeedDown();
                spacePressed = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.W) && _direction.x != 0)
        {
            Vector2 vec;
            vec.x = 0;
            vec.y = 1;
            _direction = vec;
            transform.rotation = Quaternion.Euler(0,0,0);
        }
        else if ( Input.GetKeyDown(KeyCode.S) && _direction.x != 0)
        {
            Vector2 vec;
            vec.x = 0;
            vec.y = -1;
            _direction = vec;
            transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        else if (Input.GetKeyDown(KeyCode.A) && _direction.y != 0)
        {
            Vector2 vec;
            vec.x = -1;
            vec.y = 0;
            _direction = vec;
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (Input.GetKeyDown(KeyCode.D) && _direction.y != 0)
        {
            Vector2 vec;
            vec.x = 1;
            vec.y = 0;
            _direction = vec;
            transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.Instance.speedup();
            currentTime = GameManager.Instance.gametime;
            spacePressed = true;
        }


        


    }

    private void FixedUpdate()
    {
        if(transform.position.x <= -30 || transform.position.x >= 30)
        {
            Vector2 vec;
            float x = transform.position.x;
            x = x/30 * 29 * -1;
            float y = transform.position.y;
            vec.x = x;
            vec.y = y;
            transform.position = vec;
        }
        if (transform.position.y <= -15 || transform.position.y >= 15)
        {
            Vector2 vec;
            float y = transform.position.y;
            float x = transform.position.x;
            y = y / 15 * 14 * -1;
            vec.y = y;
            vec.x = x;
            transform.position = vec;
        }

        if (_snakeparts.Count != 0)
            _snakeparts[0].position = transform.position - _direction * 1.1f;

        for (int i = _snakeparts.Count -1; i > 0; i--)
        {
            if(_snakeparts[i]!= null)
                _snakeparts[i].position = _snakeparts[i - 1].position;
        }

        this.transform.position = new Vector3(
            transform.position.x + _direction.x * GameManager.Instance.GetSpeed(),
            transform.position.y + _direction.y * GameManager.Instance.GetSpeed(),
            0.0f
        );
    }


    private void Grow()
    {
        Transform tail = Instantiate(this.snakePrefab);
        if(_snakeparts.Count != 0)
            tail.position = _snakeparts[_snakeparts.Count - 1].position;
        else
            tail.position = new Vector3(
            transform.position.x - _direction.x,
            transform.position.y - _direction.y,
            0.0f
        );
        _snakeparts.Add(tail);
    }


    public void Poison()
    {
        if(_snakeparts.Count < 1)
        {
            ResetState();
            return;
        }
        Transform tail = _snakeparts[_snakeparts.Count - 1];
        if ( tail != null)
        {
            _snakeparts.Remove(tail);
            Destroy(tail.gameObject);
        }
    }

    private void generateSnake()
    {

        for (int i = 0; i < _intialsnakesize; i++)
        {
            _snakeparts.Add(Instantiate(snakePrefab,tailParent));
        }
    }

    private void ResetState()
    {
        for(int i = 0; i < _snakeparts.Count; i++)
        {
            Destroy(_snakeparts[i].gameObject);
        }

        _snakeparts.Clear();
        //_snakeparts.Add(transform);

        generateSnake();
        transform.position = Vector3.zero;
        GameManager.Instance.ResetScore();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.GetComponent<Food>())
        {
            Grow();
            collision.GetComponent<Food>().OnDigest();
        }
        else if (collision.GetComponent<Poison>())
        {
            Poison();
            collision.GetComponent<Poison>().OnDigest();
        }
        if (collision.CompareTag("Tail"))
        {
            ResetState();
        }
        //else if (collision.tag == ("Wall"))
        {
            //ResetState();
        }
    }

    //Posion function
}
