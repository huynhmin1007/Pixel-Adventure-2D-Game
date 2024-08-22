using Assets.Scripts.Characters.ObservatoryBoss;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public static BossManager instance;
    public ObservatoryBossCharacter boss;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else instance = this;
    }


}
