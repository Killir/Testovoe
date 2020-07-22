using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager singleton;

    private void Awake()
    {
        singleton = this;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject target = GetRaycastHitGameObject();

            if (target) {
                if (target.tag == "chip") {
                    GameManager.singleton.SetActiveChip(target.GetComponent<Chip>());
                }

                if (target.tag == "field") {
                    GameManager.singleton.MoveActiveChip(target.GetComponent<Field>());
                }
            }
        }
    }

    GameObject GetRaycastHitGameObject()
    {
        GameObject obj = null;

        Vector3 cameraPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = Camera.main.transform.forward;
        RaycastHit2D hit = Physics2D.Raycast(cameraPosition, direction);
        if (hit.collider!= null)
            obj = hit.collider.gameObject;

        return obj;
    }

}
