using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.Service;
using Game.View;
using System;
using UnityEngine;

namespace Game.Presenter
{
    public class Loading : MonoBehaviour
    {
        public LoadingView view { get; private set; }

        public void Awake()
        {
            view = GetComponentInChildren<LoadingView>();
        }

        public void LoadNow()
        {
            view.LoadNow();
        }
    }
}
