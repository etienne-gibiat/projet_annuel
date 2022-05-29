using PGSauce.AudioManagement;
using UnityEngine;

namespace PGSauce.AudioManagement
{
    /// <summary>
    /// Base class to SFX Objects and Groups.
    /// Can be referenced in Editor.
    /// A Scriptable Object.
    /// </summary>
    public abstract class PgSfxBase : ScriptableObject, IPgSfx
    {
        public abstract Coroutine Play(PgSfxModule pgSfxModule);
        public abstract Coroutine Play(PgSfxModule pgSfxModule, float delayDuration);
        public abstract Coroutine Play(PgSfxModule pgSfxModule, float delayDuration, float volume);
        public abstract Coroutine Play(PgSfxModule pgSfxModule, float delayDuration, float volume, float pitch);
        public abstract Coroutine Play(PgSfxModule pgSfxModule, Vector3 position, Transform parent, bool isGlobalPosition);
        public abstract Coroutine Play(PgSfxModule pgSfxModule, Vector3 position, float delayDuration, Transform parent, bool isGlobalPosition);

        public abstract Coroutine Play(PgSfxModule pgSfxModule, Vector3 position, float delayDuration, float volume, Transform parent,
            bool isGlobalPosition);

        public abstract Coroutine Play(PgSfxModule pgSfxModule, Vector3 position, float delayDuration, float volume, float pitch,
            Transform parent, bool isGlobalPosition);

        public abstract Coroutine PlaySuspended(PgSfxModule pgSfxModule, float delayDuration, float volume, float pitch);

        public abstract Coroutine PlaySuspended(PgSfxModule pgSfxModule, Vector3 position, float delayDuration, float volume, float pitch,
            Transform parent, bool isGlobalPosition);
    }
}