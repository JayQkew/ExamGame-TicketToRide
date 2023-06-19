using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationLogic : MonoBehaviour
{
    [SerializeField] public List<GameObject> p1_connectedDestintaions = new List<GameObject>();
    [SerializeField] public List<GameObject> p2_connectedDestintaions = new List<GameObject>();

    [SerializeField] public List<GameObject> p1_connectedRoutes = new List<GameObject>();
    [SerializeField] public List<GameObject> p2_connectedRoutes = new List<GameObject>();
}
