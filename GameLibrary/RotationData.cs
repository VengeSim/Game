using System;
using System.Windows.Media.Media3D;

namespace GameLibrary
{
    public class RotationData
    {
        private double heading;
        private double attitude;
        private double bank;
        private int[,] matrix;

        public double Heading { get => heading;}
        public double Attitude { get => attitude;}
        public double Bank { get => bank;}
        public int[,] Matrix { get => matrix;}

        public RotationData()
        {
            this.heading = 0; //rotate on vertical
            this.attitude = 0; //rotate on horizontal
            this.bank = 0; //rotate on depth

            this.matrix = new int[3,3];
            this.ReCalculateMatrix();
        }

        private void ReCalculateMatrix()
        {
            //|| ch * ca    || -ch * sa * cb + sh * sb  || ch * sa * sb + sh * cb
            //|| sa         || ca * cb                  || -ca * sb
            //|| -sh * ca   || sh * sa * cb + ch * sb   || -sh * sa * sb + ch * cb

            //sa = sin(attitude)
            //ca = cos(attitude)
            //sb = sin(bank)
            //cb = cos(bank)
            //sh = sin(heading)
            //ch = cos(heading)

            double headingRadians = Math.PI / 180.0 * this.heading ;
            double attitudeRadians = Math.PI / 180.0 * this.attitude;
            double bankRadians = Math.PI / 180.0 * this.bank;

            //int[,] newMatrix = new int[3,3];

            this.matrix[0,0] = (int)Math.Round(Math.Cos(headingRadians) * Math.Cos(attitudeRadians));
            this.matrix[1,0] = (int)Math.Round(-Math.Cos(headingRadians) * Math.Sin(attitudeRadians) * Math.Cos(bankRadians) + Math.Sin(headingRadians) * Math.Sin(bankRadians));
            this.matrix[2,0] = (int)Math.Round(Math.Cos(headingRadians) * Math.Sin(attitudeRadians) * Math.Sin(bankRadians) + Math.Sin(headingRadians) * Math.Cos(bankRadians));

            this.matrix[0,1] = (int)Math.Round(Math.Sin(attitudeRadians));
            this.matrix[1,1] = (int)Math.Round(Math.Cos(attitudeRadians) * Math.Cos(bankRadians));
            this.matrix[2,1] = (int)Math.Round(-Math.Cos(attitudeRadians) * Math.Sin(bankRadians));

            this.matrix[0,2] = (int)Math.Round(-Math.Sin(headingRadians) * Math.Cos(attitudeRadians));
            this.matrix[1,2] = (int)Math.Round(Math.Sin(headingRadians) * Math.Sin(attitudeRadians) * Math.Cos(bankRadians) + Math.Cos(headingRadians) * Math.Sin(bankRadians));
            this.matrix[2,2] = (int)Math.Round(-Math.Sin(headingRadians) * Math.Sin(attitudeRadians) * Math.Sin(bankRadians) + Math.Cos(headingRadians) * Math.Cos(bankRadians));

            //this.matrix[0, 0] = this.matrix[0, 0] * newMatrix[0, 0];
            //this.matrix[1, 0] = this.matrix[1, 0] * newMatrix[1, 0];
            //this.matrix[2, 0] = this.matrix[2, 0] * newMatrix[2, 0];

            //this.matrix[0, 1] = this.matrix[0, 1] * newMatrix[0, 1];
            //this.matrix[1, 1] = this.matrix[1, 2] * newMatrix[1, 1];
            //this.matrix[2, 1] = this.matrix[2, 2] * newMatrix[2, 1];

            //this.matrix[0, 2] = this.matrix[0, 2] * newMatrix[0, 2];
            //this.matrix[1, 2] = this.matrix[1, 2] * newMatrix[1, 2];
            //this.matrix[2, 2] = this.matrix[2, 2] * newMatrix[2, 2];
        }

        public void HeadingLeft()
        {
            this.heading = this.heading + 90;
            if(this.heading >= 360) { this.heading = this.heading - 360; }
            this.ReCalculateMatrix();
        }
        public void HeadingRight()
        {
            this.heading = this.heading - 90;
            if (this.heading < 0) { this.heading = this.heading + 360; }
            this.ReCalculateMatrix();
        }
        public void AttitudeUp()
        {
            this.attitude = this.attitude + 90;
            if (this.attitude >= 360) { this.attitude = this.attitude - 360; }
            this.ReCalculateMatrix();
        }
        public void AttitudeDown()
        {
            this.attitude = this.attitude - 90;
            if (this.attitude < 0) { this.attitude = this.attitude + 360; }
            this.ReCalculateMatrix();
        }
        public void BankLeft()
        {
            this.bank = this.bank + 90;
            if (this.bank >= 360) { this.bank = this.bank - 360; }
            this.ReCalculateMatrix();
        }
        public void BankRight()
        {
            this.bank = this.bank - 90;
            if (this.bank < 0) { this.bank = this.bank + 360; }
            this.ReCalculateMatrix();
        }

    }
}
