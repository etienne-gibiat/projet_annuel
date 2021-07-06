using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;

public class MapGenerator : MonoBehaviour
{
    public enum DrawMode { NoiseMap, ColorMap, Mesh, FalloffMap };
    public DrawMode drawMode;

    public Noise.NormalizeMode normalizeMode;

    public const int mapChunkSize = 239;
    [Range(0,6)]
    public int editorPreviewLOD;
    public float noiseScale;

    public int octaves;
    [Range(0, 1)]
    public float persistance;
    public float lacunarity;

    public int seed;
    public Vector2 offset;

    public bool useFalloff;

    public float meshHeightMultiplier;
    public AnimationCurve meshHeightCurve;

    public bool autoUpdate;

    public TerrainType[] regions;

    float[,] falloffMap;

    Queue<MapThreadInfo<MapData>> mapDataThreadInfoQueue = new Queue<MapThreadInfo<MapData>>();
    Queue<MapThreadInfo<MeshData>> meshDataThreadInfoQueue = new Queue<MapThreadInfo<MeshData>>();

    public GameObject parentPrefab;
    [Range(0, 5)]
    public int biomeToGenerate;

    public Texture2D main_texture;
    public Texture2D[] main_texture_tab;
    public Texture2D sand_texture;

    public Renderer m_Renderer;

    public void DrawMapInEditor()
    {
        MapData mapData = GenerateMapData(Vector2.zero);

        MapDisplay display = FindObjectOfType<MapDisplay>();
        if (drawMode == DrawMode.NoiseMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(mapData.heightMap));
        }
        else if (drawMode == DrawMode.ColorMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromColourMap(mapData.colourMap, mapChunkSize, mapChunkSize));
        }
        else if (drawMode == DrawMode.Mesh)
        {
            display.DrawMesh(MeshGenerator.GenerateTerrainMesh(mapData.heightMap, meshHeightMultiplier, meshHeightCurve, editorPreviewLOD, main_texture, m_Renderer), main_texture);
            switch(biomeToGenerate)
            {
                case 0: //Sand
                    GenerateNature.GenerateSandWithHeight(useFalloff, seed, noiseScale, octaves, persistance, lacunarity, Vector2.zero, offset, normalizeMode, falloffMap, regions, meshHeightCurve, meshHeightMultiplier, parentPrefab);
                    break;
                case 1: //Grass
                    GenerateNature.GenerateGrassWithHeight(useFalloff, seed, noiseScale, octaves, persistance, lacunarity, Vector2.zero, offset, normalizeMode, falloffMap, regions, meshHeightCurve, meshHeightMultiplier, parentPrefab);
                    break;
                case 2: //Grass2
                    GenerateNature.GenerateGrass2WithHeight(useFalloff, seed, noiseScale, octaves, persistance, lacunarity, Vector2.zero, offset, normalizeMode, falloffMap, regions, meshHeightCurve, meshHeightMultiplier, parentPrefab);
                    break;
                case 3: //Nature
                    GenerateNature.GenerateNatureWithHeight(useFalloff, seed, noiseScale, octaves, persistance, lacunarity, Vector2.zero, offset, normalizeMode, falloffMap, regions, meshHeightCurve, meshHeightMultiplier, parentPrefab);
                    break;
                case 4: //Texture
                    GenerateNature.GenerateTextureMesh(useFalloff, seed, noiseScale, octaves, persistance, lacunarity, Vector2.zero, offset, normalizeMode, falloffMap, regions, meshHeightCurve, meshHeightMultiplier, parentPrefab, main_texture_tab, m_Renderer);
                    break;
                default:
                    Debug.Log("Wrong biome");
                    break;
            }
            //GenerateNature.GenerateNatureWithHeight(useFalloff, seed, noiseScale, octaves, persistance, lacunarity, Vector2.zero, offset, normalizeMode, falloffMap, regions, meshHeightCurve, meshHeightMultiplier, prefabParent);
            //GenerateNature.GenerateGrassWithHeight(useFalloff, seed, noiseScale, octaves, persistance, lacunarity, Vector2.zero, offset, normalizeMode, falloffMap, regions, meshHeightCurve, meshHeightMultiplier, prefabParent);
            //GenerateNature.GenerateGrass2WithHeight(useFalloff, seed, noiseScale, octaves, persistance, lacunarity, Vector2.zero, offset, normalizeMode, falloffMap, regions, meshHeightCurve, meshHeightMultiplier, parentPrefab);
            //GenerateNature.GenerateSandWithHeight(useFalloff, seed, noiseScale, octaves, persistance, lacunarity, Vector2.zero, offset, normalizeMode, falloffMap, regions, meshHeightCurve, meshHeightMultiplier, prefabParent);
        }
        else if(drawMode == DrawMode.FalloffMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(FalloffGenerator.GenerateFalloffMap(mapChunkSize)));
        }
    }

    public void RequestMapData(Vector2 center, Action<MapData> callback)
    {
        ThreadStart threadStart = delegate
        {
            MapDataThread(center, callback);
        };

        new Thread(threadStart).Start();
    }

    public void MapDataThread(Vector2 center, Action<MapData> callback)
    {
        MapData mapData = GenerateMapData(center);
        lock (mapDataThreadInfoQueue)
        {
            mapDataThreadInfoQueue.Enqueue(new MapThreadInfo<MapData>(callback, mapData));
        }
    }

    public void RequestMeshData(MapData mapData, int lod, Action<MeshData> callback)
    {
        ThreadStart threadStart = delegate
        {
            MeshDataThread(mapData, lod, callback);
        };

        new Thread(threadStart).Start();
    }

    void MeshDataThread(MapData mapData, int lod, Action<MeshData> callback)
    {
        MeshData meshData = MeshGenerator.GenerateTerrainMesh(mapData.heightMap, meshHeightMultiplier, meshHeightCurve, lod, main_texture, m_Renderer);
        lock (meshDataThreadInfoQueue)
        {
            meshDataThreadInfoQueue.Enqueue(new MapThreadInfo<MeshData>(callback, meshData));
        }
    }

    private void Update()
    {
        if(mapDataThreadInfoQueue.Count > 0)
        {
            for (int i = 0; i < mapDataThreadInfoQueue.Count; i++)
            {
                MapThreadInfo<MapData> threadInfo = mapDataThreadInfoQueue.Dequeue();
                threadInfo.callback(threadInfo.paramater);
            }
        }
        
        if(meshDataThreadInfoQueue.Count > 0)
        {
            for (int i = 0; i < meshDataThreadInfoQueue.Count; i++)
            {
                MapThreadInfo<MeshData> threadInfo = meshDataThreadInfoQueue.Dequeue();
                threadInfo.callback(threadInfo.paramater);
            }
        }
    }

    MapData GenerateMapData(Vector2 center)
    {
        /*m_Renderer.material.EnableKeyword("_NORMALMAP");
        m_Renderer.material.EnableKeyword("_METALLICGLOSSMAP");*/

        float[,] noiseMap;
        noiseMap = Noise.GenerateNoiseMap(mapChunkSize + 2, mapChunkSize + 2, seed, noiseScale, octaves, persistance, lacunarity, center + offset, normalizeMode);

        if (useFalloff)
        {
            if(falloffMap == null)
            {
                falloffMap = FalloffGenerator.GenerateFalloffMap(mapChunkSize + 2);
            }

            Color[] colourMap = new Color[mapChunkSize * mapChunkSize];
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

                        for (int i = 0; i < regions.Length; i++)
                        {
                            if (currentHeight >= regions[i].height)
                            {
                                colourMap[y * mapChunkSize + x] = regions[i].colour;

                                /*m_Renderer.material.SetTexture("_MainTex", main_texture);
                                m_Renderer.material.SetTexture("_BumpMap", main_texture);
                                m_Renderer.material.SetTexture("_MetallicGlossMap", main_texture);*/

                                Vector3 position = new Vector3(y, 0f, x);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }
            return new MapData(noiseMap, colourMap);
        }

        Color[] colourMapDefault = new Color[mapChunkSize * mapChunkSize];
        return new MapData(noiseMap, colourMapDefault);


    }

    private void OnValidate()
    {
        if(lacunarity < 1)
        {
            lacunarity = 1;
        }
        if(octaves < 0)
        {
            octaves = 0;
        }

    }

    struct MapThreadInfo<T>
    {
        public readonly Action<T> callback;
        public readonly T paramater;

        public MapThreadInfo(Action<T> callback, T parameter)
        {
            this.callback = callback;
            this.paramater = parameter;
        }
    }
}

[System.Serializable]
public struct TerrainType
{
    public string name;
    public float height;
    public Color colour;
    public GameObject[] prefab;
}

public struct MapData
{
    public readonly float[,] heightMap;
    public readonly Color[] colourMap;

    public MapData (float[,] heightMap, Color[] colourMap)
    {
        this.heightMap = heightMap;
        this.colourMap = colourMap;
    }
}