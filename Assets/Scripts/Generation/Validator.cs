using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonCrawler.Generation
{
    public class Validator : MonoBehaviour
    {
        [SerializeField] public GameObject myGameObject;
        //[SerializeField] public DungeonNode myNode;
        [SerializeField] bool isValid = true;

        public bool IsValid => isValid;

        private void Awake()
        {
            Debug.Log($"{myGameObject.name} instantiated");
            Collider collider = GetComponent<Collider>();
            //myNode = myGameObject.GetComponent<DungeonNode>();
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log($"{myGameObject.name} validator collided with {other.name}");
            if (other.GetComponent<Validator>() != null)
            {
                Debug.Log("Validation if statement executed.");
                isValid = false;
            }
        }
    }
}