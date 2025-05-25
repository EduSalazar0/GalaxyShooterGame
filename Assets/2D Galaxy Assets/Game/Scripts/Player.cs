using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Player : MonoBehaviour
{
    //public or private identify
    //data type (int,float,bool,strings)
    //every variable has a name
    //option value assigned
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;

    //fireRate is 0.5f
    //canFire -- has the amount of time between diring passed?
    //Time.time
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = 0.0f;

    [SerializeField]
    private float _speed = 5.0f;
    public float horizontalInput;
    public float verticalInput;

    //powerup triple shot
    public bool canTripleShot = false;
    //powerup speed boost
    public bool canSpeedBoost = false;
    //powerup shields 
    public bool canShield = false;
    public int lives = 3;
    [SerializeField]
    private GameObject _explosionPrefab;
    [SerializeField]
    private GameObject _shieldGameObject;

    private UIManager _uiManager;
    private GameManager _gameManager;

    private SpawnManager _spawnManager;

    // Start is called before the first frame update
    private void Start()
    {
        //Debug.Log("Hola como estas?");
        //Debug.Log("Name: " + name);
        //Debug.Log("x pos: " + transform.position.x);
        //Debug.Log(transform.position);
        //current pos = new position
        //speed = 10;
        transform.position = new Vector3(0, 0, 0);
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if(_uiManager != null)
        {
            _uiManager.UpdateLives(lives);
        }
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        if(_spawnManager != null)
        {
            _spawnManager.StartSpawnRoutines();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        Movement();
        //if space key pressed
        //spawn laser at player position
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0))
        {
            Shoot();
        }

    }
    private void Shoot()
    {
        //if tiple shoot 
        //shoot 3 lasers
        //else
        //shoot 1

        if (Time.time > _canFire)
        {
            if (canTripleShot == true)
            {
                /*
                //Right
                Instantiate(_laserPrefab, transform.position + new Vector3(2.14f, -1.01f, 0), Quaternion.identity);
                //Center
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 2.55f, 0), Quaternion.identity);
                //Left
                Instantiate(_laserPrefab, transform.position + new Vector3(-2.14f, -1.01f, 0), Quaternion.identity);
                */
                Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);

            }
            else
            {
                //spawn my laser
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 2.55f, 0), Quaternion.identity);
            }


            _canFire = Time.time + _fireRate;
        }
    }
    private void Movement()
    {
        //Debug.Log("Eduardo");
        float horizontalInput = Input.GetAxis("Horizontal");
        float vertialInput = Input.GetAxis("Vertical");
        //new vector3(1->5,0,0) * 1 * 5, vector3(1->-3,0,0) * 1 * -3
        //vector3(1,0,0)* 1 * (-1-> left, 1 -> right)

        if (canSpeedBoost == true)
        {
            transform.Translate(Vector3.right * (_speed * 3.0f) * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.up * (_speed * 3.0f) * vertialInput * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.right * _speed * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.up * _speed * vertialInput * Time.deltaTime);
        }


        //if player on the y is greater than 0
        //set player position to 0

        if (transform.position.y > 4.2f)
        {
            transform.position = new Vector3(transform.position.x, 4.2f, 0);

        }
        else if (transform.position.y < -4.2f)
        {
            transform.position = new Vector3(transform.position.x, -4.2f, 0);

        }
        /* Controlar que no salga de los limites en x
         * if (transform.position.x > 13)
        {
            transform.position = new Vector3(13, transform.position.y, 0);
        }
        else if (transform.position.x < -13)
        {
            transform.position = new Vector3(-13, transform.position.y, 0);
        }*/
        //Hacer que nuestro player en caso de salirse por la derecha que aparezca en la izquierda y vicesersa
        if (transform.position.x > 8)
        {
            transform.position = new Vector3(-8, transform.position.y, 0);
        }
        else if (transform.position.x < -8)
        {
            transform.position = new Vector3(8, transform.position.y, 0);
        }
    }
    public void Damage()
    {
        if(canShield == true)
        {
            canShield = false;
            _shieldGameObject.SetActive(false);
            return;
        }
        lives -= 1;
        _uiManager.UpdateLives(lives);
        if (lives < 1)
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            _gameManager.gameOver = true;
            _uiManager.ShowTitleScreen();
            Destroy(this.gameObject);
        }
    }
    public void TripleShotPowerOn()
    {
        canTripleShot = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }
    public IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        canTripleShot = false;
    }

    public void SpeedBoostOn()
    {
        canSpeedBoost = true;
        StartCoroutine(SpeedBoostDown());
    }
    public IEnumerator SpeedBoostDown()
    {
        yield return new WaitForSeconds(5.0f);
        canSpeedBoost = false;
    }
    public void enableShield()
    {
        canShield = true;
        _shieldGameObject.SetActive(true);
    }
}


