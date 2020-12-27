using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PacmanMove : MonoBehaviour
{
    public float speed = 0.4f;
    Vector2 dest = Vector2.zero;

    public int score;
    public GameObject ScoreObject;
    Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        dest = transform.position;
        score = -2;
        scoreText = ScoreObject.GetComponent<Text>();
        scoreText.text = "Score: " + score.ToString();
    }

    // Update is called once per frame
    void FixedUpdate()
    {        
        Vector2 p = Vector2.MoveTowards(transform.position, dest, speed);
        GetComponent<Rigidbody2D>().MovePosition(p);
        
        if ((Vector2)transform.position == dest)
        {
            if (Input.GetKey(KeyCode.UpArrow) && valid(Vector2.up))
                dest = (Vector2)transform.position + 2.0f*Vector2.up;
            if (Input.GetKey(KeyCode.RightArrow) && valid(Vector2.right))
                dest = (Vector2)transform.position + 2.0f * Vector2.right;
            if (Input.GetKey(KeyCode.DownArrow) && valid(-Vector2.up))
                dest = (Vector2)transform.position - 2.0f *Vector2.up;
            if (Input.GetKey(KeyCode.LeftArrow) && valid(-Vector2.right))
                dest = (Vector2)transform.position - 2.0f * Vector2.right;
        }
        Vector2 dir = dest - (Vector2)transform.position;
        GetComponent<Animator>().SetFloat("DirX", dir.x);
        GetComponent<Animator>().SetFloat("DirY", dir.y);
    }

    bool valid(Vector2 dir)
    {        
        Vector2 pos = transform.position;
        RaycastHit2D hit = Physics2D.Linecast(pos + 1.5f * dir, pos);//при 2 застревает почему-то...
        return (hit.collider == GetComponent<Collider2D>());
    }


    public GameObject gameover;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "dot")
        {
            score = score + 1;
            scoreText.text = "Score: " + score.ToString();
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Enemy")
        {
            Time.timeScale = 0f;
            GameObject gameoverEnemy = Instantiate(gameover, transform);            
            StartCoroutine(Wait(10.0f));            
        }
    }

    private IEnumerator Wait(float seconds)
    {
        
        yield return new WaitForSecondsRealtime(5f); 
        Time.timeScale = 1f;
        SceneManager.LoadScene("menu"); 
    }
}