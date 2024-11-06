using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{

    public float rayon = 1f;
    public int meridiens = 24;
    public int paralleles = 12;

    // Start is called before the first frame update
    void Start()
    {
        MeshFilter mf = gameObject.AddComponent<MeshFilter>();
        MeshRenderer mr = gameObject.AddComponent<MeshRenderer>();

        Mesh mesh = new Mesh();
        Vector3[] vertices = new Vector3[(meridiens + 1) * (paralleles + 1)];
        int[] triangles = new int[meridiens * paralleles * 6];

        for (int lat = 0; lat <= paralleles; lat++)
        {
            float theta = lat * Mathf.PI / paralleles;
            float sinTheta = Mathf.Sin(theta);
            float cosTheta = Mathf.Cos(theta);

            for (int lon = 0; lon <= meridiens; lon++)
            {
                float phi = lon * 2 * Mathf.PI / meridiens;
                float sinPhi = Mathf.Sin(phi);
                float cosPhi = Mathf.Cos(phi);

                float x = cosPhi * sinTheta;
                float y = cosTheta;
                float z = sinPhi * sinTheta;

                vertices[lat * (meridiens + 1) + lon] = new Vector3(x, y, z) * rayon;
            }
        }

        int triIndex = 0;
        for (int lat = 0; lat < paralleles; lat++)
        {
            for (int lon = 0; lon < meridiens; lon++)
            {
                int current = lat * (meridiens + 1) + lon;
                int next = current + meridiens + 1;

                triangles[triIndex] = current;
                triangles[triIndex + 1] = next;
                triangles[triIndex + 2] = current + 1;

                triangles[triIndex + 3] = current + 1;
                triangles[triIndex + 4] = next;
                triangles[triIndex + 5] = next + 1;

                triIndex += 6;
            }
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        mf.mesh = mesh;
    }
}
