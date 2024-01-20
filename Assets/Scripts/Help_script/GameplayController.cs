using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameplayController : MonoBehaviour
{   
    
    public static GameplayController instance;

    public GameObject fruit_pickup;
    public GameObject mushroom_pickup;

    public GameObject golden_apple;
    
    private float min_X = -20f, max_X = 20f, min_Y = -10f, max_Y = 10f, Z = 19.15f;

    private TMP_Text score_text;
    private int score_count;
    public bool spawn = true;
    void Awake()
    {
        MakeInstance();
    }

    void Start(){
        score_text = GameObject.Find("scoreText").GetComponent<TMP_Text>();
        if(spawn){
            SpawnPickups();
        }
    }
    void Update(){
        if(spawn){
            SpawnPickups();
        }
    }
    void MakeInstance()
    {
       if(instance == null){
        instance = this;
       } 
    }

    void SpawnPickups(){
        spawn = false;
        Instantiate(fruit_pickup, new Vector3(Random.Range(min_X, max_X), Random.Range(min_Y,max_Y), Z), Quaternion.identity);
        if(Random.Range(0,7)<=1){
            Instantiate(mushroom_pickup, new Vector3(Random.Range(min_X, max_X), Random.Range(min_Y,max_Y), Z), Quaternion.identity);
        }
        if(Random.Range(0,15)<=1){
            Instantiate(golden_apple, new Vector3(Random.Range(min_X, max_X), Random.Range(min_Y,max_Y), Z), Quaternion.identity);
        }
        
    }

    public void IncreaseScore(){
        score_count ++;
        score_text.text = "SCORE : " + score_count.ToString();
    }

}
