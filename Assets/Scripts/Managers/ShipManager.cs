using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ShipManager : MonoBehaviour
{
    public Action<ShipBase> OnRocketLaunching;
    public Action<RocketBase> onAddingRocket;
}
