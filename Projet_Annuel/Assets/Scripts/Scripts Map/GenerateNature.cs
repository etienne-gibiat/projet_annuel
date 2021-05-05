using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

                                    int rand = UnityEngine.Random.Range(0, 200);
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

                                int rand = UnityEngine.Random.Range(0, 2);
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

                                int rand = UnityEngine.Random.Range(0, 100);
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
}
