using System;
using DG.Tweening;
using PGSauce.Animation;

namespace _Elementis.Scripts.Skybox_Blending
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Rendering;
    public class SkyboxBlender : MonoBehaviour
    {
        public Material skybox;
        public DotweenProfile skyAnim;

        private Tween _fadeTween;
        private static readonly int BlendCubemaps = Shader.PropertyToID("_BlendCubemaps");

        private void Awake()
        {
            FadeToDefaultSky();
        }

        public void FadeToCloudySky()
        {
            _fadeTween?.Kill();
            _fadeTween = DOVirtual.Float(0, 1, skyAnim.Duration, t =>
                {
                    skybox.SetFloat(BlendCubemaps, 1-t);
                })
                //.SetAs(skyAnim.Params)
                .OnComplete(() =>
                {
                    skybox.SetFloat(BlendCubemaps, 0);
                    _fadeTween = null;
                });
        }

        public void FadeToDefaultSky()
        {
            _fadeTween?.Kill();
            _fadeTween = DOVirtual.Float(0, 1, skyAnim.Duration, t =>
                {
                    skybox.SetFloat(BlendCubemaps, t);
                })
                //.SetAs(skyAnim.Params)
                .OnComplete(() =>
                {
                    skybox.SetFloat(BlendCubemaps, 1);
                    _fadeTween = null;
                });
        }
    }
}