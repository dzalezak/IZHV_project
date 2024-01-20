using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject mushroom;
    void Start()
    {
        Invoke("despawnMushroom",10f);
    }

    void despawnMushroom(){
        mushroom.SetActive(false);
    }
}
