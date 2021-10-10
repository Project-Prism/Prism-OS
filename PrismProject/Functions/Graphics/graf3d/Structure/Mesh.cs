using System;

namespace graf3d.Engine.Structure
{
    /// <summary>
    ///     Trójkąt o wierzchołkach w przestrzeni trójwymiarowej.
    ///     Komponenty A, B, C oznaczają indeks wierzchołka w klasie Mesh.
    /// </summary>
    public struct Face
    {
        public readonly int A;
        public readonly int B;
        public readonly int C;

        public Face(int a, int b, int c)
        {
            A = a;
            B = b;
            C = c;
        }

        public void Edges(Action<int, int> action)
        {
            action.Invoke(A, B);
            action.Invoke(B, C);
            action.Invoke(A, C);
        }
    }

    /// <summary>
    ///     Reprezentuje wielościan o ścianach trójkątnych.
    /// </summary>
    public class Mesh
    {
        /// <summary>
        ///     Tworzy wielościan o danej ilości wierzchołków i trójkątnych ścian.
        /// </summary>
        public Mesh(int verticesCount, int facesCount)
        {
            Vertices = new Vector3[verticesCount];
            Faces = new Face[facesCount];
        }

        /// <summary>
        ///     Opisowa nazwa wielościanu.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Kolor siatki (przy rysowaniu krawędziowym).
        /// </summary>
        public Color Color { get; set; } = Colors.Yellow;

        /// <summary>
        ///     Zbiór wierzchołków wielościanu.
        /// </summary>
        public Vector3[] Vertices { get; }

        /// <summary>
        ///     Zbiór trójkątnych ścian (o wierzchołkach zdefiniowanych w Vertices).
        /// </summary>
        public Face[] Faces { get; set; }

        /// <summary>
        ///     Położenie wielościanu w przestrzeni świata.
        /// </summary>
        public Vector3 Position { get; set; } = Vector3.Zero;

        /// <summary>
        ///     Kwaternion rotacji wielościanu.
        /// </summary>
        public Quaternion Rotation { get; set; } = Quaternion.RotationYawPitchRoll(0, 0, 0);

        /// <summary>
        ///     Wektor skali. Domyślnie wielościan pozostaje w oryginalnych wymiarach.
        /// </summary>
        public Vector3 Scaling { get; set; } = Vector3.One;
    }

    /// <summary>
    ///     Przykładowy wielościan (sześcian) o 8 wierzchołkach i 12 ścianach.
    ///     Ścian jest dwanaście, gdyż zamiast kwadratów ściany składają się z trójkątów.
    /// </summary>
    public class Cube : Mesh
    {
        public Cube() : base(8, 12)
        {
            Vertices[0] = new Vector3(-1, 1, 1);
            Vertices[1] = new Vector3(1, 1, 1);
            Vertices[2] = new Vector3(-1, -1, 1);
            Vertices[3] = new Vector3(1, -1, 1);
            Vertices[4] = new Vector3(-1, 1, -1);
            Vertices[5] = new Vector3(1, 1, -1);
            Vertices[6] = new Vector3(1, -1, -1);
            Vertices[7] = new Vector3(-1, -1, -1);

            Faces[0] = new Face(0, 1, 2);
            Faces[1] = new Face(1, 2, 3);
            Faces[2] = new Face(1, 3, 6);
            Faces[3] = new Face(1, 5, 6);
            Faces[4] = new Face(0, 1, 4);
            Faces[5] = new Face(1, 4, 5);
            Faces[5] = new Face(2, 3, 7);
            Faces[6] = new Face(3, 6, 7);
            Faces[7] = new Face(0, 2, 7);
            Faces[8] = new Face(0, 4, 7);
            Faces[9] = new Face(4, 5, 6);
            Faces[10] = new Face(4, 6, 7);
        }
    }
}