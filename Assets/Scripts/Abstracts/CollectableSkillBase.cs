using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public abstract class CollectableSkillBase : MonoBehaviour
{
    bool istriger = false;
    [SerializeField] GameObject obj;
    //private void Start()
    //{
    //    meshRenderer = GetComponent<MeshRenderer>();
    //}

    Sequence sq;

    public virtual Tween Use(ShipBase ship)
    {
        sq = DOTween.Sequence();

        transform.SetParent(ship.transform);
        gameObject.layer = 0;
        return sq.Append(transform.DOJump(Vector3.zero, 1f, 1, .5f)).Join(transform.DOScale(Vector3.one * .1f, .3f)).OnComplete(() => obj.SetActive(false));
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
