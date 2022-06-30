using System;
using Cinemachine;
using DG.Tweening;
using PGSauce.Animation;
using UnityEngine;

namespace _Elementis.Scripts.Growing_Tree_Dungeon
{
    public class DungeonDoor : MonoBehaviour
    {
        public DotweenProfile openProfile;
        public float addRotationY = 90f;
        public Transform leftDoor;
        public Transform rightDoor;
        public CinemachineVirtualCamera cam;
        public AudioSource doorSound;
        
        public void Open(Action onOpened)
        {
            cam.Priority = 100;
            doorSound.Play();
            DOTween.Sequence()
                .Append(leftDoor.DOLocalRotate(new Vector3(0, addRotationY, 0), openProfile.Duration)
                    .SetAs(openProfile.Params))
                .Join(rightDoor.DOLocalRotate(new Vector3(0, -addRotationY, 0), openProfile.Duration)
                    .SetAs(openProfile.Params))
                .AppendCallback(() =>
                {
                    cam.Priority = 0;
                    doorSound.Stop();
                    onOpened();
                });
        }
    }
}