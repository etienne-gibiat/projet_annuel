using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
public class GenerateNature : MonoBehaviour
{
    public const int mapChunkSize = 239;
    public static void GenerateNatureWithHeight(bool useFalloff, int seed, float noiseScale, int octaves, float persistance, float lacunarity, Vector2 center, Vector2 offset, Noise.NormalizeMode normalizeMode, float[,] falloffMap, TerrainType[] regions, AnimationCurve meshHeightCurve, float meshHeightMultiplier, GameObject parentPrefab)
    {
        float[,] noiseMap;
        noiseMap = Noise.GenerateNoiseMap(mapChunkSize + 2, mapChunkSize + 2, seed, noiseScale, octaves, persistance, lacunarity, center + offset, normalizeMode);
        int nbPrefab;
        int prefabRand;
        parentPrefab.transform.position = new Vector3(0, 0, 0);

        if (useFalloff)
        {
            if (falloffMap == null)
            {
                falloffMap = FalloffGenerator.GenerateFalloffMap(mapChunkSize + 2);
            }

            for (int y = 0; y < mapChunkSize + 2; y++)
            {
                for (int x = 0; x < mapChunkSize + 2; x++)
                {
                    if (useFalloff)
                    {
                        noiseMap[x, y] = Mathf.Clamp01(noiseMap[x, y] - falloffMap[x, y]);
                    }

                    if (y < mapChunkSize && x < mapChunkSize)
                    {
                        float currentHeight = noiseMap[x, y];
                        if (currentHeight >= 0.28)
                        {
                            for (int i = 2; i < regions.Length; i++)
                            {
                                if (currentHeight >= regions[i].height)
                                {
                                    float height = meshHeightCurve.Evaluate(currentHeight) * meshHeightMultiplier;

                                    int rand = UnityEngine.Random.Range(0, 100);
                                    if (rand == 0)
                                    {
                                        Vector3 position = new Vector3(x * 5, height * 5, -y * 5);
                                        nbPrefab = regions[i].prefab.Length;
                                        prefabRand = UnityEngine.Random.Range(0, nbPrefab);
                                        if (nbPrefab != 0)
                                        {
                                            GameObject prefab = Instantiate(regions[i].prefab[prefabRand], position, Quaternion.identity);
                                            prefab.transform.parent = parentPrefab.transform;
                                        }
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            parentPrefab.transform.position = new Vector3(-600, 0, 600);
        }
    }

    public static void GenerateGrassWithHeight(bool useFalloff, int seed, float noiseScale, int octaves, float persistance, float lacunarity, Vector2 center, Vector2 offset, Noise.NormalizeMode normalizeMode, float[,] falloffMap, TerrainType[] regions, AnimationCurve meshHeightCurve, float meshHeightMultiplier, GameObject parentPrefab)
    {
        float[,] noiseMap;
        noiseMap = Noise.GenerateNoiseMap(mapChunkSize + 2, mapChunkSize + 2, seed, noiseScale, octaves, persistance, lacunarity, center + offset, normalizeMode);
        int nbPrefab;
        int prefabRand;
        parentPrefab.transform.position = new Vector3(0, 0, 0);

        if (useFalloff)
        {
            if (falloffMap == null)
            {
                falloffMap = FalloffGenerator.GenerateFalloffMap(mapChunkSize + 2);
            }

            for (int y = 0; y < mapChunkSize + 2; y++)
            {
                for (int x = 0; x < mapChunkSize + 2; x++)
                {
                    if (useFalloff)
                    {
                        noiseMap[x, y] = Mathf.Clamp01(noiseMap[x, y] - falloffMap[x, y]);
                    }

                    if (y < mapChunkSize && x < mapChunkSize)
                    {
                        float currentHeight = noiseMap[x, y];

                        for (int i = 2; i < regions.Length; i++)
                        {
                            if (currentHeight >= 0.38 && currentHeight < 0.55)
                            {
                                //Debug.Log(regions[i].name);
                                float height = meshHeightCurve.Evaluate(currentHeight) * meshHeightMultiplier;

                                int rand = UnityEngine.Random.Range(0, 15);
                                if (rand == 0)
                                {
                                    if (regions[i].name == "Grass")
                                    {
                                        Vector3 position = new Vector3(x * 5, height * 5, -y * 5);
                                        nbPrefab = regions[i].prefab.Length;
                                        prefabRand = UnityEngine.Random.Range(0, nbPrefab);
                                        if (nbPrefab != 0)
                                        {
                                            GameObject prefab = Instantiate(regions[i].prefab[prefabRand], position, Quaternion.identity);
                                            prefab.transform.parent = parentPrefab.transform;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                break;
                            }

                        }
                    }
                }
            }
            parentPrefab.transform.position = new Vector3(-600, 0, 600);
        }
    }

    public static void GenerateGrass2WithHeight(bool useFalloff, int seed, float noiseScale, int octaves, float persistance, float lacunarity, Vector2 center, Vector2 offset, Noise.NormalizeMode normalizeMode, float[,] falloffMap, TerrainType[] regions, AnimationCurve meshHeightCurve, float meshHeightMultiplier, GameObject parentPrefab)
    {
        float[,] noiseMap;
        noiseMap = Noise.GenerateNoiseMap(mapChunkSize + 2, mapChunkSize + 2, seed, noiseScale, octaves, persistance, lacunarity, center + offset, normalizeMode);
        int nbPrefab;
        int prefabRand;
        parentPrefab.transform.position = new Vector3(0, 0, 0);

        if (useFalloff)
        {
            if (falloffMap == null)
            {
                falloffMap = FalloffGenerator.GenerateFalloffMap(mapChunkSize + 2);
            }

            for (int y = 0; y < mapChunkSize + 2; y++)
            {
                for (int x = 0; x < mapChunkSize + 2; x++)
                {
                    if (useFalloff)
                    {
                        noiseMap[x, y] = Mathf.Clamp01(noiseMap[x, y] - falloffMap[x, y]);
                    }

                    if (y < mapChunkSize && x < mapChunkSize)
                    {
                        float currentHeight = noiseMap[x, y];

                        for (int i = 2; i < regions.Length; i++)
                        {
                            if (currentHeight >= 0.55 && currentHeight < 0.7)
                            {
                                //Debug.Log(regions[i].name);
                                float height = meshHeightCurve.Evaluate(currentHeight) * meshHeightMultiplier;

                                int rand = UnityEngine.Random.Range(0, 5);
                                if (rand == 0)
                                {
                                    if (regions[i].name == "Grass 2")
                                    {
                                        Vector3 position = new Vector3(x * 5, height * 5, -y * 5);
                                        nbPrefab = regions[i].prefab.Length;
                                        prefabRand = UnityEngine.Random.Range(0, nbPrefab);
                                        if (nbPrefab != 0)
                                        {
                                            GameObject prefab = Instantiate(regions[i].prefab[prefabRand], position, Quaternion.identity);
                                            prefab.transform.parent = parentPrefab.transform;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                break;
                            }

                        }
                    }
                }
            }
            parentPrefab.transform.position = new Vector3(-600, 0, 600);
        }
    }


    public static void GenerateSandWithHeight(bool useFalloff, int seed, float noiseScale, int octaves, float persistance, float lacunarity, Vector2 center, Vector2 offset, Noise.NormalizeMode normalizeMode, float[,] falloffMap, TerrainType[] regions, AnimationCurve meshHeightCurve, float meshHeightMultiplier, GameObject parentPrefab)
    {
        float[,] noiseMap;
        noiseMap = Noise.GenerateNoiseMap(mapChunkSize + 2, mapChunkSize + 2, seed, noiseScale, octaves, persistance, lacunarity, center + offset, normalizeMode);
        int nbPrefab;
        int prefabRand;
        parentPrefab.transform.position = new Vector3(0, 0, 0);


        if (useFalloff)
        {
            if (falloffMap == null)
            {
                falloffMap = FalloffGenerator.GenerateFalloffMap(mapChunkSize + 2);
            }

            for (int y = 0; y < mapChunkSize + 2; y++)
            {
                for (int x = 0; x < mapChunkSize + 2; x++)
                {
                    if (useFalloff)
                    {
                        noiseMap[x, y] = Mathf.Clamp01(noiseMap[x, y] - falloffMap[x, y]);
                    }

                    if (y < mapChunkSize && x < mapChunkSize)
                    {
                        float currentHeight = noiseMap[x, y];

                        for (int i = 2; i < regions.Length; i++)
                        {
                            if (currentHeight >= 0.28 && currentHeight < 0.38)
                            {
                                //Debug.Log(regions[i].name);
                                float height = meshHeightCurve.Evaluate(currentHeight) * meshHeightMultiplier;

                                int rand = UnityEngine.Random.Range(0, 20);
                                if (rand == 0)
                                {
                                    if (regions[i].name == "Sand")
                                    {
                                        Vector3 position = new Vector3(x * 5, height * 5, -y * 5);
                                        nbPrefab = regions[i].prefab.Length;
                                        prefabRand = UnityEngine.Random.Range(0, nbPrefab);
                                        if (nbPrefab != 0)
                                        {
                                            GameObject prefab = Instantiate(regions[i].prefab[prefabRand], position, Quaternion.identity);
                                            prefab.transform.parent = parentPrefab.transform;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                break;
                            }

                        }
                    }
                }
            }
            parentPrefab.transform.position = new Vector3(-600, 0, 600);
        }
    }

    public static void GenerateTextureMesh(bool useFalloff, int seed, float noiseScale, int octaves, float persistance, float lacunarity, Vector2 center, Vector2 offset, Noise.NormalizeMode normalizeMode, float[,] falloffMap, TerrainType[] regions, AnimationCurve meshHeightCurve, float meshHeightMultiplier, GameObject parentPrefab, Texture2D[] main_texture_tab, Renderer main_renderer)
    {
        Texture2DArray textureArray = new Texture2DArray(mapChunkSize, mapChunkSize, main_texture_tab.Length, TextureFormat.RGB565, true);
        
        for(int i = 0; i < main_texture_tab.Length; i++)
        {
            textureArray.SetPixels(main_texture_tab[i].GetPixels(), i);
        }
        textureArray.Apply();
        

        float[,] noiseMap;
        noiseMap = Noise.GenerateNoiseMap(mapChunkSize + 2, mapChunkSize + 2, seed, noiseScale, octaves, persistance, lacunarity, center + offset, normalizeMode);
        int nbPrefab;
        int prefabRand;
        parentPrefab.transform.position = new Vector3(0, 0, 0);

        if (useFalloff)
        {
            if (falloffMap == null)
            {
                falloffMap = FalloffGenerator.GenerateFalloffMap(mapChunkSize + 2);
            }

            Color32[] image = new Color32[mapChunkSize * mapChunkSize];


            for (int y = 0; y < mapChunkSize + 2; y++)
            {
                for (int x = 0; x < mapChunkSize + 2; x++)
                {
                    if (useFalloff)
                    {
                        noiseMap[x, y] = Mathf.Clamp01(noiseMap[x, y] - falloffMap[x, y]);
                    }

                    if (y < mapChunkSize && x < mapChunkSize)
                    {
                        float currentHeight = noiseMap[x, y];

                        int imageIndex = y * mapChunkSize + x;
/*
                        image[imageIndex].r =                          (byte)(((main_texture_tab[0].GetPixel(y, x).r * 255.0f) + (main_texture_tab[0].GetPixel(y, x).g * 255.0f) + (main_texture_tab[0].GetPixel(y, x).b * 255.0f)) / 3.0);
                        image[imageIndex].g = (currentHeight > 0.38) ? (byte)(((main_texture_tab[1].GetPixel(y, x).r * 255.0f) + (main_texture_tab[1].GetPixel(y, x).g * 255.0f) + (main_texture_tab[1].GetPixel(y, x).b * 255.0f)) / 3.0) : (byte)0;
                        image[imageIndex].b = (currentHeight > 0.55) ? (byte)(((main_texture_tab[2].GetPixel(y, x).r * 255.0f) + (main_texture_tab[2].GetPixel(y, x).g * 255.0f) + (main_texture_tab[2].GetPixel(y, x).b * 255.0f)) / 3.0) : (byte)0;
                        image[imageIndex].a = (currentHeight > 0.7) ? (byte)(((main_texture_tab[3].GetPixel(y, x).r * 255.0f) + (main_texture_tab[3].GetPixel(y, x).g * 255.0f) + (main_texture_tab[3].GetPixel(y, x).b * 255.0f)) / 3.0) : (byte)0;
*/
                        image[imageIndex] = main_texture_tab[0].GetPixel(y, x);
                        
                        if (currentHeight > 0.38)
                        {
                            image[imageIndex] = main_texture_tab[1].GetPixel(y, x);
                        }
                        if (currentHeight > 0.55)
                        {
                            image[imageIndex] = main_texture_tab[2].GetPixel(y, x);
                        }
                        if (currentHeight > 0.7)
                        {
                            image[imageIndex] = main_texture_tab[3].GetPixel(y, x);
                        }


                        /*for (int i = 2; i < regions.Length; i++)
                        {
                            if (currentHeight >= 0.28 && currentHeight < 0.38)
                            {
                                //Debug.Log(regions[i].name);
                                float height = meshHeightCurve.Evaluate(currentHeight) * meshHeightMultiplier;

                                Vector3 position = new Vector3(x * 5, height * 5, -y * 5);



                            }
                            else
                            {
                                break;
                            }

                         }*/
                    }
                }
            }
            string dataPath = "Assets/Dossier Guillaume/texture_test.asset";
            Texture2D finalSplatTexture = new Texture2D(mapChunkSize, mapChunkSize);
            finalSplatTexture.SetPixels32(image);
            finalSplatTexture.Apply();
            byte[] res = finalSplatTexture.EncodeToPNG();
            File.WriteAllBytes("Assets/Dossier Guillaume" + " / exported_texture.png", res);
            //AssetDatabase.CreateAsset(finalSplatTexture, dataPath);
            parentPrefab.transform.position = new Vector3(-600, 0, 600);
        }
    }
}
