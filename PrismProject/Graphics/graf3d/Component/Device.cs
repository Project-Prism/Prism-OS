using System.Linq;
using graf3d.Engine.Abstractions;
using graf3d.Engine.Algorithm;
using graf3d.Engine.Structure;

namespace graf3d.Engine.Component
{
    public class Device
    {
        /// <summary>
        ///     Implementacja algorytmu obcinania odcinków poza ekranem.
        /// </summary>
        protected readonly IClippingAlgorithm ClippingAlgorithm;

        /// <summary>
        ///     Implementacja algorytmu rasteryzacji odcinka.
        /// </summary>
        protected readonly ILineDrawingAlgorithm LineDrawingAlgorithm;

        /// <summary>
        ///     Bitmapa, która będzie wyświetlona na ekranie.
        /// </summary>
        protected IBufferedBitmap Bitmap;

        /// <summary>
        ///     Inicjalizuje bitmapę i implementacje algorytmów.
        /// </summary>
        public Device(IBufferedBitmap bmp, ILineDrawingAlgorithm lineDrawing, IClippingAlgorithm clip)
        {
            Bitmap = bmp;
            LineDrawingAlgorithm = lineDrawing;
            ClippingAlgorithm = clip;
            ClippingAlgorithm.SetBoundingRectangle(new Vector2(0, 0), new Vector2(Bitmap.PixelWidth, Bitmap.PixelHeight));
        }

        /// <summary>
        ///     Przekształca współrzędne trójwymiarowe do dwuwymiarowych
        ///     za pomocą macierzy transformacji w celu późniejszej rasteryzacji.
        /// </summary>
        public Vector2 Project(Vector3 coord, Matrix transMat)
        {
            var point = Vector3.TransformCoordinate(coord, transMat);

            // Uzyskane współrzędne należy poprawić, aby x i y były równe zero w górnym lewym rogu ekranu.
            var x = Bitmap.PixelWidth*(point.X + 0.5f);
            var y = Bitmap.PixelHeight*(-point.Y + 0.5f);
            return new Vector2(x, y);
        }

        /// <summary>
        ///     Renderuje obiekty na bitmapie używając danej kamery.
        /// </summary>
        public void Render(Scene scene)
        {
            // Kolejność przekształceń.
            // [1. Object Space]. W tej przestrzeni znajdują się modele na początku, nie mają swojego położenia, ani rotacji.
            // [2. World Space]. Wspólna przestrzeń, w której znajduje się kamera i wszystkie modele już po nadaniu im współrzędnych oraz rotacji.
            // [3. View Space]. Przestrzeń ze współrzędnymi względem kamery, która znajduje się w (0, 0, 0).
            // [4. Projection Space]. Po nadaniu tego przekształcenia obiekty widziane przez kamerę zyskują perspektywę.

            // Macierz przekształcenia z [2. World space] do [3. View space].
            var viewMatrix = scene.Camera.LookAtLH();

            // Macierz przekształcenia z [3. View space] do [4. Projection space].
            var projectionMatrix = Matrix.PerspectiveFovLH(
                scene.Camera.FieldOfViewRadians,
                Bitmap.AspectRatio,
                scene.Camera.ZNear,
                scene.Camera.ZFar);

            // Iterujemy wszystkie siatki modelów które mają zostać wyrenderowane.
            foreach (var mesh in scene.Meshes)
            {
                // Przygotowujemy macierz przekształcenia z [1. Object Space] do [2. World Space].
                // W tym celu zastosujemy najpierw rotację, a potem translację obiektu tak, aby znalazł się
                // w pożądanych współrzędnych w przestrzeni świata (World Space).
                var worldMatrix = Matrix.Scaling(mesh.Scaling)*Matrix.RotationQuaternion(mesh.Rotation)*
                                  Matrix.Translation(mesh.Position);

                // Poniższa macierz łączy wszystkie przekształcenia od 1. do 4. w odpowiedniej kolejności.
                var transformMatrix = worldMatrix*viewMatrix*projectionMatrix;

                // Przekształcamy współrzędne 3D do współrzędnych 2D na bitmapie.
                var pixels = mesh.Vertices.Select(vertex => Project(vertex, transformMatrix)).ToArray();


                var vertices =
                    mesh.Vertices.Select(vertex => Vector3.TransformCoordinate(vertex, worldMatrix*viewMatrix))
                        .ToArray();

                var color32 = mesh.Color.ToColor32();
                // Iterujemy wszystkie "trójkąty" bieżącej siatki.
                foreach (var face in mesh.Faces)
                {
                    // Każdy trójkąt ma trzy wierzchołki.
                    if (vertices[face.A].Z < scene.Camera.ZNear
                        || vertices[face.B].Z < scene.Camera.ZNear
                        || vertices[face.C].Z < scene.Camera.ZNear
                        )
                    {
                        continue;
                    }

                    face.Edges((a, b) =>
                    {
                        var p1 = pixels[a];
                        var p2 = pixels[b];

                        // Obcina linie siatki wychodzące poza ekran.
                        if (ClippingAlgorithm.ClipLine(ref p1, ref p2))
                        {
                            // Rysuje linie siatki między wierzchołkami trójkątów.
                            LineDrawingAlgorithm.DrawLine(p1, p2, (x, y) => PrismProject.Graphics.Canvas2.Canvas.DrawPoint(new Cosmos.System.Graphics.Pen(System.Drawing.Color.FromArgb(color32.A, color32.R, color32.G, color32.B)), x, y));
                        }
                    });
                }
            }
        }
    }
}