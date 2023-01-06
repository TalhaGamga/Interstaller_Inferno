using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public abstract class CollectableBase : MonoBehaviour, ICollectable
{
    public abstract void Use();
}
