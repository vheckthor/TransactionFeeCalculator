using Newtonsoft.Json;
using System;
using System.IO;


namespace TransactionFeeCalculator
{
    class Program
    {
        /// <summary>
        /// Please change the file path to use or test the application
        /// the format chosen is Json.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {

            //program runs until it is closed.
            while (true)
            {
                Console.Write("Please input the Transaction Amount: ");
                var input = Console.ReadLine();

                var inputValue = int.TryParse(input, out int result);

                //for extremely large value that are above integer range or incorrect input.
                if (!inputValue)
                {
                    Console.WriteLine($"The input details is incorrect please check your input");
                    continue;

                }
                //please change the path below to your file path to test the code
                string pathToFile = @"C:\Users\path\path\path\fees.config.json";
                
                var content = FileReader(pathToFile, result);

                Console.Write("Your Charges is: ");
                Console.WriteLine(content); 
            }

        }

        static int FileReader(string path,int input)
        {

            //incase the input value is less than minimum transaction level
            if (input <= 0)
            {
                return 0;
            }


            //path to where configuration file is located
            string filePath = path;

            string fileContent=String.Empty;

            try
            {
                StreamReader reader = new StreamReader(filePath);
                fileContent = reader.ReadToEnd();
            }
            catch (IOException e)
            {
                //console can be repaced with a logger to log error
                Console.WriteLine(e);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


            
            //deserialized file from configuration
            FeesAndCharges serialized;
            try
            { 
                serialized = JsonConvert.DeserializeObject<FeesAndCharges>(fileContent);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            int feeToPay = 0;
            

            foreach (var elem in serialized.fees)
            {
                if (input>=elem.minAmount && input <= elem.maxAmount)
                {
                    feeToPay = elem.feeAmount;
                }

            }
            return feeToPay;
        }
    }
}
