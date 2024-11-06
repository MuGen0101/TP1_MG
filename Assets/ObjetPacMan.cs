using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetPacMan : MonoBehaviour
{
    public float rayon = 1f;
    public int meridiens = 24;
    public int paralleles = 12;
    public float angleBouche = 45f;


    // Start is called before the first frame update
    void Start()
    {
        MeshFilter mf = gameObject.AddComponent<MeshFilter>();
        MeshRenderer mr = gameObject.AddComponent<MeshRenderer>();

        Mesh mesh = new Mesh();
        Vector3[] vertices = new Vector3[(meridiens + 1) * (paralleles + 1)];
        List<int> triangles = new List<int>();

        float angleRadians = angleBouche * Mathf.Deg2Rad;

        for (int lat = 0; lat <= paralleles; lat++)
        {
            float theta = lat * Mathf.PI / paralleles;
            float sinTheta = Mathf.Sin(theta);
            float cosTheta = Mathf.Cos(theta);

            for (int lon = 0; lon <= meridiens; lon++)
            {
                float phi = lon * 2 * Mathf.PI / meridiens;

                // Vérifier si le point est dans la "bouche" de Pac-Man
                if (phi <= angleRadians || phi >= 2 * Mathf.PI - angleRadians)
                {
                    float sinPhi = Mathf.Sin(phi);
                    float cosPhi = Mathf.Cos(phi);
                    float x = cosPhi * sinTheta;
                    float y = cosTheta;
                    float z = sinPhi * sinTheta;
                    vertices[lat * (meridiens + 1) + lon] = new Vector3(x, y, z) * rayon;
                }
                else
                {
                    // Placer les points de la "bouche" au centre pour créer une troncature
                    vertices[lat * (meridiens + 1) + lon] = Vector3.zero;
                }
            }
        }

        for (int lat = 0; lat < paralleles; lat++)
        {
            for (int lon = 0; lon < meridiens; lon++)
            {
                int current = lat * (meridiens + 1) + lon;
                int next = current + meridiens + 1;

                // Ne créer des triangles que si les points ne sont pas au centre
                if (vertices[current] != Vector3.zero && vertices[current + 1] != Vector3.zero && vertices[next] != Vector3.zero)
                {
                    triangles.Add(current);
                    triangles.Add(next);
                    triangles.Add(current + 1);
                }

                if (vertices[current + 1] != Vector3.zero && vertices[next] != Vector3.zero && vertices[next + 1] != Vector3.zero)
                {
                    triangles.Add(current + 1);
                    triangles.Add(next);
                    triangles.Add(next + 1);
                }
            }
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();
        mf.mesh = mesh;
    }
}