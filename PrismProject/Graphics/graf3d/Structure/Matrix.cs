using System;
using graf3d.Engine.Component;

namespace graf3d.Engine.Structure
{
    /// <summary>
    ///     Macierz 4x4, o wartościach typu zmiennoprzecinkowego.
    /// </summary>
    public class Matrix
    {
        private const int Size = 4;
        public readonly float[,] M = new float[Size, Size];

        /// <summary>
        ///     Zwraca nową macierz przekształcenia tożsamościowego, czyli taką, co ma po przekątnej
        ///     jedynki, a pozostałe wartości są równe zero.
        /// </summary>
        public static Matrix Identity => new Matrix
        {
            M =
            {
                [0, 0] = 1,
                [1, 1] = 1,
                [2, 2] = 1,
                [3, 3] = 1
            }
        };

        /// <summary>
        ///     Tworzy macierz przekształcenia z [2. World Space] do [3. View Space] (przestrzeni świata do przestrzeni widoku).
        /// </summary>
        /// <param name="cameraPosition">
        ///     Współrzędne przestrzeni świata, gdzie umieszczone jest oko kamery.
        /// </param>
        /// <param name="cameraTarget">
        ///     Punkt, w który skierowana jest kamera. Nie ma znaczenia w jakiej odległości od kamery leży ten punkt.
        ///     Jego zastosowanie sprowadza się jedynie do wyznaczenia kierunku, w którym została skierowana kamera.
        /// </param>
        /// <param name="cameraUpVector">
        ///     Wektor, którego zwrot (strzałka) określa gdzie jest "góra" w układzie współrzędnych.
        ///     Przykładowo jeśli obrócimy głowę o 90 stopni, nie zmieniamy kierunku patrzenia, ani nie przemieścimy się.
        ///     Zadaniem tego wektora jest właśnie sprecyzować gdzie jest góra i dół.
        /// </param>
        /// <returns></returns>
        public static Matrix LookAtLH(
            Vector3 cameraPosition,
            Vector3 cameraTarget,
            Vector3 cameraUpVector)
        {
            // Wektor jednostkowy określający zwrot kamery.
            // Intuicyjnie, obiekty leżące na tej osi będą blisko lub daleko względem kamery.
            var zaxis = (cameraTarget - cameraPosition).Normalize();

            // Wektor jednostkowy prostopadły do wektora określającego górę oraz wektora zwrotu kamery.
            // Intuicyjnie, obiekty leżące na tej osi będą po prawej lub lewej względem kamery.
            var xaxis = Vector3.Cross(cameraUpVector, zaxis);

            // Wektor jednostkowy określający gdzie jest góra w układzie odniesienia świata.
            // Intuicyjnie, obiekty leżące na tej osi będą wysoko lub nisko względem kamery.
            var yaxis = Vector3.Cross(zaxis, xaxis);

            var result = Identity;
            result.M[0, 0] = xaxis.X;
            result.M[1, 0] = xaxis.Y;
            result.M[2, 0] = xaxis.Z;
            result.M[3, 0] = -Vector3.Dot(xaxis, cameraPosition);

            result.M[0, 1] = yaxis.X;
            result.M[1, 1] = yaxis.Y;
            result.M[2, 1] = yaxis.Z;
            result.M[3, 1] = -Vector3.Dot(yaxis, cameraPosition);

            result.M[0, 2] = zaxis.X;
            result.M[1, 2] = zaxis.Y;
            result.M[2, 2] = zaxis.Z;
            result.M[3, 2] = -Vector3.Dot(zaxis, cameraPosition);
            return result;
        }

        /// <summary>
        ///     Zwraca współrzędne wybranej osi.
        /// </summary>
        public Vector3 GetAxis(Axis axis)
        {
            var n = (int) axis;
            return new Vector3(M[0, n], M[1, n], M[2, n]);
        }

        /// <summary>
        ///     Tworzy macierz przekształcenia perspektywicznego z [3. View Space] do [4. Projection Space].
        /// </summary>
        /// <param name="fieldOfViewY">
        ///     Pole widzenia w radianach. Przyjmuje wartości od 0 do PI. Gdy pole widzenia jest małe,
        ///     widziane obiekty są powiększone (zoom). Gdy pole widzenia jest duże, widziane obiekty są małe.
        /// </param>
        /// <param name="aspectRatio">
        ///     Stosunek szerokości do wysokości ekranu.
        /// </param>
        /// <param name="znearPlane">
        ///     Odległość przedniej płaszczyzny obcinającej ostrosłupa widzenia od kamery.
        /// </param>
        /// <param name="zfarPlane">
        ///     Odległość tylnej płaszczyzny obcinającej ostrosłupa widzenia od kamery.
        /// </param>
        /// <returns></returns>
        public static Matrix PerspectiveFovLH(float fieldOfViewY, float aspectRatio, float znearPlane, float zfarPlane)
        {
            var cotTheta = (float) (1f/Math.Tan(fieldOfViewY*0.5f));
            var q = zfarPlane/(zfarPlane - znearPlane);

            var result = new Matrix();
            result.M[0, 0] = cotTheta/aspectRatio;
            result.M[1, 1] = cotTheta;
            result.M[2, 2] = q;
            result.M[2, 3] = 1f;
            result.M[3, 3] = -q*znearPlane;
            return result;
        }

        /// <summary>
        ///     Tworzy macierz rotacji dla danego kwaternionu.
        /// </summary>
        public static Matrix RotationQuaternion(Quaternion rotation)
        {
            var xx = rotation.X*rotation.X;
            var yy = rotation.Y*rotation.Y;
            var zz = rotation.Z*rotation.Z;
            var xy = rotation.X*rotation.Y;
            var zw = rotation.Z*rotation.W;
            var zx = rotation.Z*rotation.X;
            var yw = rotation.Y*rotation.W;
            var yz = rotation.Y*rotation.Z;
            var xw = rotation.X*rotation.W;

            var result = Identity;
            result.M[0, 0] = 1.0f - 2.0f*(yy + zz);
            result.M[0, 1] = 2.0f*(xy + zw);
            result.M[0, 2] = 2.0f*(zx - yw);

            result.M[1, 0] = 2.0f*(xy - zw);
            result.M[1, 1] = 1.0f - 2.0f*(zz + xx);
            result.M[1, 2] = 2.0f*(yz + xw);

            result.M[2, 0] = 2.0f*(zx + yw);
            result.M[2, 1] = 2.0f*(yz - xw);
            result.M[2, 2] = 1.0f - 2.0f*(yy + xx);

            return result;
        }

        /// <summary>
        ///     Tworzy macierz przesunięcia (translacji) o dany wektor.
        /// </summary>
        public static Matrix Translation(Vector3 vector)
        {
            var result = Identity;
            result.M[3, 0] = vector.X;
            result.M[3, 1] = vector.Y;
            result.M[3, 2] = vector.Z;
            return result;
        }

        /// <summary>
        ///     Tworzy macierz skalowania o dany wektor.
        /// </summary>
        public static Matrix Scaling(Vector3 vector)
        {
            var result = Identity;
            result.M[0, 0] = vector.X;
            result.M[1, 1] = vector.Y;
            result.M[2, 2] = vector.Z;
            return result;
        }

        /// <summary>
        ///     Mnoży dwie macierze.
        /// </summary>
        public static Matrix operator *(Matrix left, Matrix right)
        {
            var result = new Matrix();

            for (var i = 0; i < Size; i++)
                for (var j = 0; j < Size; j++)
                    for (var k = 0; k < Size; k++)
                        result.M[i, j] += left.M[i, k]*right.M[k, j];

            return result;
        }

        /// <summary>
        ///     Oblicza macierz odwrotną do bieżącej macierzy.
        /// </summary>
        public Matrix Invert()
        {
            var b0 = M[2, 0]*M[3, 1] - M[2, 1]*M[3, 0];
            var b1 = M[2, 0]*M[3, 2] - M[2, 2]*M[3, 0];
            var b2 = M[2, 3]*M[3, 0] - M[2, 0]*M[3, 3];
            var b3 = M[2, 1]*M[3, 2] - M[2, 2]*M[3, 1];
            var b4 = M[2, 3]*M[3, 1] - M[2, 1]*M[3, 3];
            var b5 = M[2, 2]*M[3, 3] - M[2, 3]*M[3, 2];

            var d11 = M[1, 1]*b5 + M[1, 2]*b4 + M[1, 3]*b3;
            var d12 = M[1, 0]*b5 + M[1, 2]*b2 + M[1, 3]*b1;
            var d13 = M[1, 0]*-b4 + M[1, 1]*b2 + M[1, 3]*b0;
            var d14 = M[1, 0]*b3 + M[1, 1]*-b1 + M[1, 2]*b0;

            var det = M[0, 0]*d11 - M[0, 1]*d12 + M[0, 2]*d13 - M[0, 3]*d14;

            if (Math.Abs(det) < float.Epsilon)
            {
                return new Matrix();
            }

            det = 1f/det;

            var a0 = M[0, 0]*M[1, 1] - M[0, 1]*M[1, 0];
            var a1 = M[0, 0]*M[1, 2] - M[0, 2]*M[1, 0];
            var a2 = M[0, 3]*M[1, 0] - M[0, 0]*M[1, 3];
            var a3 = M[0, 1]*M[1, 2] - M[0, 2]*M[1, 1];
            var a4 = M[0, 3]*M[1, 1] - M[0, 1]*M[1, 3];
            var a5 = M[0, 2]*M[1, 3] - M[0, 3]*M[1, 2];

            var d21 = M[0, 1]*b5 + M[0, 2]*b4 + M[0, 3]*b3;
            var d22 = M[0, 0]*b5 + M[0, 2]*b2 + M[0, 3]*b1;
            var d23 = M[0, 0]*-b4 + M[0, 1]*b2 + M[0, 3]*b0;
            var d24 = M[0, 0]*b3 + M[0, 1]*-b1 + M[0, 2]*b0;

            var d31 = M[3, 1]*a5 + M[3, 2]*a4 + M[3, 3]*a3;
            var d32 = M[3, 0]*a5 + M[3, 2]*a2 + M[3, 3]*a1;
            var d33 = M[3, 0]*-a4 + M[3, 1]*a2 + M[3, 3]*a0;
            var d34 = M[3, 0]*a3 + M[3, 1]*-a1 + M[3, 2]*a0;

            var d41 = M[2, 1]*a5 + M[2, 2]*a4 + M[2, 3]*a3;
            var d42 = M[2, 0]*a5 + M[2, 2]*a2 + M[2, 3]*a1;
            var d43 = M[2, 0]*-a4 + M[2, 1]*a2 + M[2, 3]*a0;
            var d44 = M[2, 0]*a3 + M[2, 1]*-a1 + M[2, 2]*a0;

            var result = new Matrix();
            result.M[0, 0] = +d11*det;
            result.M[0, 1] = -d21*det;
            result.M[0, 2] = +d31*det;
            result.M[0, 3] = -d41*det;
            result.M[1, 0] = -d12*det;
            result.M[1, 1] = +d22*det;
            result.M[1, 2] = -d32*det;
            result.M[1, 3] = +d42*det;
            result.M[2, 0] = +d13*det;
            result.M[2, 1] = -d23*det;
            result.M[2, 2] = +d33*det;
            result.M[2, 3] = -d43*det;
            result.M[3, 0] = -d14*det;
            result.M[3, 1] = +d24*det;
            result.M[3, 2] = -d34*det;
            result.M[3, 3] = +d44*det;
            return result;
        }
    }
}