using Assets.Scripts.Characters.Player;
using Assets.Scripts.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatForm : MonoBehaviour
{
    public PlatformEffector2D effector;
    private PlayerCharacter player;
    void Start()
    {
        player = PlayerManager.instance.player;
        effector = GetComponent<PlatformEffector2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (player.YVelocity > 0)
            effector.rotationalOffset = 0f;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        effector.rotationalOffset = 0f;
    }
}
