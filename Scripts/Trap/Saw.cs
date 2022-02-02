using UnityEngine;

public class Saw : TrapDamage
{
    [SerializeField] private float movementDistance;
    [SerializeField] private float speed;
    private bool movingToFirstEdge = true;
    [SerializeField] private bool isHorizontal = true;
    private float firstEdge;
    private float secondEdge;
    private void Awake()
    {
        if (isHorizontal)
        {
            firstEdge = transform.position.x - movementDistance;
            secondEdge = transform.position.x + movementDistance;
        }
        else 
        {
            firstEdge = transform.position.y + movementDistance;
            secondEdge = transform.position.y - movementDistance;
        }
    }
    private void Update()
    {
        SawMovement();
    }
    private void SawMovement()
    {
        // if "isHorizontal" is true, move it left and right, otherwise move it up and down
        // "isHorizontal" を変えることでトラップを左右か上下に移動ことができます
        if (isHorizontal)
        {
            if (movingToFirstEdge)
            {
                if (transform.position.x > firstEdge)
                {
                    transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
                }
                else
                {
                    movingToFirstEdge = false;
                }
            }
            else
            {
                if (transform.position.x < secondEdge)
                {
                    transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
                }
                else
                {
                    movingToFirstEdge = true;
                }                
            }
        }
        else
        {
            if (movingToFirstEdge)
            {
                if (transform.position.y < firstEdge)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime, transform.position.z);
                }
                else
                {
                    movingToFirstEdge = false;
                }
            }
            else
            {
                if (transform.position.y > secondEdge)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y - speed * Time.deltaTime, transform.position.z);
                }
                else
                {
                    movingToFirstEdge = true;
                }                
            }            
        }
    }
}
