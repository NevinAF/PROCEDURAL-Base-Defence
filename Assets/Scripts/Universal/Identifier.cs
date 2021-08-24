using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Identifier : MonoBehaviour
{
    public enum Flag { Ally, Enemy, Player, Walker, Structure, Base, Weapon, Terrain, }

    public Flag[] flags;

}
