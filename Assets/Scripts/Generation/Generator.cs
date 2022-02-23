using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DungeonCrawler.Generation
{
    public class Generator : MonoBehaviour
    {
        [SerializeField] List<GameObject> nodeList;

        List<GameObject> generatedNodes = new List<GameObject>();

        void Start()
        {
            GameObject currentNode = null;
            if (nodeList.Any(n => n.GetComponent<DungeonNode>().roomType == RoomTypes.DungeonStart))
            {
                Debug.Log("Entered if statement.");
                List<GameObject> startNodes = nodeList.Where(n => n.GetComponent<DungeonNode>().roomType == RoomTypes.DungeonStart).ToList();
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

                List<DoorAnchor> doorAnchors = currentNode.GetComponentsInChildren<DoorAnchor>().ToList();
                foreach (DoorAnchor doorAnchor in doorAnchors)
                {
                    List<GameObject> validConnectionNodes = new List<GameObject>();
                    foreach (RoomTypes roomType in doorAnchor.validConnectionTypes)
                    {
                        validConnectionNodes.AddRange(nodeList.Where(n => n.GetComponent<DungeonNode>().roomType == roomType));
                    }
                    if (validConnectionNodes.Count == 0)
                    {
                        // Replace door with wall
                        if (doorAnchor.door != null)
                        {
                            Destroy(doorAnchor.door);
                        }
                        Instantiate(doorAnchor.wall, doorAnchor.transform);
                    }
                    else if (validConnectionNodes.Count > 1)
                    {
                        GameObject nextNode = validConnectionNodes[Random.Range(0, validConnectionNodes.Count)];
                        currentNode = Instantiate(nextNode, transform);
                        currentNode.transform.position = doorAnchor.transform.position;
                        currentNode.transform.Rotate(0, doorAnchor.connectionRotation, 0);
                        currentNode.SetActive(true);
                        generatedNodes.Add(currentNode);
                    }
                    else
                    {
                        GameObject nextNode = validConnectionNodes.First();
                        currentNode = Instantiate(nextNode, transform);
                        currentNode.transform.position = doorAnchor.transform.position;
                        currentNode.transform.Rotate(0, doorAnchor.connectionRotation, 0);
                        currentNode.SetActive(true);
                        generatedNodes.Add(currentNode);
                    }
                }
            }
        }
    }
}