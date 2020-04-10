namespace Week_11_HashFunctions
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            //For testing a smaller file
            var text = TextProcessor.ReadText(@"C:\Users\s_ele\RiderProjects\Algoritmer og Datastrukturer\Week_11_HashFunctions\test-text.txt");
            
            // Reading the whole text from file to a string
            var text2 = TextProcessor.ReadText(); //For doing the sort on the complete text
            
            //Taking all words from the string and putting them in a string-array
            var words = TextProcessor.SanitizeText(text);
            
            //
            

            //Use the below line to print out the finished sorted string
            // Console.WriteLine(stringSorted);
        }
    }
}