namespace TP4
{
    public class ConsoleMain
    {
        public static void Main(string[] args)
        {
            Test testStripesSpace = new Test("StripSpace");
            testStripesSpace.AddAssert(new AssertEqual<string>("yolo", "abc", "abc"));
            testStripesSpace.Run();
            
            Test testSwap = new Test("Swap");
            
            Test testSort = new Test("Sort");
            int[] array = new int[5];
            testSort.AddAssert(new AssertArrayEqual<int>("yoloassert",));
        }
    }
}