using System.Collections.Generic;
using PGSauce.AudioManagement.External;
using PGSauce.Core.Strings;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PGSauce.AudioManagement
{
    /// <summary>A group of various different complete PgSfxObjects. Playing this PgSfxGroup will result in one of its constituent SFXObjects being played at random.</summary>
    [CreateAssetMenu(fileName = "SFX Group", menuName = MenuPaths.Audio + "SFX Group", order = 3)]
    public class PgSfxGroup : SFXGroup
    {
        public override Coroutine Play(PgSfxModule pgSfxModule)
        {
            return pgSfxModule.SfxManager.Play(this);
        }
        public override Coroutine Play(PgSfxModule pgSfxModule, float delayDuration)
        {
            return pgSfxModule.SfxManager.Play(this, delayDuration);
        }
        public override Coroutine Play(PgSfxModule pgSfxModule, float delayDuration, float volume)
        {
            return pgSfxModule.SfxManager.Play(this, delayDuration, volume);
        }

        public override Coroutine Play(PgSfxModule pgSfxModule, float delayDuration, float volume, float pitch)
        {
            return pgSfxModule.SfxManager.Play(this, delayDuration, volume, pitch);
        }

        public override Coroutine Play(PgSfxModule pgSfxModule, Vector3 position, Transform parent, bool isGlobalPosition)
        {
            return pgSfxModule.SfxManager.Play(this, position, parent, isGlobalPosition);
        }

        public override Coroutine Play(PgSfxModule pgSfxModule, Vector3 position, float delayDuration, Transform parent, bool isGlobalPosition)
        {
            return pgSfxModule.SfxManager.Play(this, position, delayDuration, parent, isGlobalPosition);
        }

        public override Coroutine Play(PgSfxModule pgSfxModule, Vector3 position, float delayDuration, float volume, Transform parent,
            bool isGlobalPosition)
        {
            return pgSfxModule.SfxManager.Play(this, position, delayDuration, volume, parent, isGlobalPosition);
        }

        public override Coroutine Play(PgSfxModule pgSfxModule, Vector3 position, float delayDuration, float volume, float pitch,
            Transform parent, bool isGlobalPosition)
        {
            return pgSfxModule.SfxManager.Play(this, position, delayDuration, volume, pitch, parent, isGlobalPosition);
        }
        
        public override Coroutine PlaySuspended(PgSfxModule pgSfxModule, float delayDuration, float volume, float pitch)
        {
            return pgSfxModule.SfxManager.PlaySuspended(this, delayDuration, volume, pitch);
        }
        
        public override Coroutine PlaySuspended(PgSfxModule pgSfxModule, Vector3 position, float delayDuration, float volume, float pitch,
            Transform parent, bool isGlobalPosition)
        {
            return pgSfxModule.SfxManager.PlaySuspended(this, position, delayDuration, volume, pitch, parent, isGlobalPosition);
        }
    }
}