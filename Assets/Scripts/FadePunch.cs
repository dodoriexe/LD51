using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadePunch : MonoBehaviour
{
    GameObject player;
    PlayerCombat combat;
    SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        combat = player.GetComponent<PlayerCombat>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(combat.currentFireCooldown > 0)
        {
            sprite.color = new Color(0.3f, 0.3f, 0.3f, 0.3f);
        }
        else
        {
            sprite.color = new Color(1.0f, 1.0f, 1.0f, 0.8f);
        }
    }
}
