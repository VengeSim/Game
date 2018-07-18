using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GameLibrary;

namespace GameServer
{
    class Program
    {
        static void Main(string[] args)
        {
            string[,,] world = new string[ 3, 3, 3];

            for (int x = 0; x < world.GetLength(0); x++)
            {
                for (int y = 0; y < world.GetLength(1); y++)
                {
                    for (int z = 0; z < world.GetLength(2); z++)
                    {                      
                        world[x, y, z] = x.ToString() + y.ToString() + z.ToString();
                    }
                }
            }

            GameObject obj = new GameObject(new GridIndex(1, 1, 1));

            //obj.RotationData.RotateXPositive();
            //obj.RotationData.RotateXPositive();
            //obj.RotationData.RotateXPositive();
            //obj.RotationData.RotateZPositive();

            obj.RotationData.RotateYPositive();
            obj.RotationData.RotateYNegitive();

            GridIndex[] frontViewPositions = new GridIndex[9];
            frontViewPositions[0] = new GridIndex(-1, 1, 1);
            frontViewPositions[1] = new GridIndex(0, 1, 1);
            frontViewPositions[2] = new GridIndex(1, 1, 1);
            frontViewPositions[3] = new GridIndex(-1, 0, 1);
            frontViewPositions[4] = new GridIndex(0, 0, 1);
            frontViewPositions[5] = new GridIndex(1, 0, 1);
            frontViewPositions[6] = new GridIndex(-1, -1, 1);
            frontViewPositions[7] = new GridIndex(0, -1, 1);
            frontViewPositions[8] = new GridIndex(1, -1, 1);

            for (int i = 0; i < frontViewPositions.GetLength(0); i++)
            {
                frontViewPositions[i].RotateAround(obj.Position, obj.RotationData);
                Console.WriteLine("{0}", frontViewPositions[i].ToString());

            }

            Console.WriteLine("{0}#{1}#{2}", obj.RotationData.Matrix.Matrix[0, 0], obj.RotationData.Matrix.Matrix[1, 0], obj.RotationData.Matrix.Matrix[2, 0]);
            Console.WriteLine("{0}#{1}#{2}", obj.RotationData.Matrix.Matrix[0, 1], obj.RotationData.Matrix.Matrix[1, 1], obj.RotationData.Matrix.Matrix[2, 1]);
            Console.WriteLine("{0}#{1}#{2}", obj.RotationData.Matrix.Matrix[0, 2], obj.RotationData.Matrix.Matrix[1, 2], obj.RotationData.Matrix.Matrix[2, 2]);
            Console.WriteLine("{0}#{1}#{2}", obj.RotationData.X, obj.RotationData.Y, obj.RotationData.Z);

            GridIndex index = new GridIndex(0, 0, -1);
            ;
            Console.WriteLine("{0}", index.RotateAround(obj.Position, obj.RotationData).ToString());

            string[,] screen = new string[3, 3];
            Console.WriteLine("{0}#{1}#{2}", screen[0, 2], screen[1, 2], screen[2, 2]);
            Console.WriteLine("{0}#{1}#{2}", screen[0, 1], screen[1, 1], screen[2, 1]);
            Console.WriteLine("{0}#{1}#{2}", screen[0, 0], screen[1, 0], screen[2, 0]);
            Console.ReadKey();


            Console.ReadKey();

        }


        public class 
    }
}
