using System;

namespace GameLibrary
{
    public class RotationData
    {
        //Rotations of positive, rotate clockwise looking down the positive axis - to +

        private double xRotation;
        private double yRotation;
        private double zRotation;
        private Matrix3D matrix;

        public double X { get => xRotation;}
        public double Y { get => yRotation;}
        public double Z { get => zRotation;}
        public Matrix3D Matrix { get => matrix;}

        public RotationData()
        {
            this.xRotation = 0; //rotate on vertical
            this.yRotation = 0; //rotate on horizontal
            this.zRotation = 0; //rotate on depth

            this.matrix = new Matrix3D();

        }

        public void RotateXPositive()
        {
            this.xRotation = this.xRotation + 90;
            if(this.xRotation >= 360) { this.xRotation = this.xRotation - 360; }
            this.matrix = this.matrix * Matrix3D.NewRotateFromDegreesAroundX(90);
        }
        public void RotateXNegitive()
        {
            this.xRotation = this.xRotation - 90;
            if (this.xRotation < 0) { this.xRotation = this.xRotation + 360; }
            this.matrix = this.matrix * Matrix3D.NewRotateFromDegreesAroundX(-90);
        }
        public void RotateYPositive()
        {
            this.yRotation = this.yRotation + 90;
            if (this.yRotation >= 360) { this.yRotation = this.yRotation - 360; }
            this.matrix = this.matrix * Matrix3D.NewRotateFromDegreesAroundY(90);
        }
        public void RotateYNegitive()
        {
            this.yRotation = this.yRotation - 90;
            if (this.yRotation < 0) { this.yRotation = this.yRotation + 360; }
            this.matrix = this.matrix * Matrix3D.NewRotateFromDegreesAroundY(-90);
        }
        public void RotateZPositive()
        {
            this.zRotation = this.zRotation + 90;
            if (this.zRotation >= 360) { this.zRotation = this.zRotation - 360; }
            this.matrix = this.matrix * Matrix3D.NewRotateFromDegreesAroundZ(90);
        }
        public void RotateZNegitive()
        {
            this.zRotation = this.zRotation - 90;
            if (this.zRotation < 0) { this.zRotation = this.zRotation + 360; }
            this.matrix = this.matrix * Matrix3D.NewRotateFromDegreesAroundZ(-90);
        }

        
    }
}
