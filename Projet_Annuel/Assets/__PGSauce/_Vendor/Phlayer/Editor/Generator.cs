﻿using Microsoft.CSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using PGSauce.Core.Strings;
using UnityEditor;
using UnityEngine;

namespace JesseStiller.PhlayerTool {
    public class Generator : AssetPostprocessor {
        private const string windowsLineEnding = "\r\n";
        private const string unixLineEnding = "\n";
        private const string header = "// Auto-generated based on the TagManager settings by Jesse Stiller's Phlayer Unity extension.";
        private static readonly StringBuilder sb = new StringBuilder(1024);
        private static readonly StringBuilder auxSB = new StringBuilder(64); // An auxillary string builder
        private static readonly string[] indentatorsArray = { " ", "  ", "   ", "    ", "\t" };
        private static byte indentation;
        private static string indentator;

        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths) {
            foreach(string str in importedAssets) {
                if(str.Equals("ProjectSettings/TagManager.asset", StringComparison.OrdinalIgnoreCase) == false) continue;
                GenerateAndSave();
            }
        }

        [MenuItem(MenuPaths.Vendor + "Phlayer/Force Class Generation")]
        internal static void GenerateAndSave() {
            if(Phlayer.InitializeSettings() == false) {
                if(Phlayer.errorState == SettingsError.NoDirectory) {
                    Debug.LogError("Phlayer could not generate the layers class automatically because it could not find its main directory. Ensure Phlayer's main directory hasn't been renamed.");
                } else if(Phlayer.errorState == SettingsError.NoValidFile) {
                    Debug.LogError("Phlayer could not generate the layers class automatically because it could not find any scripts in its main directory. Reimport the Phlayer package.");
                }
                return;
            }

            if(Path.IsPathRooted(Phlayer.settings.localOutputDirectory)) {
                EditorUtility.DisplayDialog("Generate Script", "The output directory must be relative to the current Unity project's Assets directory.", "Close");
                return;
            }

            Generate(preview: false);

            string absoluteFilePath = GetAbsolutePathFromLocalPath(GetLocalPath());
            string absoluteDirectory = Path.GetDirectoryName(absoluteFilePath);

            // Validation of path
            try {
                new FileInfo(absoluteFilePath);

                if(Directory.Exists(absoluteDirectory) == false) {
                    Directory.CreateDirectory(absoluteDirectory);
                }
            } catch(Exception e) {
                EditorUtility.DisplayDialog("Generate Script", string.Format("The output filepath is invalid for the following reason: {0}", e.ToString()), "Close");
                return;
            }

            // Make sure that we are writing to one of our own files if already present, and not something created by a anyone/anything else.
            if(File.Exists(absoluteFilePath)) {
                using(StreamReader sr = new StreamReader(absoluteFilePath)) {
                    if(sr.Peek() != -1 && sr.ReadLine().StartsWith(header, StringComparison.Ordinal) == false) {
                        bool overwriteAnyway = EditorUtility.DisplayDialog("Generate Script", 
                            "The file that is going to be overwritten does not appear to have been previously generated by Phlayer.\n\nDo you want to overwrite it anyway?", "Overwrite Anyway", "Cancel");
                        if(overwriteAnyway == false) return;
                    }
                }
            }

            bool fileWritten = false;
            try {
                File.WriteAllText(absoluteFilePath, sb.ToString());
                fileWritten = true;
            } catch(Exception e) {
                Debug.LogError(string.Format("Phlayer was not able to save {0} for the following reason:\n{1}", absoluteFilePath, e.ToString()));
            }
            if(fileWritten == false) return;
            AssetDatabase.ImportAsset("Assets/" + GetLocalPath(), ImportAssetOptions.ForceUpdate);
        }

        internal static string GetLocalPath() {
            string className = string.IsNullOrEmpty(Phlayer.settings.className) ? "Layers" : Phlayer.settings.className;
            string extension = Phlayer.settings.appendDotGInFileName ? ".g.cs" : ".cs";
            return Path.Combine(Phlayer.settings.localOutputDirectory, className + extension).Replace('\\', '/');
        }

        private static void Generate(bool preview) {
            // Don't display the tabs in the preview because they are way too wide and seemingly can't be shrinked.
            if(preview && Phlayer.settings.indentationStyle == IndentationStyle.Tabs) {
                indentator = indentatorsArray[(byte)IndentationStyle.FourSpaces];
            } else {
                indentator = indentatorsArray[(byte)Phlayer.settings.indentationStyle];
            }

            // Reset state
            sb.Length = 0;
            indentation = 0;

            if(preview == false) { 
                AppendLine(header);
            }

            // Namespace
            if(string.IsNullOrEmpty(Phlayer.settings.classNamespace) == false) {
                AppendLineWithCurlyBracket("namespace " + Phlayer.settings.classNamespace);
            }

            // Class declaration
            string className = string.IsNullOrEmpty(Phlayer.settings.className) ? "Layers" : Phlayer.settings.className;
            AppendLineWithCurlyBracket("public static class " + className);

            CSharpCodeProvider codeProvider = new CSharpCodeProvider();

            HashSet<string> layers = new HashSet<string>();

            for(int i = Phlayer.settings.skipDefaultLayers ? 8 : 0; i < 32; i++) {
                string layerName = LayerMask.LayerToName(i);
                if(IsNullOrWhitespace(layerName)) continue;

                auxSB.Length = 0;

                if(Phlayer.settings.casing == Casing.LeaveAsIs) {
                    auxSB.Append(Utilities.ConvertToValidIdentifier(layerName));
                } else {
                    bool newWord = false;
                    for(int c = 0; c < layerName.Length; c++) {
                        if(layerName[c] == ' ') {
                            if(Phlayer.settings.casing == Casing.CapsLockWithUnderscores) {
                                auxSB.Append('_');
                            } else {
                                newWord = true;
                            }
                        } else if(Utilities.IsCharValidForIdentifier(layerName[c]) == false) {
                            auxSB.Append('_');
                        } else if(newWord) {
                            auxSB.Append(char.ToUpperInvariant(layerName[c]));
                            newWord = false;
                        } else if(Phlayer.settings.casing == Casing.CapsLock || Phlayer.settings.casing == Casing.CapsLockWithUnderscores) {
                            auxSB.Append(char.ToUpperInvariant(layerName[c]));
                        } else {
                            auxSB.Append(layerName[c]);
                        }
                    }

                    if(Phlayer.settings.casing == Casing.Camel) {
                        for(int c = 0; c < auxSB.Length; c++) {
                            if(char.IsLetter(auxSB[c]) == false) continue;
                            auxSB[c] = char.ToLowerInvariant(auxSB[c]);
                            break;
                        }
                    }
                }

                if(char.IsDigit(auxSB[0])) {
                    auxSB.Insert(0, '_');
                }

                if(char.IsDigit(auxSB[0])) {
                    auxSB.Insert(0, "_");
                }
                
                if(codeProvider.IsValidIdentifier(auxSB.ToString()) == false) {
                    if(Phlayer.settings.escapeIdentifiersWithAtSymbol) {
                        auxSB.Insert(0, '@');
                    } else {
                        auxSB.Insert(0, '_');
                    }
                }

                /**
                * Make sure there aren't any duplicates.
                * The while loop can only happen up to ~23 times in the worse case (since there are 8 built-in layers which can't be modified)
                */
                if(layers.Contains(auxSB.ToString())) {
                    int count = 2;
                    while(layers.Contains(auxSB.ToString() + count.ToString())) {
                        count++;
                    }
                    auxSB.Append(count);
                }
                layers.Add(auxSB.ToString());

                AppendLine($"public const int {auxSB.ToString()} = {i};");
            }

            // Write all ending curly brakets
            while(indentation-- > 0) {
                if(indentation == 0) Append("}");
                                else AppendLine("}");
            }
        }

        private static bool IsNullOrWhitespace(string value)
        {
            return value == null || value.All(char.IsWhiteSpace);
        }

        internal static string GetPreview() {
            Settings previewSettings = Phlayer.settings.Clone();
            previewSettings.indentationStyle = IndentationStyle.FourSpaces;

            Generate(preview: true);
            return sb.ToString();
        }

        private static void AppendLineWithCurlyBracket(string v) {
            Append(v);
            if(Phlayer.settings.curlyBracketOnNewLine) {
                AppendLine("");
                AppendLine("{");
            } else {
                sb.Append(" {");
                AppendLine("");
            }

            indentation++;
        }

        private static void Append(string s) {
            for(byte i = 0; i < indentation; i++) {
                sb.Append(indentator);
            }
            sb.Append(s);
        }
        
        private static void AppendLine(string s) {
            Append(s);
            if(Phlayer.settings.windowsStyleLineEndings) {
                sb.Append(windowsLineEnding);
            } else {
                sb.Append(unixLineEnding);
            }
        }

        private static string GetAbsolutePathFromLocalPath(string localPath) {
            return Path.Combine(Application.dataPath, localPath);
        }
    }
}
