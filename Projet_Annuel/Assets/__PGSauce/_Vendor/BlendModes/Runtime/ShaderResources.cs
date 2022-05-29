// Copyright 2014-2019 Elringus (Artyom Sovetnikov). All Rights Reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BlendModes
{
    /// <summary>
    /// Used to store references to the shaders we might need at runtime.
    /// </summary>
    public sealed class ShaderResources : ScriptableObject
    {
        [Tooltip("Shaders currently included in the build.")]
        [SerializeField] private List<Shader> shaders = new List<Shader>();
        [Tooltip("Additional shader paths to use when resolving available shader extensions. Should be relative to the project root, eg: `Assets/Shaders`.")]
        [SerializeField] private List<string> additionalPaths = new List<string>();

        private const string resourcesAssetPath = "BlendModes/ShaderResources";
        // Caches are required to prevent constant GC allocations when accessing shader.name and building strings.
        private static readonly Dictionary<string, int> cachedShaderNames = new Dictionary<string, int>(StringComparer.Ordinal);
        private static readonly HashSet<string> cachedShaderFamilies = new HashSet<string>(StringComparer.Ordinal);

        /// <summary>
        /// Loads a <see cref="ShaderResources"/> asset instance from the project resources.
        /// </summary>
        public static ShaderResources Load ()
        {
            var resources = Resources.Load<ShaderResources>(resourcesAssetPath);
            if (!resources) return null;
            if (cachedShaderNames.Count == 0) 
                resources.RebuildShaderNamesCache();
            if (cachedShaderFamilies.Count == 0) 
                resources.RebuildShaderFamiliesCache();
            return resources;
        }

        /// <summary>
        /// Asynchronously loads a <see cref="ShaderResources"/> asset instance from the project resources.
        /// </summary>
        public static ResourceRequest LoadAsync ()
        {
            return Resources.LoadAsync<ShaderResources>(resourcesAssetPath);
        }

        /// <summary>
        /// Returns unique available shaders. 
        /// </summary>
        public IEnumerable<Shader> GetShaders ()
        {
            return new HashSet<Shader>(shaders);
        }

        /// <summary>
        /// Resolves a shader by it's name (when available).
        /// </summary>
        public Shader GetShaderByName (string shaderName)
        {
            return cachedShaderNames.TryGetValue(shaderName, out var index) ? shaders[index] : null;
        }

        /// <summary>
        /// Whether a shader with the provided name is available.
        /// </summary>
        public bool ShaderExists (string shaderName)
        {
            return cachedShaderNames.ContainsKey(shaderName);
        }

        /// <summary>
        /// Returns available shader families. 
        /// </summary>
        public IEnumerable<string> GetShaderFamilies () => cachedShaderFamilies;

        /// <summary>
        /// Whether a shader family with the provided name has a grab variant (supports <see cref="RenderMode.SelfWithScreen"/>).
        /// </summary>
        public bool FamilyImplementsGrab (string shaderFamily)
        {
            return shaders.Exists(s => GetShaderFamily(s.name) == shaderFamily && GetShaderVariant(s.name) == "Grab");
        }

        /// <summary>
        /// Whether a shader family with the provided name has an overlay variant (supports <see cref="RenderMode.TextureWithSelf"/>).
        /// </summary>
        public bool FamilyImplementsOverlay (string shaderFamily)
        {
            return shaders.Exists(s => GetShaderFamily(s.name) == shaderFamily && GetShaderVariant(s.name) == "Overlay");
        }

        /// <summary>
        /// Whether a shader family with the provided name supports <see cref="BlendModeEffect.UnifiedGrabEnabled"/> optimization.
        /// </summary>
        public bool FamilyImplementsUnifiedGrab (string shaderFamily)
        {
            return shaders.Exists(s => GetShaderFamily(s.name) == shaderFamily && GetShaderVariant(s.name) == "UnifiedGrab");
        }

        /// <summary>
        /// Whether a shader family with the provided name supports <see cref="BlendModeEffect.FramebufferEnabled"/> optimization.
        /// </summary>
        public bool FamilyImplementsFramebuffer (string shaderFamily)
        {
            return shaders.Exists(s => GetShaderFamily(s.name) == shaderFamily && GetShaderVariant(s.name) == "Framebuffer");
        }

        /// <summary>
        /// Whether a shader family with the provided name supports masking feature.
        /// </summary>
        public bool FamilyImplementsMasking (string shaderFamily)
        {
            return shaders.Exists(s => GetShaderFamily(s.name) == shaderFamily && GetShaderVariant(s.name).EndsWith("Masked"));
        }

        public void AddShader (Shader shader)
        {
            if (shaders.Exists(s => s.name == shader.name)) return;
            shaders.Add(shader);
            RebuildShaderNamesCache();
            RebuildShaderFamiliesCache();
        }

        public void RemoveShader (Shader shader)
        {
            shaders.RemoveAll(s => s.name == shader.name);
            RebuildShaderNamesCache();
            RebuildShaderFamiliesCache();
        }

        public void RemoveAllShaders ()
        {
            shaders.Clear();
            RebuildShaderNamesCache();
            RebuildShaderFamiliesCache();
        }

        public IEnumerable<string> GetAdditionalPaths ()
        {
            return additionalPaths;
        }

        private static string GetShaderFamily (string shaderName)
        {
            return shaderName.Split('/')[2];
        }

        private static string GetShaderVariant (string shaderName)
        {
            return shaderName.Split('/')[3]; 
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void PreloadResources () => Load();

        private void RebuildShaderNamesCache ()
        {
            cachedShaderNames.Clear();
            for (int i = 0; i < shaders.Count; i++)
                cachedShaderNames[shaders[i].name] = i;
        }
        
        private void RebuildShaderFamiliesCache ()
        {
            var shaderFamilies = GetShaders().Select(s => GetShaderFamily(s.name));
            cachedShaderFamilies.Clear();
            cachedShaderFamilies.UnionWith(shaderFamilies);
        }
    }
}
