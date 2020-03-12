using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructor : MonoBehaviour
{
    [SerializeField] float timeUntilRemoval = 3f;

    void Start()
    {
        Destroy(gameObject, timeUntilRemoval);
    }
}
