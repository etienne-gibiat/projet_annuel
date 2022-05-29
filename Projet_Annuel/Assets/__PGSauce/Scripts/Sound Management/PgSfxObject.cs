using PGSauce.AudioManagement.External;
using PGSauce.Core.Strings;
using UnityEngine;

namespace PGSauce.AudioManagement
{
    /// <summary>A full sound effect comprised of multiple layers, each containing their own AudioClip and customised randomisation.</summary>
    [CreateAssetMenu(fileName = "SFX", menuName = MenuPaths.Audio + "SFX Object", order = 3)]
    public class PgSfxObject : SFXObject
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