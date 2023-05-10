using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Destination Card", menuName = "Scriptable Objects/Destination Card")]
public class SO_DestinationTicket : ScriptableObject
{
    public string from;
    public string to;
    public Sprite destinatnImage;
    public int points;
}
