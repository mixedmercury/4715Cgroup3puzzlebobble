using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ceilingScript : MonoBehaviour
{
    private int turnCount = 0;
    public int ceilingMoves = 1;
    new Renderer renderer;
    private Vector2 size;
    public CannonScript Cannon;
    public GameObject WallOfDoom;
    public GameObject warninganiObject;
    public static int remainingBubbles = 1;
    private bool won = false;

    void Start()
    {
        turnCount = 0;
        ceilingMoves = 1;
        renderer = gameObject.GetComponent<Renderer>();
        size = renderer.bounds.size;
        warninganiObject.SetActive(false);
        var allBubbles = FindObjectsOfType<activeChecker>();
        foreach(var activeBubbles in allBubbles)
        {
            remainingBubbles += 1;
        }
    }

    void Update()
    {
        if(remainingBubbles <=0 && won == false)
        {
            Debug.Log("You Win!");
            won = true;
            var scene = SceneManager.GetActiveScene();
            if(scene.name == "mvereb code")
            {
                SceneManager.LoadScene("Level2Original");
            }
            if(scene.name == "Level2Original")
            {
                SceneManager.LoadScene("WinnerScreen");
            }
        }
    }

    public void checkCeiling()
    {
        turnCount += 1;
        Debug.Log("Turn Count: " + turnCount);
        remainingBubbles = 1;
        Collider2D[] ceilingChecker = Physics2D.OverlapBoxAll(transform.position, size * 1.5f, 0);
        foreach (var ceilingBubble in ceilingChecker)
        {
            if(ceilingBubble.tag != "Wall" && ceilingBubble.tag != "Roof")
            {
                activeChecker ceilingChain = ceilingBubble.GetComponent<Collider2D>().GetComponent<activeChecker>();
                ceilingChain.checkCeilingChain(turnCount);
            }
        }
        var allBubbles = FindObjectsOfType<activeChecker>();
        foreach(var activeBubbles in allBubbles)
        {
            activeChecker ceilingChain = activeBubbles.GetComponent<Collider2D>().GetComponent<activeChecker>();
            ceilingChain.StartCoroutine(ceilingChain.ceilingPopper(turnCount));
        }
        foreach(var activeBubbles in allBubbles)
        {
            remainingBubbles += 1;
        }
        remainingBubbles -= 1;
        if(turnCount == (ceilingMoves * 8) - 2) //Warning Animation 1
        {
            warninganiObject.SetActive(true);
        }
        if(turnCount == ceilingMoves * 8)
        {
            ceilingMoves = ceilingMoves + 1;
            Debug.Log("Ceiling Moves: " + ceilingMoves);
            foreach(var activeBubbles in allBubbles)
            {
                activeBubbles.transform.position = activeBubbles.transform.position + new Vector3(0, -.875f, 0);
            }
            warninganiObject.SetActive(false);
            transform.position = transform.position + new Vector3(0, -.875f, 0);
            WallOfDoom.transform.position = WallOfDoom.transform.position + new Vector3(0, -.875f, 0);
        }
        Cannon.StartCoroutine(Cannon.waitForList());
    }

    public void removeFromCount()
    {
        remainingBubbles -= 1;
    }
}
