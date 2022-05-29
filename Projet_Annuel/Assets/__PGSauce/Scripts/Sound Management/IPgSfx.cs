using System;
using UnityEngine;

namespace PGSauce.AudioManagement
{
    /// <summary>
    /// Represents a SFX object
    /// </summary>
    public interface IPgSfx
    {
        Coroutine Play(PgSfxModule pgSfxModule);
        Coroutine Play(PgSfxModule pgSfxModule, float delayDuration);
        Coroutine Play(PgSfxModule pgSfxModule, float delayDuration, float volume);
        Coroutine Play(PgSfxModule pgSfxModule, float delayDuration, float volume, float pitch);
        Coroutine Play(PgSfxModule pgSfxModule, Vector3 position, Transform parent, bool isGlobalPosition);
        Coroutine Play(PgSfxModule pgSfxModule, Vector3 position, float delayDuration, Transform parent, bool isGlobalPosition);
        Coroutine Play(PgSfxModule pgSfxModule, Vector3 position, float delayDuration, float volume, Transform parent, bool isGlobalPosition);
        Coroutine Play(PgSfxModule pgSfxModule, Vector3 position, float delayDuration, float volume, float pitch, Transform parent, bool isGlobalPosition);
        Coroutine PlaySuspended(PgSfxModule pgSfxModule, float delayDuration, float volume, float pitch);
        Coroutine PlaySuspended(PgSfxModule pgSfxModule, Vector3 position, float delayDuration, float volume, float pitch, Transform parent, bool isGlobalPosition);
    }
}