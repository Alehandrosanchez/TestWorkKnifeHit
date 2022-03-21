using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] Transform Throw_Position;
    [SerializeField] float ThrowForce;
    [SerializeField] GameObject Current_Knife;

    [SerializeField] float CreateKnifeTime;

    Rigidbody2D CKRB;

    [SerializeField] public Text ApplesTXT;
    [SerializeField] public Text ScoreTXT;
    [SerializeField] public Text BestScoreTXT;
    [SerializeField] Text KnifesTXT;
    void Awake()
    {
        
        StartCoroutine(Create_Knife());
    }
    private void Start()
    {
        Global.BestScore = PlayerPrefs.GetInt("Best");
        BestScoreTXT.text = "BestScore: "+ Global.BestScore;

        Global.Apples = PlayerPrefs.GetInt("Apples");
        ApplesTXT.text = "Apple: " + Global.Apples;

        ScoreTXT.text = "Score: ";

        KnifesTXT.text = "Knifes: " + Global.KnifeCountPerLevel+"/"+ Global.DefaultKnife;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ThrowKnife();
        }
    }
    public void ThrowKnife()
    {
        if (CKRB.velocity==new Vector2(0,0))
        {
            CKRB.AddForce(transform.up * ThrowForce, ForceMode2D.Impulse);
            Current_Knife = null;
            KnifesTXT.text = "Knifes: " + (Global.KnifeCountPerLevel-1)+"/"+ Global.DefaultKnife;
        }
       
    }
    
    public IEnumerator Create_Knife()
    {
        yield return new WaitForSeconds(0f);
        GameObject Knife = ObjectPool.instance.GetPooledObject();
        Current_Knife = Knife;
        if (Knife != null)
        {
            Knife.transform.position = Throw_Position.position;
            Knife.SetActive(true);
        }
        CKRB = Current_Knife.gameObject.GetComponent<Rigidbody2D>();
    }
}
