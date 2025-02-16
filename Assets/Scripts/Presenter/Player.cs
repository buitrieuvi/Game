using Game.Service;
using System.Security.Cryptography;
using UnityEngine;
using Zenject;

namespace Game.Presenter
{
    public class Player : MonoBehaviour
    {
        public PlayerMovement playerMovement { get; private set; }
        public PlayerCamera playerCamera { get; private set; }
        public PlayerInventory playerInventory { get; private set; }

        public void Awake()
        {
            playerMovement = GetComponent<PlayerMovement>();
            playerCamera = GetComponent<PlayerCamera>();
            playerInventory = GetComponent<PlayerInventory>();
        }
    }
}
