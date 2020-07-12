using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Ball : MonoBehaviour
{

    public float speed = 10;
    private AudioSource source;
    private Rigidbody2D rb2d;
    void startBall(){
        float rand = Random.Range(0,4);
        if(rand<1){
            rb2d.AddForce(new Vector2(40,-15));
        }else if(rand<2){
            rb2d.AddForce(new Vector2(-40,-15));
        }else if(rand<3){
            rb2d.AddForce(new Vector2(-40,15));
        }else{
            rb2d.AddForce(new Vector2(40,15));
        }
    }

    void resetBall(){
        rb2d.velocity = Vector2.zero;
        transform.position = Vector2.zero;
    }

    void restartGame(){
        resetBall();
        Invoke("startBall", 2);
    }

    void Start(){
        rb2d=GetComponent<Rigidbody2D>();
        source = GetComponent<AudioSource>();
        Invoke("startBall",2);
    }

    float hitFactor(Vector2 ballPos, Vector2 racketPos, float racketHeight){
        return (ballPos.y - racketPos.y) / racketHeight;
    }

    void OnCollisionEnter2D(Collision2D col){
        if (col.gameObject.name=="RacketLeft"){
            float y = hitFactor(transform.position,col.transform.position,col.collider.bounds.size.y);
            Vector2 dir = new Vector2(1,y).normalized;
            GetComponent<Rigidbody2D>().velocity = dir * speed;
            source.Play();
        }
        if (col.gameObject.name == "RacketRight") {
            float y = hitFactor(transform.position, col.transform.position, col.collider.bounds.size.y);
            Vector2 dir = new Vector2(-1, y).normalized;
            GetComponent<Rigidbody2D>().velocity = dir * speed;
            source.Play();
        }
    }
}
