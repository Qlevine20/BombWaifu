using System;
using UnityEngine;

public class HeartUpgrade : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player"))
        {
            GiveUpgrade(other.gameObject.GetComponentInParent<PlayerController>());
        }
    }

    private void GiveUpgrade(PlayerController pc)
    {
        pc.GainHeartUpgrade();
        HeartUpgradePool.Instance.ReturnToPool(this);
    }
}
