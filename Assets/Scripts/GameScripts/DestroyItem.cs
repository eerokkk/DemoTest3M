using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyItem : MonoBehaviour
{
    public void TouchItem()
    {
        Destroy(gameObject);
        Debug.Log("Объект уничтожен");
    }
}
