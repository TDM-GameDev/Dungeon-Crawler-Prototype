using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonCrawler.Generation
{
    public class DungeonNode : MonoBehaviour
    {
        [SerializeField] RoomTypes roomType;
        [SerializeField] bool isLockedIn = false;
        [SerializeField] float rotationAngle = 0f;

        public RoomTypes RoomType => roomType;
        public bool IsLockedIn => isLockedIn;
        public float RotationAngle => rotationAngle;
    }
}