using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ceilingScript : MonoBehaviour
{
    private static int turnCount = 0;
    new Renderer renderer;
    private Vector2 size;

    void Start()
    {
        renderer = gameObject.GetComponent<Renderer>();
        size = renderer.bounds.size;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            checkCeiling();
        }
    }

    public void checkCeiling()
    {
        turnCount = turnCount + 1;
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
                ceilingChain.ceilingPopper(turnCount);
            }
    }
}
