using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodTile : MonoBehaviour
{
    public bool Rotate;
    float x,y,z, Randx, Randy, Randz;
    private void Start()
    {
        this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        Rotate = false;
        Randx = Random.Range(10, 50);
        Randy = Random.Range(10, 50);
        Randz = Random.Range(10, 50);
    }
    void Update()
    {
        if (Rotate)
        {
            x += Time.deltaTime * Randx;
            y += Time.deltaTime * Randy;
            z += Time.deltaTime * Randz;
            transform.rotation = Quaternion.Euler(x, y, z);
        }
    }
}
