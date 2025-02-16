using Game.Presenter;
using Game.View;
using UnityEngine;
using Zenject;

namespace Game.Presenter
{

    public class Authen : MonoBehaviour
    {

        private AuthenView _authenView;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _authenView = GetComponentInChildren<AuthenView>();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

    }
}
