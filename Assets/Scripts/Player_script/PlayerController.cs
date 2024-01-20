using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    [HideInInspector]
    public PlayerDirection direction;

    //[HideInInspector]
    public float step_lenght = 0.5f;

    public GameObject gameOverMenu;

    [HideInInspector]
    public float movement_frequency = 0.1f;
    private float counter;
    private bool move;

    [SerializeField]
    private GameObject tailPrefab;
    
    private List<Vector3> delta_position;

    private List<Rigidbody> body_parts;

    private Rigidbody main_body;
    private Rigidbody head_body;
    private Transform tr;

    private bool create_tail;

    private bool immortal;

    public GameObject golden_apple;

    // Start is called before the first frame update
    void Awake()
    {
        tr = transform;
        main_body = GetComponent<Rigidbody>();

        InitSnake();
        InitPlayer();

        delta_position = new List<Vector3>(){
            new Vector3(-step_lenght,0f),
            new Vector3(0f, step_lenght),
            new Vector3(step_lenght,0f),
            new Vector3(0f, -step_lenght)
        };
    }

    // Update is called once per frame
    void Update()
    {
        CheckMovementFrequency();
    }

    void FixedUpdate(){
        if(move){
            move = false;
            Move();
        }
    }

    void InitSnake(){
        body_parts = new List<Rigidbody>();
        body_parts.Add(tr.GetChild(0).GetComponent<Rigidbody>());
        body_parts.Add(tr.GetChild(1).GetComponent<Rigidbody>());
        body_parts.Add(tr.GetChild(2).GetComponent<Rigidbody>());
        body_parts.Add(tr.GetChild(3).GetComponent<Rigidbody>());

        head_body = body_parts[0];
    }
    void SetDirection(){
        int random_dir = Random.Range(0, (int)PlayerDirection.COUNT);
        direction = (PlayerDirection)random_dir;
        
    }
    void InitPlayer(){
        SetDirection();
        switch(direction){
            case PlayerDirection.RIGHT:
                body_parts[1].position = body_parts[0].position - new Vector3(Metrics.NODE, 0f, 0f);
                body_parts[2].position = body_parts[0].position - new Vector3(Metrics.NODE*2, 0f, 0f);
                body_parts[3].position = body_parts[0].position - new Vector3(Metrics.NODE*3, 0f, 0f);
                
                break;
            case PlayerDirection.LEFT:
                body_parts[1].position = body_parts[0].position + new Vector3(Metrics.NODE, 0f, 0f);
                body_parts[2].position = body_parts[0].position + new Vector3(Metrics.NODE*2, 0f, 0f);
                body_parts[3].position = body_parts[0].position + new Vector3(Metrics.NODE*3, 0f, 0f);
                body_parts[0].transform.Rotate(0f, 0f, 180f);
                break;
            case PlayerDirection.UP:
                body_parts[1].position = body_parts[0].position - new Vector3(0f, Metrics.NODE, 0f);
                body_parts[2].position = body_parts[0].position - new Vector3(0f, Metrics.NODE*2, 0f);
                body_parts[3].position = body_parts[0].position - new Vector3(0f, Metrics.NODE*3, 0f);
                body_parts[0].transform.Rotate(0f, 0f, 90f);
                break;
            case PlayerDirection.DOWN:
                body_parts[1].position = body_parts[0].position + new Vector3(0f, Metrics.NODE, 0f);
                body_parts[2].position = body_parts[0].position + new Vector3(0f, Metrics.NODE*2, 0f);
                body_parts[3].position = body_parts[0].position + new Vector3(0f, Metrics.NODE*3, 0f);
                body_parts[0].transform.Rotate(0f, 0f, -90f);
                
                break;
        }
    }

    void Move(){
        Vector3 dPosition = delta_position[(int)direction];
        Vector3 parentPos = head_body.position;
        Vector3 prevPosition;
        main_body.position = main_body.position + dPosition;
        head_body.position = head_body.position + dPosition;

        for (int i = 1; i < body_parts.Count; i++){
            prevPosition = body_parts[i].position;
            body_parts[i].position = parentPos;
            parentPos = prevPosition;
        }

        if(create_tail){
            create_tail = false;
            for(int i =0; i <2; i++){
                GameObject newTail = Instantiate(tailPrefab, body_parts[body_parts.Count-1].position, Quaternion.identity);
                newTail.transform.SetParent(transform, true);
                body_parts.Add(newTail.GetComponent<Rigidbody>());
            }
            
            
        }
    }

    void CheckMovementFrequency(){
        counter += Time.deltaTime;

        if(counter >= movement_frequency){
            counter = 0;
            move = true;
        }
    }

    public void SetInputDirection(PlayerDirection dir) {
        if( dir == PlayerDirection.UP && direction == PlayerDirection.DOWN ||
            dir == PlayerDirection.DOWN && direction == PlayerDirection.UP ||
            dir == PlayerDirection.RIGHT && direction == PlayerDirection.LEFT ||
            dir == PlayerDirection.LEFT && direction == PlayerDirection.RIGHT ){
                return;
        }
        direction = dir;
        
        switch(direction){
            case PlayerDirection.RIGHT:
                body_parts[0].transform.localRotation = Quaternion.Euler(0, 0, 0);
                break;
            case PlayerDirection.LEFT:
                body_parts[0].transform.localRotation = Quaternion.Euler(0, 0, 180);
                break;
            case PlayerDirection.UP:
                body_parts[0].transform.localRotation = Quaternion.Euler(0, 0, 90);
                break;
            case PlayerDirection.DOWN:
                body_parts[0].transform.localRotation = Quaternion.Euler(0, 0, -90);
                break;
        }
        ForceMove();
    }

    void ForceMove(){
        counter = 0;
        move = false;
        Move();
    }

    void OnTriggerEnter(Collider target){
        if(target.tag == Tags.FRUIT){
            target.gameObject.SetActive(false);
            create_tail = true;
            GameplayController.instance.spawn = true;
            GameplayController.instance.IncreaseScore();
            AudioManager.instance.Play_pickupSound();
        }
        if(target.tag == Tags.MUSHROOM || target.tag == Tags.TAIL){
            if(immortal){
                immortal = false;
                golden_apple.SetActive(false);
                target.gameObject.SetActive(false);
            }
            else{
                AudioManager.instance.Play_gameoverSound();
                gameOverMenu.SetActive(true);
                target.gameObject.SetActive(false); 
                Time.timeScale = 0f;
            }
            
        }
        if(target.tag == Tags.WALL){
            AudioManager.instance.Play_gameoverSound();
            gameOverMenu.SetActive(true); 
            Time.timeScale = 0f;
        }
        if(target.tag == Tags.G_APPLE){
            immortal = true;
            AudioManager.instance.Play_pickupSound();
            golden_apple.SetActive(true);
            target.gameObject.SetActive(false);
        }
        
    }
}
