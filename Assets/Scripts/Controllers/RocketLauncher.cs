using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class RocketLauncher : MonoBehaviour// sadece 1 tane olacak
{
    public List<Transform> slots;
    [SerializeField]
    ShipManager shipManager;
    Queue<RocketBase> rockets = new Queue<RocketBase>();
    private void OnEnable()
    {
        shipManager.OnRocketLaunching += Fire;

    }
    private void OnDisable()
    {
        shipManager.OnRocketLaunching -= Fire;
    }

    public bool Add(RocketBase rocket)
    {
        if (rockets.Count <= 3)
        {
            if (!rocket.isAdded)
            {
                Instantiate(rocket).gameObject.SetActive(false);
                rockets.Enqueue(rocket);
                rocket.transform.DOLocalJump(transform.position, 2f, 1, 1f).OnStepComplete(() => rocket.gameObject.SetActive(true));
                rocket.isAdded = true;
                return true;
            }
        }
        return false;
    }

    public void Fire(ShipBase sourceShip)
    {
        if (rockets.Count > 0)
        {
            RocketBase rocket = rockets.Dequeue();
            rocket.gameObject.SetActive(true);
            int x = slots.Count;
            int randomValue = Random.Range(0, x);
            rocket.transform.SetParent(slots[randomValue]);
            rocket.transform.localPosition = Vector3.zero;
            rocket.transform.DOLocalMoveZ(2f, .4f).OnStepComplete(() => { rocket.Fire(sourceShip); });

        }
    }


}
