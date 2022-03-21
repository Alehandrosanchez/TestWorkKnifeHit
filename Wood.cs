using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Wood : MonoBehaviour
{
    [SerializeField]GameObject[] Wood_Tiles;
    [SerializeField] float Crash_Force;
    [SerializeField] float RandMin, RandMax;
    [SerializeField] ParticleSystem Particle_Chips;
    [SerializeField] ParticleSystem Particle_Dust;
    [SerializeField] Animation Anim;

    [SerializeField] float RandTimeMin, RandTimeMax;
    [SerializeField] float rotateSpeedMin, rotateSpeedMax;
    float RotateSpeed,z;

    [SerializeField] GameObject[] RandPoint;
    [SerializeField] GameObject Apple,KnifeOnWood;
    int KnifesOnWood, MaxKnifesOnWood;


    [SerializeField] int AppleChaneToSpawn;
    GameObject[] StaticKnifes;
    GameObject[] Apples;
    void Start()
    {
        
        StartCoroutine(RandRot());
    }
    private void Awake()
    {
        Global.KnifeCountPerLevel = Random.Range(5, 10);
        Global.DefaultKnife= Global.KnifeCountPerLevel;
        Debug.Log(Global.KnifeCountPerLevel);
        MaxKnifesOnWood = Random.Range(1, 3);
        if (Random.Range(0, 100) < AppleChaneToSpawn)
        {
            int rand = Random.Range(0, RandPoint.Length);
            GameObject newApple = Instantiate(Apple, RandPoint[rand].transform.position, RandPoint[rand].transform.rotation);
            newApple.transform.parent = this.transform;
            RandPoint[rand] = Apple;
        }
        RandKnife();
    }
    void RandKnife()
    {
        int rand = Random.Range(0, RandPoint.Length);
        if (RandPoint[rand].gameObject.tag == "Untagged"&& KnifesOnWood< MaxKnifesOnWood)
        {
            GameObject newKnife = Instantiate(KnifeOnWood, RandPoint[rand].transform.position, RandPoint[rand].transform.rotation);
            newKnife.gameObject.tag = "KnifeStatic";
            newKnife.gameObject.GetComponent<Knife>().Hited = true;
            newKnife.transform.parent = this.transform;
            RandPoint[rand] = KnifeOnWood;
            KnifesOnWood += 1;
            Debug.Log(Global.KnifeCountPerLevel);
            RandKnife();
        }
        else if(KnifesOnWood < MaxKnifesOnWood)
        {
            RandKnife();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
        
    }
    private void FixedUpdate()
    {
        z += Time.deltaTime * RotateSpeed;
        transform.rotation = Quaternion.Euler(0, 0, z);
    }

    public void Crash()
    {
       
        Particle_Chips.Play();
        Particle_Dust.Play();
        StaticKnifes = GameObject.FindGameObjectsWithTag("KnifeStatic");
        
        for (int i = 0; i < StaticKnifes.Length; i++)
        {
            StaticKnifes[i].gameObject.GetComponent<Knife>().ThrowOut();
            StaticKnifes[i].transform.parent = null;
        }

        Apples = GameObject.FindGameObjectsWithTag("Apple");
        for (int i = 0; i < Apples.Length; i++)
        {
            Apples[i].gameObject.GetComponent<Knife>().ThrowOut();
            Apples[i].transform.parent = null;
        }
        for (int i=0;i< Wood_Tiles.Length;i++)
        {
            Wood_Tiles[i].transform.parent = null;
            Wood_Tiles[i].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            Wood_Tiles[i].GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-RandMin, RandMax), Random.Range(RandMin, RandMax)) * Crash_Force, ForceMode2D.Impulse);
            Wood_Tiles[i].GetComponent<WoodTile>().Rotate = true;
        }
    }

    IEnumerator RandRot()
    {
        if (Random.Range(-2, 2) < 0)
        {
            RotateSpeed = Random.Range(-rotateSpeedMin, -rotateSpeedMax);
        }
        else
        {
            RotateSpeed = Random.Range(rotateSpeedMin, rotateSpeedMax);
        }
        yield return new WaitForSeconds(Random.Range(RandTimeMin, RandTimeMax));
        StartCoroutine(RandRot());
    }
}
