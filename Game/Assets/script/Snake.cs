using System.Collections.Generic;
using UnityEngine;
using System.Collections;


public class Snake : MonoBehaviour
{
    private Vector2 _direction = Vector2.down;

    public List<Transform> _snakeparts = new List<Transform>();         

    public Transform snakePrefab;

    [SerializeField] private int _intialsnakesize = 3;

    [SerializeField] private Transform tailParent = null;

    private void Start()
    {
        generateSnake();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && _direction.x != 0)
        {
            Vector2 vec;
            vec.x = 0;
            vec.y = 1;
            _direction = vec;
        }
        else if ( Input.GetKeyDown(KeyCode.S) && _direction.x != 0)
        {
            Vector2 vec;
            vec.x = 0;
            vec.y = -1;
            _direction = vec;
        }
        else if (Input.GetKeyDown(KeyCode.A) && _direction.y != 0)
        {
            Vector2 vec;
            vec.x = -1;
            vec.y = 0;
            _direction = vec;
        }
        else if (Input.GetKeyDown(KeyCode.D) && _direction.y != 0)
        {
            Vector2 vec;
            vec.x = 1;
            vec.y = 0;
            _direction = vec;
            
        }
    }

    private void FixedUpdate()
    {
        if(transform.position.x == -30 || transform.position.x == 30)
        {
            Vector2 vec;
            float x = transform.position.x;
            x = x/30 * 29 * -1;
            float y = transform.position.y;
            vec.x = x;
            vec.y = y;
            transform.position = vec;
        }
        if (transform.position.y == -15 || transform.position.y == 15)
        {
            Vector2 vec;
            float y = transform.position.y;
            float x = transform.position.x;
            y = y / 15 * 14 * -1;
            vec.y = y;
            vec.x = x;
            transform.position = vec;
        }


        for (int i = _snakeparts.Count -1; i > 0; i--)
        {
            if(_snakeparts[i]!= null)
                _snakeparts[i].position = _snakeparts[i - 1].position;
        }
        if(_snakeparts[0])
        _snakeparts[0].position = transform.position;
       this.transform.position = new Vector3(
       Mathf.Round(this.transform.position.x) + _direction.x,
       Mathf.Round(this.transform.position.y) + _direction.y,
       0.0f
       );
    }

    private void Grow()
    {
        Transform tail = Instantiate(this.snakePrefab);
        tail.position = _snakeparts[_snakeparts.Count - 1].position;
        _snakeparts.Add(tail);
    }


    public void Poison()
    {
        if(_snakeparts.Count == 1)
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
