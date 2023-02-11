using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CannonScript : MonoBehaviour
{

    public GameObject Loader;
    public GameObject Ceiling;
    public SpriteRenderer nextColorLoader;
    public SpriteRenderer currentColorLoader;

    public static List<int> colorList = new List<int>();

    public Sprite[] spriteArray;

    private bool started = false;

    private static int nextColor;
    private static int currentColor;

    public GameObject RedBubble;
    public GameObject OrangeBubble;
    public GameObject YellowBubble;
    public GameObject GreenBubble;
    public GameObject BlueBubble;
    public GameObject PurpleBubble;
    public GameObject WhiteBubble;
    public GameObject BlackBubble;

    private GameObject projectileObject;
    
    Vector2 cannonDirection;
    private float angle;

    private int shotCounter;

    void Start()
    {
        makeList();
        nextColor = colorList[Random.Range(0, colorList.Count)];
        changeNext();
        started = true;
    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;

        Vector3 objectPos = Camera.main.WorldToScreenPoint (transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        cannonDirection.Set(mousePos.x, mousePos.y);
        cannonDirection.Normalize();
 
        angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));

        if (Input.GetMouseButtonDown(0) && started == true)
        {        
            if (currentColor == 0)
            {
                projectileObject = Instantiate(RedBubble, transform.position, Quaternion.identity);
            }
            else if (currentColor == 1)
            {
                projectileObject = Instantiate(OrangeBubble, transform.position, Quaternion.identity);                    
            }
            else if (currentColor == 2)
            {
                projectileObject = Instantiate(YellowBubble, transform.position, Quaternion.identity);              
            }
            else if (currentColor == 3)
            {
                projectileObject = Instantiate(GreenBubble, transform.position, Quaternion.identity);               
            }
            else if (currentColor == 4)
            {
                projectileObject = Instantiate(BlueBubble, transform.position, Quaternion.identity);                 
            }
            else if (currentColor == 5)
            {
                projectileObject = Instantiate(PurpleBubble, transform.position, Quaternion.identity);                    
            }
            else if (currentColor == 6)
            {
                projectileObject = Instantiate(WhiteBubble, transform.position, Quaternion.identity);                  
            }
            else if (currentColor == 7)
            {
                projectileObject = Instantiate(BlackBubble, transform.position, Quaternion.identity);                   
            }
            activeChecker bubble = projectileObject.GetComponent<activeChecker>();
            bubble.Launch(cannonDirection, 1000);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void changeNext()
    {
        makeList();
        currentColor = nextColor;
        currentColorLoader = GameObject.FindWithTag("Current Color").GetComponent<SpriteRenderer>();
        currentColorLoader.sprite = spriteArray[currentColor];
        nextColor = colorList[Random.Range(0, colorList.Count)];
        nextColorLoader = GameObject.FindWithTag("Next Color").GetComponent<SpriteRenderer>();
        nextColorLoader.sprite = spriteArray[nextColor];
    }

    public void makeList()
    {
        colorList.Clear();
        if(GameObject.FindGameObjectsWithTag("RedBubble").Length >= 1)
        {
            colorList.Add(0);
        }
        if(GameObject.FindGameObjectsWithTag("OrangeBubble").Length >= 1)
        {
            colorList.Add(1);
        }
        if(GameObject.FindGameObjectsWithTag("YellowBubble").Length >= 1)
        {
            colorList.Add(2);
        }
        if(GameObject.FindGameObjectsWithTag("GreenBubble").Length >= 1)
        {
            colorList.Add(3);
        }
        if(GameObject.FindGameObjectsWithTag("BlueBubble").Length >= 1)
        {
            colorList.Add(4);
        }
        if(GameObject.FindGameObjectsWithTag("PurpleBubble").Length >= 1)
        {
            colorList.Add(5);
        }
        if(GameObject.FindGameObjectsWithTag("WhiteBubble").Length >= 1)
        {
            colorList.Add(6);
        }
        if(GameObject.FindGameObjectsWithTag("BlackBubble").Length >= 1)
        {
            colorList.Add(7);
        }
    }
}
