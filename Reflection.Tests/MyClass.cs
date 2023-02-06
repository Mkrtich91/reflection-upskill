namespace Reflection.Tests
{
    public class MyClass
    {
        public const int StaticIntField = 10;

        private int intField;

        public MyClass()
        {
            this.intField = -1;
        }

        public MyClass(int intValue)
        {
            this.intField = intValue;
        }

        public int IntValue
        {
            get
            {
                return this.intField;
            }

            set
            {
                this.intField = value < 0 ? 0 : value;
            }
        }
    }
}
