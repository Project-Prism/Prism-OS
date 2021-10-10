using System;
using graf3d.Engine.Structure;

namespace graf3d.Engine.Component
{
    /// <summary>
    ///     Względna oś obrotu.
    /// </summary>
    public enum Axis
    {
        X = 0,
        Y = 1,
        Z = 2
    }

    /// <summary>
    ///     Względna przestrzeń - świata lub kamery (lokalna).
    /// </summary>
    public enum RelativeSpace
    {
        World,
        Camera
    }

    public class Camera
    {
        /// <summary>
        ///     Pole widzenia kamery w stopniach.
        /// </summary>
        private float _fieldOfView = 60;

        /// <summary>
        ///     Pozycja kamery w przestrzeni świata (World Space).
        /// </summary>
        public Vector3 Position { get; set; }

        /// <summary>
        ///     Względny kierunek patrzenia kamery.
        /// </summary>
        public Vector3 LookDirection { get; set; } = Vector3.UnitZ;

        /// <summary>
        ///     Kierunek wskazujący górę (w przestrzeni modelu).
        /// </summary>
        public Vector3 UpDirection { get; set; } = Vector3.UnitY;

        /// <summary>
        ///     Rotacja kierunku patrzenia kamery.
        /// </summary>
        public Quaternion Rotation { get; set; } = Quaternion.RotationYawPitchRoll(0, 0, 0);

        /// <summary>
        ///     Pole widzenia. Jako wartości przyjmuje liczby z zakresu 0 - 180 stopni.
        ///     Im mniejsze pole widzenia, tym większy "zoom", czy też zbliżenie.
        /// </summary>
        public float FieldOfView
        {
            get { return _fieldOfView; }
            set { if (value > 0 && value < 180) _fieldOfView = value; }
        }

        /// <summary>
        ///     Pole widzenia w radianach.
        /// </summary>
        public float FieldOfViewRadians
        {
            get { return (float) (FieldOfView/180*Math.PI); }
            set { FieldOfView = (float) (value*180/Math.PI); }
        }

        /// <summary>
        ///     Odległość przedniej płaszczyzny obcinającej ostrosłupa widzenia od kamery.
        /// </summary>
        public float ZNear { get; set; } = 0.01f;

        /// <summary>
        ///     Odległość tylnej płaszczyzny obcinającej ostrosłupa widzenia od kamery.
        /// </summary>
        public float ZFar { get; set; } = 100f;

        /// <summary>
        ///     Tworzy macierz przekształcenia z [2. World Space] do [3. View Space].
        /// </summary>
        public Matrix LookAtLH()
        {
            var cameraRotationMatrix = Matrix.RotationQuaternion(Rotation);

            // Zastosowuje rotację kamery do jej kierunku patrzenia.
            var cameraLookDirection = Vector3.TransformCoordinate(
                LookDirection,
                cameraRotationMatrix);

            var cameraUpDirection = Vector3.TransformCoordinate(
                UpDirection,
                cameraRotationMatrix);

            return Matrix.LookAtLH(Position, Position + cameraLookDirection, cameraUpDirection);
        }

        /// <summary>
        ///     Przesuwa kamerę po dowolnej osi względem bieżącej pozycji.
        /// </summary>
        public void Move(Vector3 move)
        {
            var viewMatrix = LookAtLH();
            var relativePosition = Vector3.TransformCoordinate(Position, viewMatrix);
            Position = Vector3.TransformCoordinate(relativePosition + move, viewMatrix.Invert());
        }

        /// <summary>
        ///     Obraca kamerę względem bieżącej pozycji.
        /// </summary>
        /// <param name="axisEnum">
        ///     Relatywna oś obrotu.
        /// </param>
        /// <param name="angle">
        ///     Kąt obrotu w stopniach.
        /// </param>
        /// <param name="space">
        ///     Relatywna przestrzeń w której znajduje się oś obrotu.
        /// </param>
        public void Rotate(Axis axisEnum, float angle, RelativeSpace space = RelativeSpace.Camera)
        {
            var radians = (float) Math.PI*angle/180;

            Vector3 axis;
            switch (space)
            {
                case RelativeSpace.World:
                    switch (axisEnum)
                    {
                        case Axis.X:
                            axis = Vector3.UnitX;
                            break;
                        case Axis.Y:
                            axis = Vector3.UnitY;
                            break;
                        case Axis.Z:
                            axis = Vector3.UnitZ;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(axisEnum), axisEnum, null);
                    }
                    break;
                case RelativeSpace.Camera:
                    axis = LookAtLH().GetAxis(axisEnum);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(space), space, null);
            }

            // Zastosowujemy rotację.
            Rotation = Quaternion.RotationAxis(axis, radians)*Rotation;
        }
    }
}