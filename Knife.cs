using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    bool Rotate;
    [SerializeField] float rotateSpeedMin, rotateSpeedMax;
    float RotateSpeed, z;
    public bool Hited;
    [SerializeField] float KnifeToKnifeForce;
    Rigidbody2D TRB;
    PauseMenu PM;
    Player PlayerScript;
    
    void Start()
    {
        TRB = this.gameObject.GetComponent<Rigidbody2D>();
        PM = GameObject.FindGameObjectWithTag("Canvas").GetComponent<PauseMenu>();
        PlayerScript= GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Player>();
        Rotate = false;
        if (Random.Range(-2,2)<0)
        {
            RotateSpeed = Random.Range(-rotateSpeedMin, -rotateSpeedMax);
        }
        else
        {
            RotateSpeed = Random.Range(rotateSpeedMin, rotateSpeedMax);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (Rotate)
        {
            z += Time.deltaTime * RotateSpeed;
            transform.rotation = Quaternion.Euler(0, 0, z);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wood"&& !Hited)
        {
            Handheld.Vibrate();
            this.gameObject.tag = "KnifeStatic";
            Global.KnifeCountPerLevel -= 1;
            Debug.Log("Hit" + Global.KnifeCountPerLevel);
            this.gameObject.transform.parent = collision.gameObject.transform;
            TRB.velocity = new Vector2(0, 0);
            collision.gameObject.GetComponent<Animation>().Play("WoodDamage");
            Hited = true;
            if (Global.KnifeCountPerLevel == 0)
            {
                Handheld.Vibrate();
                StartCoroutine(PM.Game_Over(1f,collision.gameObject, true));
            }
            else
            {
                StartCoroutine(PlayerScript.Create_Knife());
            }
            Global.Score += 1;
            PlayerScript.ScoreTXT.text ="Scroe: "+ Global.Score;

        }
        if (collision.gameObject.tag == "KnifeStatic"&& !Hited)
        {
            Handheld.Vibrate();
            this.gameObject.GetComponent<Animation>().Play("Knife");
            ThrowOut();
            StartCoroutine(PM.Game_Over(0.1f,null, false));
        }
        if (collision.gameObject.tag == "Apple" && !Hited)
        {
            Global.Apples += 1;
            collision.gameObject.GetComponent<Knife>().Hited = true;
            PlayerScript.ApplesTXT.text = "Apple: " + Global.Apples;
            collision.gameObject.transform.parent = null;
            collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 3f;
        }
    }

    public void ThrowOut()
    {
        TRB.velocity = new Vector2(0, 0);
        TRB.AddForce(new Vector2(Random.Range(2, -2), -2) * KnifeToKnifeForce, ForceMode2D.Impulse);
        Rotate = true;
        Hited = true;
    }
}
