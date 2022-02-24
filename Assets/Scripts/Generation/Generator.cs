using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DungeonCrawler.Generation
{
    public class Generator : MonoBehaviour
    {
        [SerializeField] List<GameObject> nodeList;
        [SerializeField] int numberOfChambersToGenerate = 2;
        [SerializeField] int numberOfRoomsToGenerate = 2;
        [SerializeField] int numberOfIntersectionsToGenerate = 5;

        [SerializeField] int chambersGenerated = 0;
        [SerializeField] int roomsGenerated = 0;
        [SerializeField] int intersectionsGenerated = 0;

        List<GameObject> generatedNodes = new List<GameObject>();
        List<GameObject> validConnectionNodes = new List<GameObject>();
        List<RoomTypes> availableRoomTypes = new List<RoomTypes>() { RoomTypes.RightTurn, RoomTypes.LeftTurn, RoomTypes.Hallway, RoomTypes.Intersection, RoomTypes.LargeChamber, RoomTypes.MediumChamber, RoomTypes.SmallChamber, RoomTypes.Room };

        GameObject currentNode = null;

        [SerializeField] string testNodeName = "";


        void Start()
        {
            GenerateDungeon();

            //GenerateStartingNode();

            //while (chambersGenerated < numberOfChambersToGenerate || roomsGenerated < numberOfRoomsToGenerate || intersectionsGenerated < numberOfIntersectionsToGenerate)
            //{

            //}

            for (int i = 0; i < 1; i++)
            {
                //ProcessAnchors();
            }
            //ProcessAnchorsTest();
        }

        public void GenerateDungeon()
        {
            foreach (var node in generatedNodes)
            {
                Destroy(node);
            }
            GenerateStartingNode();
            ProcessAnchorsTest();
        }

        private void GenerateStartingNode()
        {
            if (nodeList.Any(n => n.GetComponent<DungeonNode>().RoomType == RoomTypes.DungeonStart))
            {
                Debug.Log("Entered if statement.");
                List<GameObject> startNodes = nodeList.Where(n => n.GetComponent<DungeonNode>().RoomType == RoomTypes.DungeonStart).ToList();
                GameObject startNode;
                if (startNodes.Count > 0)
                {
                    startNode = startNodes[Random.Range(0, startNodes.Count)];
                }
                else
                {
                    startNode = startNodes.First();
                }
                currentNode = Instantiate(startNode, transform);
                currentNode.SetActive(true);
                generatedNodes.Add(currentNode);
            }
            ProcessAnchors();
        }

        private void ProcessAnchors()
        {
            if (currentNode == null)
            {
                return;
            }

            List<DoorAnchor> doorAnchors = currentNode.GetComponentsInChildren<DoorAnchor>().ToList();
            foreach (DoorAnchor doorAnchor in doorAnchors)
            {
                List<GameObject> validConnectionNodes = new List<GameObject>();
                foreach (RoomTypes roomType in doorAnchor.validConnectionTypes)
                {
                    validConnectionNodes.AddRange(nodeList.Where(n => n.GetComponent<DungeonNode>().RoomType == roomType));
                }
                if (validConnectionNodes.Count == 0)
                {
                    PlaceClosedDoor(doorAnchor);
                }
                else if (validConnectionNodes.Count > 1)
                {
                    //GameObject nextNode = validConnectionNodes[Random.Range(0, validConnectionNodes.Count)];
                    //currentNode = Instantiate(nextNode, transform);
                    ////currentNode.transform.position = doorAnchor.transform.position;
                    ////currentNode.transform.Rotate(0, doorAnchor.ConnectionRotation, 0);
                    //currentNode.SetActive(true);
                    //generatedNodes.Add(currentNode);
                    bool invalid = false;
                    
                    do
                    {
                        GenerateRoom(doorAnchor, validConnectionNodes.First(n => n.name == "Long Hallway"));
                        //GenerateRoom(doorAnchor, validConnectionNodes[Random.Range(0, validConnectionNodes.Count)]);
                    } while (invalid);
                    
                    List<Validator> validators = currentNode.GetComponentsInChildren<Validator>().ToList();
                    foreach (Validator validator in validators)
                    {
                        if (!validator.IsValid)
                        {
                            invalid = true;
                        }
                    }

                }
                else
                {
                    GenerateRoom(doorAnchor, validConnectionNodes.First());
                }
            }
        }

        private void ProcessAnchorsTest()
        {
            if (currentNode == null)
            {
                return;
            }

            List<DoorAnchor> doorAnchors = currentNode.GetComponentsInChildren<DoorAnchor>().ToList();
            foreach (DoorAnchor doorAnchor in doorAnchors)
            {
                List<GameObject> validConnectionNodes = new List<GameObject>();
                foreach (RoomTypes roomType in doorAnchor.validConnectionTypes)
                {
                    validConnectionNodes.AddRange(nodeList.Where(n => n.GetComponent<DungeonNode>().RoomType == roomType));
                }
                if (validConnectionNodes.Count == 0)
                {
                    PlaceClosedDoor(doorAnchor);
                }
                else if (validConnectionNodes.Count > 1)
                {
                    //GameObject nextNode = validConnectionNodes[Random.Range(0, validConnectionNodes.Count)];
                    //currentNode = Instantiate(nextNode, transform);
                    ////currentNode.transform.position = doorAnchor.transform.position;
                    ////currentNode.transform.Rotate(0, doorAnchor.ConnectionRotation, 0);
                    //currentNode.SetActive(true);
                    //generatedNodes.Add(currentNode);
                    bool invalid = false;

                    do
                    {
                        GenerateRoom(doorAnchor, validConnectionNodes.First(n => n.name == testNodeName));
                        //GenerateRoom(doorAnchor, validConnectionNodes[Random.Range(0, validConnectionNodes.Count)]);
                    } while (invalid);

                    List<Validator> validators = currentNode.GetComponentsInChildren<Validator>().ToList();
                    foreach (Validator validator in validators)
                    {
                        if (!validator.IsValid)
                        {
                            invalid = true;
                        }
                    }

                }
                else
                {
                    GenerateRoom(doorAnchor, validConnectionNodes.First());
                }
            }
        }

        private void GenerateRoom(DoorAnchor doorAnchor, GameObject nextNode)
        {
            DungeonNode node = nextNode.GetComponent<DungeonNode>();
            currentNode = Instantiate(nextNode, new Vector3(doorAnchor.transform.position.x, doorAnchor.transform.position.y, doorAnchor.transform.position.z), Quaternion.identity, transform);
            currentNode.transform.Rotate(new Vector3(0, node.RotationAngle, 0), Space.Self);
            //currentNode.transform.position = doorAnchor.transform.position;
            //currentNode.transform.Rotate(0, doorAnchor.ConnectionRotation, 0);
            currentNode.SetActive(true);
            generatedNodes.Add(currentNode);
        }

        private static void PlaceClosedDoor(DoorAnchor doorAnchor)
        {
            if (doorAnchor.door != null)
            {
                Destroy(doorAnchor.door);
            }
            Instantiate(doorAnchor.ClosedDoor, new Vector3(doorAnchor.transform.position.x + 0.5f, doorAnchor.transform.position.y, doorAnchor.transform.position.z), Quaternion.identity, doorAnchor.transform);
        }
    }
}