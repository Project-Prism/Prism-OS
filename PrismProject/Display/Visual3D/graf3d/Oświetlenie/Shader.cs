using System;
using graf3d.Engine.Abstractions;
using graf3d.Engine.Component;
using graf3d.Engine.Structure;

namespace graf3d.Engine.Oświetlenie
{
    public class Light
    {
        public float AttenuationCutoff = 5;
        public bool AttenuationOn = false;
        public Color Color = new Color(1f, 1f, 1f, 1f);
        public Vector3 Position = new Vector3(0.0f, 10.0f, 13.0f);
        public float Radius = 1f;
    }

    public enum IlluminationObject
    {
        Sphere,
        Rectangle
    }

    public interface ISurfaceShader
    {
        bool Compute(float x, float y, ref Vector3 normal, ref Vector3 surfacePos);
    }

    public class Sphere : ISurfaceShader
    {
        public bool Compute(float x, float y, ref Vector3 normal, ref Vector3 surfacePos)
        {
            var radius = 0.7f;
            var z = (float) Math.Sqrt(radius - x*x - y*y);

            if (double.IsNaN(z))
            {
                return false;
            }

            normal = new Vector3(x, y, z);
            surfacePos = normal;
            return true;
        }
    }

    public class Plane : ISurfaceShader
    {
        public bool Compute(float x, float y, ref Vector3 normal, ref Vector3 surfacePos)
        {
            // WEKTOR NORMALNY POWIERZCHNI
            surfacePos = new Vector3(x, y, 0);
            normal = Vector3.TransformCoordinate(Vector3.UnitZ*-1,
                Matrix.RotationQuaternion(Quaternion.RotationAxis(Vector3.UnitY, 2f)));
            return true;
        }
    }

    public class PixelShader
    {
        protected readonly IBufferedBitmap Bitmap;
        protected readonly double HalfHeight;
        protected readonly double HalfWidth;

        public PixelShader(IBufferedBitmap bitmap)
        {
            Bitmap = bitmap;
            HalfWidth = bitmap.PixelWidth*0.5;
            HalfHeight = bitmap.PixelHeight*0.5;
        }

        // Kierunek patrzenia
        //public Vector3 LookDirection => 

        public void Illuminate(Scene scene)
        {
            PrismProject.Display.Visual2D.DisplayConfig.Controler.Clear(System.Drawing.Color.Black);

            var normal = Vector3.Zero;
            var surfacePos = Vector3.Zero;
            var lookDirection = scene.Camera.LookAtLH().GetAxis(Axis.Z);

            for (var x = 0; x < Bitmap.PixelWidth; ++x)
            {
                for (var y = 0; y < Bitmap.PixelHeight; ++y)
                {
                    var xf = (float) (-1.0 + x/HalfWidth);
                    var yf = (float) (1.0 - y/HalfHeight);
                    // Wektor normalny do powierzchni modelu
                    if (!scene.SurfaceShader.Compute(xf, yf, ref normal, ref surfacePos))
                    {
                        // Nie znaleziono normalnej dla danych współrzędnych.
                        continue;
                    }

                    normal = scene.Material.MapNormal(x, y, normal).Normalize();

                    var finalColor = new Color();
                    foreach (var light in scene.Lights)
                    {
                        finalColor += IlluminatePixel(surfacePos, normal, lookDirection, light, scene.Material);
                    }

                    PrismProject.Display.Visual2D.DisplayConfig.Controler.DrawPoint(new Cosmos.System.Graphics.Pen(System.Drawing.Color.FromArgb((int)finalColor.A, (int)finalColor.R, (int)finalColor.G, (int)finalColor.B)), x, y);
                }
            }

            Bitmap.Present();
        }

        private Color IlluminatePixel(Vector3 surfacePosition, Vector3 normal, Vector3 observerDir, Light light,
            Material material)
        {
            var lightVector = light.Position - surfacePosition;
            var distance = lightVector.Length();
            var atten = light.AttenuationOn ? Attenuation(light.Radius, light.AttenuationCutoff, distance) : 1;

            // Kierunek padania światła
            lightVector = lightVector.Normalize();

            // AMBIENT
            var color = atten*material.Ambient*light.Color;
            // DIFFUSE
            var df = Vector3.Dot(normal, lightVector).Saturate();
            color += atten*df*material.Diffuse*light.Color;
            // SPECULAR
            var sf = material.SpecularAlgorithm.Specular(normal, observerDir, lightVector, material.Shininess);
            color += atten*sf*material.Specular;
            // EMISSIVE
            color += material.Emissive;

            return color;
        }


        private static float Attenuation(float r, float f, float d)
        {
            return (float) Math.Pow(Math.Max(0.0, 1.0 - d/r), f + 1.0);
        }
    }
}