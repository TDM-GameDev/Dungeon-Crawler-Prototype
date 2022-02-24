using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonCrawler.Generation
{
    public class DoorAnchor : MonoBehaviour
    {
        [SerializeField] int connectionRotation = 0;
        [SerializeField] public GameObject door;
        [SerializeField] GameObject closedDoor;
        [SerializeField] public RoomTypes[] validConnectionTypes;
        [SerializeField] bool hasBeenUsed = false;

        public int ConnectionRotation => connectionRotation;
        public bool HasBeenUsed => hasBeenUsed;
        public GameObject ClosedDoor => closedDoor;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetUsed()
        {
            hasBeenUsed = true;
        }
    }
}