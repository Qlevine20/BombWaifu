using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera cam;
    public Transform bombThrowLocation;
    public LayerMask interactionMask;
    public Collider myCollider;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 100, interactionMask))
            {
                Bomb newBomb = BombPool.Instance.Get();
                newBomb.transform.position = bombThrowLocation.position;
                newBomb.transform.rotation = bombThrowLocation.rotation;
                newBomb.transform.localScale = bombThrowLocation.localScale;
                newBomb.gameObject.SetActive(true);
                newBomb.Throw(hit.point, myCollider);
            }
        }
    }
} 