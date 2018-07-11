namespace GameLibrary
{
    public class GameObject
    {
        private GridIndex position;
        private RotationData rotationData;

        public GridIndex Position { get => position;}
        public RotationData RotationData { get => rotationData;}

        public GameObject(GridIndex position)
        {
            this.position = position;
            this.rotationData = new RotationData();
        }

        public GameObject(GridIndex position, RotationData rotationData)
        {
            this.position = position;
            this.rotationData = rotationData;
        }


    }
}
