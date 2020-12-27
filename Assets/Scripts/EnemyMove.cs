using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyMove : MonoBehaviour
{    
    Vector2 secondpoint;
    Vector2[] randomVector = { Vector2.up,Vector2.down,Vector2.right,Vector2.left};
    int[] randomstep = { 1, 2, 3, 4 };

    Vector2 randomdir;
    int randstep;
    float[] randomspeed = { 0.1f, 0.2f, 0.3f };
    float speed;
        
    void Start()
    {        
        secondpoint = transform.position;
        randomdir = randomVector[Random.Range(0, randomVector.Length)];
        randstep = randomstep[Random.Range(0, randomstep.Length)];
        speed = randomspeed[Random.Range(0, randomspeed.Length)];
    }

    void FixedUpdate()
    {
        Vector2 p = Vector2.MoveTowards(transform.position, secondpoint, speed);
        GetComponent<Rigidbody2D>().MovePosition(p);
        if((Vector2)transform.position==secondpoint)
        {            
            if (valid(randomdir) && randstep>=0)
            {
                secondpoint = (Vector2)transform.position + 2.0f * randomdir;
                randstep = randstep - 1;
            }
            else 
            {
                randomdir = randomVector[Random.Range(0, randomVector.Length)];
                randstep = randomstep[Random.Range(0, randomstep.Length)];
            }
        }
        Vector2 dir = secondpoint - (Vector2)transform.position;
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
        if (other.gameObject.tag == "dot")
        {            
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Player")
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
