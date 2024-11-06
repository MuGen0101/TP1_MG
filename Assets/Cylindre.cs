using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cylindre : MonoBehaviour
{
    // Start is called before the first frame update
    public float rayon = 1f;
    public float hauteur = 2f;
    public int meridiens = 24;

    void Start()
    {
        MeshFilter mf = gameObject.AddComponent<MeshFilter>();
        MeshRenderer mr = gameObject.AddComponent<MeshRenderer>();

        Mesh mesh = new Mesh();
        Vector3[] vertices = new Vector3[(meridiens + 1) * 2 + 2];
        int[] triangles = new int[meridiens * 12];

        // Corps du cylindre
        for (int i = 0; i <= meridiens; i++)
        {
            float angle = i * Mathf.PI * 2 / meridiens;
            float x = Mathf.Cos(angle) * rayon;
            float z = Mathf.Sin(angle) * rayon;
            vertices[i] = new Vector3(x, -hauteur / 2, z); // Bas
            vertices[i + meridiens + 1] = new Vector3(x, hauteur / 2, z); // Haut
        }

        // Fermeture des "couvercles"
        vertices[vertices.Length - 2] = new Vector3(0, -hauteur / 2, 0); // Bas centre
        vertices[vertices.Length - 1] = new Vector3(0, hauteur / 2, 0); // Haut centre

        int triIndex = 0;
        for (int i = 0; i < meridiens; i++)
        {
            // Côtés du cylindre
            triangles[triIndex] = i;
            triangles[triIndex + 1] = i + meridiens + 1;
            triangles[triIndex + 2] = i + 1;

            triangles[triIndex + 3] = i + 1;
            triangles[triIndex + 4] = i + meridiens + 1;
            triangles[triIndex + 5] = i + meridiens + 2;

            // Bas du cylindre
            triangles[triIndex + 6] = vertices.Length - 2;
            triangles[triIndex + 7] = i + 1;
            triangles[triIndex + 8] = i;

            // Haut du cylindre
            triangles[triIndex + 9] = vertices.Length - 1;
            triangles[triIndex + 10] = i + meridiens + 1;
            triangles[triIndex + 11] = i + meridiens + 2;

            triIndex += 12;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        mf.mesh = mesh;
    }
}