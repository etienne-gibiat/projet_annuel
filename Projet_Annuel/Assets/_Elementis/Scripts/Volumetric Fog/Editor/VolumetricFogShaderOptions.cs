using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;

namespace VolumetricFogAndMist2 {

    public class VolumetricFogShaderOptions {

        const string SHADER_NAME = "VolumetricFog2/VolumetricFog2DURP";
        const string OPTIONS_SHADER_FILENAME = "CommonsURP.hlsl";
        const string OPTIONS_SCRIPT_FILENAME = "VolumetricFogManager.cs";

        public bool pendingChanges;
        public ShaderAdvancedOption[] options;

        public void ReadOptions() {
            pendingChanges = false;
            // Populate known options
            options = new ShaderAdvancedOption[]
            {
                new ShaderAdvancedOption
                {
                    id = "ORTHO_SUPPORT", name = "Orthographic Mode", description = "Enables support for orthographic camera projection."
                },
                new ShaderAdvancedOption
                {
                    id = "USE_ALTERNATE_RECONSTRUCT_API",
                    name = "Alternate WS Reconstruction",
                    description = "Uses an alternate world space position reconstruction in XR."
                },
                new ShaderAdvancedOption
                {
                    id = "FOG_BLUE_NOISE",
                    name = "Blue Noise",
                    description = "Enables blue noise based dithering/jittering."
                },
                new ShaderAdvancedOption
                {
                    id = "USE_WORLD_SPACE_NOISE",
                    name = "World Space Noise",
                    description = "Uses world-space aligned noise (noise will change when fog volume position changes)."
                },
                new ShaderAdvancedOption
                {
                    id = "WEBGL_COMPATIBILITY_MODE",
                    name = "WebGL Compatibilty Mode",
                    description = "Enable this option only if you're building for WebGL and notice rendering issues."
                },
                new ShaderAdvancedOption
                {
                    id = "FOG_VOID_ROTATION",
                    name = "Fog Voids Rotation",
                    description = "Enable this option to allow rotation of fog voids using the transform."
                },
                new ShaderAdvancedOption
                {
                    id = "MAX_ITERATIONS",
                    name = "",
                    description = "",
                    hasValue = true
                }
            };


            Shader shader = Shader.Find(SHADER_NAME);
            if (shader != null) {
                string path = AssetDatabase.GetAssetPath(shader);
                string file = Path.GetDirectoryName(path) + "/" + OPTIONS_SHADER_FILENAME;
                string[] lines = File.ReadAllLines(file, Encoding.UTF8);
                for (int k = 0; k < lines.Length; k++) {
                    for (int o = 0; o < options.Length; o++) {
                        if (lines[k].Contains("#define " + options[o].id)) {
                            options[o].enabled = !lines[k].StartsWith("//");
                            if (options[o].hasValue) {
                                string[] tokens = lines[k].Split(null);
                                if (tokens.Length > 2) {
                                    int.TryParse(tokens[2], out options[o].value);
                                }
                            }
                            break;
                        }
                    }
                }
            }
        }


        public bool GetAdvancedOptionState(string optionId) {
            if (options == null)
                return false;
            for (int k = 0; k < options.Length; k++) {
                if (options[k].id.Equals(optionId)) {
                    return options[k].enabled;
                }
            }
            return false;
        }

        public void UpdateAdvancedOptionsFile() {
            // Reloads the file and updates it accordingly
            Shader shader = Shader.Find(SHADER_NAME);
            if (shader != null) {
                string path = AssetDatabase.GetAssetPath(shader);

                // update shader options
                string file = Path.GetDirectoryName(path) + "/" + OPTIONS_SHADER_FILENAME;
                string[] lines = File.ReadAllLines(file, Encoding.UTF8);
                for (int k = 0; k < lines.Length; k++) {
                    for (int o = 0; o < options.Length; o++) {
                        string token = "#define " + options[o].id;
                        if (lines[k].Contains(token)) {
                            if (options[o].hasValue) {
                                lines[k] = token + " " + options[o].value;
                            } else {
                                if (options[o].enabled) {
                                    lines[k] = token;
                                } else {
                                    lines[k] = "//#define " + options[o].id;
                                }
                            }
                            break;
                        }
                    }
                }
                File.WriteAllLines(file, lines, Encoding.UTF8);

                // update manager options
                file = Path.GetDirectoryName(path) + "/../../Scripts/Managers/" + OPTIONS_SCRIPT_FILENAME;
                lines = File.ReadAllLines(file, Encoding.UTF8);
                for (int k = 0; k < lines.Length; k++) {
                    for (int o = 0; o < options.Length; o++) {
                        string token = "#define " + options[o].id;
                        if (lines[k].Contains(token)) {
                            if (options[o].hasValue) {
                                lines[k] = token + " " + options[o].value;
                            } else {
                                if (options[o].enabled) {
                                    lines[k] = token;
                                } else {
                                    lines[k] = "//#define " + options[o].id;
                                }
                            }
                            break;
                        }
                    }
                }
                File.WriteAllLines(file, lines, Encoding.UTF8);

            }

            pendingChanges = false;
            AssetDatabase.Refresh();
        }

        public int GetOptionValue(string id) {
            for (int k = 0; k < options.Length; k++) {
                if (options[k].hasValue && options[k].id.Equals(id)) {
                    return options[k].value;
                }
            }
            return 0;
        }

        public void SetOptionValue(string id, int value) {
            for (int k = 0; k < options.Length; k++) {
                if (options[k].hasValue && options[k].id.Equals(id)) {
                    options[k].value = value;
                }
            }
        }


    }

    public struct ShaderAdvancedOption {
        public string id;
        public string name;
        public string description;
        public bool enabled;
        public bool hasValue;
        public int value;
    }


}