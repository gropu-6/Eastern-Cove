using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameManager : MonoBehaviour
{
    public GameObject _prefab;
    public Sprite[] _enemySprites;

    private float canvasScale;

    private GameObject _minigame;
    private Text _text;
    private Transform _progress;
    private Transform _player;
    private Transform _organ;
    private Transform _bounds;

    public bool game_started = false;
    public bool game_won = false;
    private bool button_held;
    public float time_since;

    public float[] player_speed = new float[3];
    public float[] organ_speed = new float[3];

    private bool change_direction;

    private float experience = 0.5f;

    public int current_wave = 0;

    public string[] winning_texts;
    public string[] failed_texts;
    public string[] waveTexts;

    public KeyCode _key = KeyCode.Space;

    void Start(){
        _minigame = Instantiate(_prefab);
        _minigame.transform.SetParent(GameObject.Find("Canvas").transform);
        _minigame.GetComponent<RectTransform>().anchoredPosition = new Vector2(30.0f, 0.0f);
        _minigame.transform.localScale = new Vector3(1,1,1);

        _player = _minigame.transform.GetChild(1).GetChild(0);
        _organ = _minigame.transform.GetChild(1).GetChild(1);
        _progress = _minigame.transform.GetChild(1).GetChild(2);
        _bounds = _minigame.transform.GetChild(2);
        _text = _minigame.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>();

    }

    void Update(){
        
        canvasScale = GameObject.Find("Canvas").transform.localScale.x;

        if(!game_won) control();

        if(game_started && !game_won){
            player_movement();
            enemy_movement();
            progressHandler();
        }

        if(game_won){
            
            if(current_wave == 2){
                _text.text = "You did it!, Congrats you can now progress with the game.";
                if(Time.time * 1000.0f - time_since > 2000.0f){
                    _minigame.GetComponent<Animator>().Play("Shrink");
                }
            }
            else
                if(Time.time * 1000.0f - time_since > 2000.0f){
                    float upperBound = _bounds.GetChild(1).position.y - (_organ.GetComponent<RectTransform>().sizeDelta.y * canvasScale) / 2;
                    float lowerBound = _bounds.GetChild(0).position.y + (_organ.GetComponent<RectTransform>().sizeDelta.y * canvasScale) / 2;
                    
                    _text.text = waveTexts[current_wave];
                    float y = Random.Range(lowerBound, upperBound);
                    _organ.position = new Vector3(_organ.position.x, y, _organ.position.z);
                    experience = 0.5f;
                    current_wave += 1;
                    _organ.GetComponent<Image>().sprite = _enemySprites[current_wave];
                    organ_speed[0] += 50.0f;
                    game_won = false;
                    game_started = false;
                }

        }

        
    }

    private void control(){

        if(Input.GetKey(_key)){
            time_since = Time.time * 1000.0f;
            if(!game_started) game_started = true;
        }

        if(Input.GetKeyDown(_key))
            button_held = true;
        else if(Input.GetKeyUp(_key)){
            button_held = false;
            player_speed[2] = 0.0f;
        }

    }

    private void movement(Transform Entity, float[] Speed){

        float upperBound = _bounds.GetChild(1).position.y - (Entity.GetComponent<RectTransform>().sizeDelta.y * canvasScale) / 2;
        float lowerBound = _bounds.GetChild(0).position.y + (Entity.GetComponent<RectTransform>().sizeDelta.y * canvasScale) / 2;

        float pY = Mathf.Clamp(Entity.position.y, lowerBound, upperBound);

        Speed[2] = ( Speed[0] / 100.0f ) * Time.deltaTime;
        Speed[1] = Mathf.Clamp(Speed[1], -Speed[0], Speed[0]);

        if(Speed[1] != 0 && (Entity.position.y <= lowerBound || Entity.position.y >= upperBound))
            Speed[1] *= -0.45f;

        pY += Speed[1];
        Entity.position = new Vector3(Entity.position.x, pY, Entity.position.y);

    }

    private void player_movement(){

        if(Time.time * 1000.0f - time_since < 1000.0f){
            if(button_held) player_speed[1] += player_speed[2];
            else player_speed[1] -= player_speed[2];
        }

        player_speed[1] -= player_speed[2] / 2.0f;

        movement(_player, player_speed);
    }

    private void enemy_movement(){

        movement(_organ, organ_speed);

        if(Random.Range(0, 100) < 25)
            change_direction = !change_direction;

        if(change_direction)
            organ_speed[1] += organ_speed[2];
        else
            organ_speed[1] -= organ_speed[2];

    }

    private bool UIColliders(Transform A, Transform B){
        float upperBound = B.position.y + (B.GetComponent<RectTransform>().sizeDelta.y * canvasScale) / 2;
        float lowerBound = B.position.y - (B.GetComponent<RectTransform>().sizeDelta.y * canvasScale) / 2; 

        return A.position.y > lowerBound && A.position.y < upperBound;
    }

    private void progressHandler(){

        experience = Mathf.Clamp(experience, 0.0f, 1.0f);

        _organ.GetComponent<Animator>().SetBool("inside", UIColliders(_organ, _player));

        if(UIColliders(_organ, _player))
            experience += 0.15f * Time.deltaTime;
        else experience -= 0.15f * Time.deltaTime;
            
        _progress.GetComponent<Slider>().value = experience;

        if(experience >= 1) {
            game_won = true;
            if(current_wave == 2){
                
            } else{
                _text.text = winning_texts[Random.Range(0, winning_texts.Length)];
                time_since = Time.time * 1000.0f;
                
            }
            
        }

    }
}
