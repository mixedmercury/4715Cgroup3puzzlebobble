using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activeChecker : MonoBehaviour
{

    public CannonScript Cannon;
    Rigidbody2D rigidbody2d;
    public bool active;
    private static GameObject lastFiredObject;
    private bool counted = false;
    
    void Awake()
    {
        lastFiredObject = gameObject;
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force);
        Cannon.makeList();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag != "Wall")
        {
            if(active == true)
            {
                rigidbody2d.constraints = RigidbodyConstraints2D.FreezeAll;
                if (other.gameObject.tag == "Roof")
                {
                    transform.position = new Vector3((Mathf.Round((transform.position.x)-.5f) + .5f), 5, 0);
                }
                else
                {
                    Vector3 col = other.transform.position;
                    Vector3 dir = transform.position - other.transform.position;
                    float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
                    angle = angle + 180;
                    if (angle >= 0 && angle < 60 )
                    {
                        transform.position = col + new Vector3(-.5f, -.875f, 0);
                        if (transform.position.x < -3.5)
                        {
                            transform.position = col + new Vector3(.5f, -.875f, 0);
                        }
                    }
                    else if (angle >= 60 && angle <120)
                    {
                        transform.position = col + new Vector3(-1, 0, 0);
                    }
                    else if (angle >= 120 && angle <180)
                    {
                        if (transform.position.x > 3.5)
                        {
                            transform.position = col + new Vector3(.5f, .875f, 0);
                        }
                    }
                    else if (angle >= 180 && angle <240)
                    {
                        transform.position = col + new Vector3(.5f, .875f, 0);
                        if (transform.position.x > 3.5)
                        {
                            transform.position = col + new Vector3(-.5f, .875f, 0);
                        }
                    }
                    else if (angle >= 240 && angle <300)
                    {
                        transform.position = col + new Vector3(1, 0, 0);
                    }
                    else if (angle >= 300 && angle <360)
                    {
                        transform.position = col + new Vector3(.5f, -.875f, 0);
                        if (transform.position.x > 3.5)
                        {
                            transform.position = col + new Vector3(-.5f, -.875f, 0);
                        }
                    }
                }
                StartCoroutine(waitToCheck());
                active = false;
                Cannon.changeNext();
            }
        }
    }
    IEnumerator waitToCheck()
    {
        yield return 1;
        checkColor(0);
    }

    public void checkColor(int chain)
    {
        if(chain >=3)
        {
            if (counted == false)
            {
                counted = true;
                Collider2D[] colorChecker = Physics2D.OverlapCircleAll(transform.position, .55f);   //detects all colliders in area
                foreach (var item in colorChecker)  //creates variable for every collider
                {
                    activeChecker colorChain = item.GetComponent<Collider2D>().GetComponent<activeChecker>();   //gets script for all colliders in area
                    if (item.tag == gameObject.tag) //checks if objects have same tag as object that triggered
                    {
                        colorChain.checkColor(4);
                        Cannon.makeList();
                    }
                } 
            }
            else
            {
                Destroy(gameObject);
                Destroy(lastFiredObject);
                Cannon.makeList();
            }
        }
        else if (counted == false) //checks if counted in chain yet
        {
            chain = chain + 1; //adds to chain (number of colors in a row)
            int surrounded = 0; //idk what this would do lmao
            counted = true; //marks as counted in chain
            Collider2D[] colorChecker = Physics2D.OverlapCircleAll(transform.position, .55f);   //detects all colliders in area
            foreach (var item in colorChecker)  //creates variable for every collider
            {
                activeChecker colorChain = item.GetComponent<Collider2D>().GetComponent<activeChecker>();   //gets script for all colliders in area
                if (item.tag == gameObject.tag) //checks if objects have same tag as object that triggered
                {
                    surrounded = surrounded + 1;  //adds 1 to chain value received for every object with same tag next to original (including original)
                    colorChain.checkColor(chain); //calls this function on every object bordering with same tag with new chain value. Each will be counted, adding to the chain
                    if (surrounded >= 3) //if 2 or more immediately surrounding
                    {
                        foreach (var sameColor in colorChecker)
                        {
                            if (sameColor.tag == gameObject.tag)
                            {
                                colorChain.checkColor(4);
                                Destroy(gameObject);
                                Cannon.makeList();
                            }
                        }
                    }
                    if (chain >=3)
                    {
                        colorChain.checkColor(4);
                        Destroy(gameObject);
                    }
                }
            }
            counted = false;
        }
    }
}