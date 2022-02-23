using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonCrawler.Generation
{
    public class DungeonNode : MonoBehaviour
    {
        [SerializeField] public RoomTypes roomType;
        [SerializeField] public bool isLockedIn = false;
    }
}