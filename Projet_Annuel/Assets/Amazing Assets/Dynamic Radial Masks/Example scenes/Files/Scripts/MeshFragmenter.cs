using System.Collections.Generic;

using UnityEngine;


namespace AmazingAssets.DynamicRadialMasks.Example
{
    public class MeshFragmenter : MonoBehaviour
    {
        void Start()
        {
            Mesh mesh = GetComponent<MeshFilter>().sharedMesh;

            Mesh fragmentedMesh = DoFragmentMesh(ref mesh);


            Vector3[] mVertices = fragmentedMesh.vertices;
            Vector3[] mNormals = fragmentedMesh.normals;
            List<Vector4> mUV3CenterPoint = new List<Vector4>();
            List<Vector3> mUV4FlatNormal = new List<Vector3>();

            for (int i = 0; i < fragmentedMesh.vertexCount; i += 3)
            {
                Vector4 centerPoint = (mVertices[i] + mVertices[i + 1] + mVertices[i + 2]) / 3f;
                centerPoint.w = 1;

                mUV3CenterPoint.Add(centerPoint);   //i
                mUV3CenterPoint.Add(centerPoint);   //i + 1
                mUV3CenterPoint.Add(centerPoint);   //i + 2



                Vector3 n = (mNormals[i] + mNormals[i + 1] + mNormals[i + 2]) / 3f;
                n = n.normalized;

                mUV4FlatNormal.Add(n);   //i
                mUV4FlatNormal.Add(n);   //i + 1
                mUV4FlatNormal.Add(n);   //i + 2
            }

            fragmentedMesh.SetUVs(2, mUV3CenterPoint);
            fragmentedMesh.SetUVs(3, mUV4FlatNormal);


            GetComponent<MeshFilter>().sharedMesh = fragmentedMesh;
        }



        static Mesh DoFragmentMesh(ref Mesh _mesh)
        {
            Vector3[] mVertices = _mesh.vertices;
            Vector3[] mNormal = _mesh.normals;
            Vector4[] mTangent = _mesh.tangents;
            List<Vector4> mUV = new List<Vector4>();
            _mesh.GetUVs(0, mUV);
            List<Vector4> mUV2 = new List<Vector4>();
            _mesh.GetUVs(1, mUV2);
            Color[] mColor = _mesh.colors;


            List<Vector3> newVertices = new List<Vector3>();
            List<List<int>> subMeshIndeces = new List<List<int>>();
            List<Vector4> newUV = new List<Vector4>();
            List<Vector4> newUV2 = new List<Vector4>();
            List<Vector3> newNormal = new List<Vector3>();
            List<Vector4> newTangent = new List<Vector4>();
            List<Color> newColor = new List<Color>();


            bool hasUV = true;
            bool hasUV2 = true;
            bool hasNormal = true;
            bool hasTangent = true;
            bool hasColor = true;

            if (_mesh.uv == null || _mesh.uv.Length != _mesh.vertexCount)
                hasUV = false;
            if (_mesh.uv2 == null || _mesh.uv2.Length != _mesh.vertexCount)
                hasUV2 = false;
            if (_mesh.normals == null || _mesh.normals.Length != _mesh.vertexCount)
                hasNormal = false;
            if (_mesh.tangents == null || _mesh.tangents.Length != _mesh.vertexCount)
                hasTangent = false;
            if (_mesh.colors == null || _mesh.colors.Length != _mesh.vertexCount)
                hasColor = false;




            int tIndec = 0;
            for (int i = 0; i < _mesh.subMeshCount; i++)
            {
                int[] mT = _mesh.GetTriangles(i);

                subMeshIndeces.Add(new List<int>());

                for (int j = 0; j < mT.Length; j += 3)
                {
                    int index1 = mT[j];
                    int index2 = mT[j + 1];
                    int index3 = mT[j + 2];

                    //Indexes
                    subMeshIndeces[subMeshIndeces.Count - 1].Add(tIndec++);
                    subMeshIndeces[subMeshIndeces.Count - 1].Add(tIndec++);
                    subMeshIndeces[subMeshIndeces.Count - 1].Add(tIndec++);

                    //Add vertices
                    newVertices.Add(mVertices[index1]);
                    newVertices.Add(mVertices[index2]);
                    newVertices.Add(mVertices[index3]);

                    //UV
                    if (hasUV)
                    {
                        newUV.Add(mUV[index1]);
                        newUV.Add(mUV[index2]);
                        newUV.Add(mUV[index3]);
                    }
                    //UV2
                    if (hasUV2)
                    {
                        newUV2.Add(mUV2[index1]);
                        newUV2.Add(mUV2[index2]);
                        newUV2.Add(mUV2[index3]);
                    }

                    //Normal
                    if (hasNormal)
                    {
                        Vector3 normal = (mNormal[index1] + mNormal[index2] + mNormal[index3]) / 3.0f;
                        normal = normal.normalized;// normal.normalized;

                        newNormal.Add(normal);
                        newNormal.Add(normal);
                        newNormal.Add(normal);
                    }
                    //Tangent
                    if (hasTangent)
                    {
                        newTangent.Add(mTangent[index1]);
                        newTangent.Add(mTangent[index2]);
                        newTangent.Add(mTangent[index3]);
                    }
                    //Color
                    if (hasColor)
                    {
                        newColor.Add(mColor[index1]);
                        newColor.Add(mColor[index2]);
                        newColor.Add(mColor[index3]);
                    }
                }

            }


            Mesh saveMesh = new Mesh();
            saveMesh.subMeshCount = _mesh.subMeshCount;
            saveMesh.hideFlags = HideFlags.HideAndDontSave;


            saveMesh.vertices = newVertices.ToArray();
            for (int i = 0; i < subMeshIndeces.Count; i++)
                saveMesh.SetTriangles(subMeshIndeces[i].ToArray(), i);

            if (hasUV)
                saveMesh.SetUVs(0, new List<Vector4>(newUV));
            if (hasUV2)
                saveMesh.SetUVs(1, new List<Vector4>(newUV2));
            if (hasNormal)
                saveMesh.normals = newNormal.ToArray();
            if (hasTangent)
                saveMesh.tangents = newTangent.ToArray();
            if (hasColor)
                saveMesh.colors = newColor.ToArray();


            return saveMesh;
        }

    }
}