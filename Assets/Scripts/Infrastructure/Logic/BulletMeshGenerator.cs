using UnityEngine;

namespace Infrastructure.Logic
{
    [RequireComponent(typeof(MeshFilter))]
    public class BulletMeshGenerator : MonoBehaviour
    {
        [SerializeField] private MeshFilter _meshFilter;
        [SerializeField] private MeshRenderer _meshRenderer;

        private void Start() =>
            GenerateRandomBullet();

        public void GenerateRandomBullet()
        {
            Mesh mesh = new Mesh();

            mesh.vertices = GenerateRandomVertices();
            mesh.triangles = GetCubeTriangles();
            mesh.RecalculateNormals();
            
            _meshFilter.mesh = mesh;
        }

        private Vector3[] GenerateRandomVertices()
        {
            Vector3[] vertices = new Vector3[8];
            float cubeHalfSize = transform.localScale.x / 2;
            int cubeVerticesDeep = 2;
            var centerPosition = transform.localPosition;

            int index = 0;
            for (int x = 0; x < cubeVerticesDeep; x++)
            for (int y = 0; y < cubeVerticesDeep; y++)
            for (int z = 0; z < cubeVerticesDeep; z++)
            {
                Vector3 pointPos = new Vector3(x - cubeHalfSize, y - cubeHalfSize, z - cubeHalfSize);
                Vector3 pointFromCenterDir = centerPosition - pointPos;

                Vector3 randomPointPos = new Vector3();
                for (int i = 0; i < 3; i++)
                {
                    Vector3 startPoint = centerPosition - pointFromCenterDir / 2;
                    randomPointPos[i] = Random.Range(startPoint[i], pointPos[i]);
                }

                vertices[index++] = randomPointPos;
            }

            return vertices;
        }

        private static int[] GetCubeTriangles()
        {
            int[] triangles =
            {
                0, 1, 3, 0, 3, 2,
                1, 7, 3, 1, 5, 7,
                5, 6, 7, 5, 4, 6,
                4, 2, 6, 4, 0, 2,
                2, 3, 7, 2, 7, 6,
                0, 5, 1, 0, 4, 5
            };
            return triangles;
        }
    }
}