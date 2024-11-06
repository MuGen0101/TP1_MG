
using UnityEngine;

[RequireComponent (typeof(MeshFilter))]
public class TP1Ex1 : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;

    public int nb_lignes = 10;   // Nombre de lignes
    public int nb_colonnes = 10; // Nombre de colonnes
    public float largeur = 1f;   // Largeur d'une cellule
    public float longueur = 1f;  // Longueur d'une cellule

    private void Awake()
    {
        mesh = GetComponent<MeshFilter> ().mesh;
    }

    // Start is called before the first frame update
    void Start()
    {
        MakeMeshData();
        CreateMesh();

    }

    void MakeMeshData()
    {
        // Créer un tableau de sommets
        vertices = new Vector3[(nb_lignes + 1) * (nb_colonnes + 1)];
        for (int i = 0, y = 0; y <= nb_lignes; y++)
        {
            for (int x = 0; x <= nb_colonnes; x++, i++)
            {
                vertices[i] = new Vector3(x * largeur, 0, y * longueur);
            }
        }

        // Créer un tableau d'indices de triangles
        triangles = new int[nb_lignes * nb_colonnes * 6]; // Chaque carré est composé de 2 triangles (3 points chacun)
        int triIndex = 0;
        for (int y = 0; y < nb_lignes; y++)
        {
            for (int x = 0; x < nb_colonnes; x++)
            {
                int start = y * (nb_colonnes + 1) + x;

                // Premier triangle
                triangles[triIndex] = start;
                triangles[triIndex + 1] = start + nb_colonnes + 1;
                triangles[triIndex + 2] = start + 1;

                // Deuxième triangle
                triangles[triIndex + 3] = start + 1;
                triangles[triIndex + 4] = start + nb_colonnes + 1;
                triangles[triIndex + 5] = start + nb_colonnes + 2;

                triIndex += 6;
            }
        }
    }


    void CreateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

    }
}
 
