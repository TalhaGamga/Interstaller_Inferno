using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public abstract class CollectableSkillBase : MonoBehaviour
{
    bool istriger = false;
    MeshRenderer meshRenderer;
    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public virtual Tween Use(ShipBase ship)
    {
        gameObject.layer = 0;
        transform.SetParent(ship.transform);
        return transform.DOLocalJump(Vector3.zero, 1f, 1, .5f).OnComplete(() => meshRenderer.enabled = false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (istriger)
        {
            return;
        }
        if (other.gameObject.TryGetComponent(out ShipBase shipBase))
        {
            istriger = true;
            Use(shipBase);

        }
    }

}
