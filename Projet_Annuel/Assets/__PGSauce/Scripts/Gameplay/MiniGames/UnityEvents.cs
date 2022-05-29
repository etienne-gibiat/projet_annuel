using System;
using UnityEngine;
using UnityEngine.Events;

namespace PGSauce.Gameplay.MiniGames
{
    [Serializable]public class UnityEventString : UnityEvent<string>{}
    [Serializable]public class UnityEventInt : UnityEvent<int>{}
    [Serializable]public class UnityEventFloat : UnityEvent<float>{}
    [Serializable]public class UnityEventAudioClip : UnityEvent<AudioClip, float>{}
    [Serializable]public class UnityEventTransform : UnityEvent<Transform>{}
}