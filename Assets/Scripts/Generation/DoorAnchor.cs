using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonCrawler.Generation
{
    public class DoorAnchor : MonoBehaviour
    {
        [SerializeField] public int connectionRotation = 0;
        [SerializeField] public GameObject door;
        [SerializeField] public GameObject wall;
        [SerializeField] public RoomTypes[] validConnectionTypes;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}