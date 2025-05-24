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
        
        if (Time.time > _canFire)
        {
            //spawn my laser
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 2.55f, 0), Quaternion.identity);
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
        transform.Translate(Vector3.right * _speed * horizontalInput * Time.deltaTime);
        transform.Translate(Vector3.up * _speed * vertialInput * Time.deltaTime);

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
}

