using System;

namespace GameLibrary
{
    public class Matrix3D
    {

        private readonly int[,] matrix = new int[3,3];
        public int[,] Matrix { get => matrix; }
        public Matrix3D()
        {
            this.MakeIdentity();
        }

        public Matrix3D(double xRadians, double yRadians, double zRadians)
        {
            this.MakeIdentity();
            this.SetBy(NewRotate(xRadians, yRadians, zRadians));
        }

        public Matrix3D MakeIdentity()
        {
            this.matrix[0, 0] = this.matrix[1, 1] = this.matrix[2, 2] = 1;
            this.matrix[0, 1] = this.matrix[0, 2] =
                                 this.matrix[1, 0] = this.matrix[1, 2] =
                                                      this.matrix[2, 0] = this.matrix[2, 1] = 0;
            return this;
        }

        public void SetBy(Matrix3D matrix)
        {
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    this.matrix[i, j] = matrix.matrix[i, j];
                }
            }
        }

        public static Matrix3D NewRotateAroundX(double radians)
        {
            var matrix = new Matrix3D();
            matrix.matrix[1, 1] = (int)Math.Round(Math.Cos(radians));
            matrix.matrix[1, 2] = (int)Math.Round(Math.Sin(radians));
            matrix.matrix[2, 1] = (int)Math.Round(-(Math.Sin(radians)));
            matrix.matrix[2, 2] = (int)Math.Round(Math.Cos(radians));
            return matrix;
        }
        public static Matrix3D NewRotateAroundY(double radians)
        {
            var matrix = new Matrix3D();
            matrix.matrix[0, 0] = (int)Math.Round(Math.Cos(radians));
            matrix.matrix[0, 2] = (int)Math.Round(-(Math.Sin(radians)));
            matrix.matrix[2, 0] = (int)Math.Round(Math.Sin(radians));
            matrix.matrix[2, 2] = (int)Math.Round(Math.Cos(radians));
            return matrix;
        }
        public static Matrix3D NewRotateAroundZ(double radians)
        {
            var matrix = new Matrix3D();
            matrix.matrix[0, 0] = (int)Math.Round(Math.Cos(radians));
            matrix.matrix[0, 1] = (int)Math.Round(Math.Sin(radians));
            matrix.matrix[1, 0] = (int)Math.Round(-(Math.Sin(radians)));
            matrix.matrix[1, 1] = (int)Math.Round(Math.Cos(radians));
            return matrix;
        }

        public static Matrix3D NewRotate(double radiansX, double radiansY, double radiansZ)
        {
            var matrix = NewRotateAroundX(radiansX);
            matrix = matrix * NewRotateAroundY(radiansY);
            matrix = matrix * NewRotateAroundZ(radiansZ);
            return matrix;
        }

        public static Matrix3D NewRotateByDegrees(double degreesX, double degreesY, double degreesZ)
        {
            return NewRotate(
                        Matrix3D.DegreesToRadians(degreesX),
                        Matrix3D.DegreesToRadians(degreesY),
                        Matrix3D.DegreesToRadians(degreesZ)
                   );
        }

        public static Matrix3D NewRotateFromDegreesAroundX(double degrees)
        {
            return NewRotateAroundX(Matrix3D.DegreesToRadians(degrees));
        }
        public static Matrix3D NewRotateFromDegreesAroundY(double degrees)
        {
            return NewRotateAroundY(Matrix3D.DegreesToRadians(degrees));
        }
        public static Matrix3D NewRotateFromDegreesAroundZ(double degrees)
        {
            return NewRotateAroundZ(Matrix3D.DegreesToRadians(degrees));
        }

        public static Matrix3D operator *(Matrix3D matrix1, Matrix3D matrix2)
        {
            var matrix = new Matrix3D();
            for(var i = 0; i < 3; i++)
            {
                for(var j = 0; j < 3; j++)
                {
                    matrix.matrix[i, j] = 
                        (matrix2.matrix[i, 0] * matrix1.matrix[0, j]) +
                        (matrix2.matrix[i, 1] * matrix1.matrix[1, j]) +
                        (matrix2.matrix[i, 2] * matrix1.matrix[2, j]);
                }
            }
            return matrix;
        }

        public static GridIndex operator *(Matrix3D matrix1, GridIndex index)
        {
            int x = index.X * matrix1.matrix[0, 0] +
                    index.Y * matrix1.matrix[0, 1] +
                    index.Z * matrix1.matrix[0, 2];
            int y = index.X * matrix1.matrix[1, 0] +
                    index.Y * matrix1.matrix[1, 1] +
                    index.Z * matrix1.matrix[1, 2];
            int z = index.X * matrix1.matrix[2, 0] +
                    index.Y * matrix1.matrix[2, 1] +
                    index.Z * matrix1.matrix[2, 2];

            return new GridIndex(x, y, z);
        }

        public static double DegreesToRadians(double degrees)
        {
            return (Math.PI / 180) * degrees;
        }

    }
}