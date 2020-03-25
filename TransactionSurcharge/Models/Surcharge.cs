using System;
using System.IO;
using Newtonsoft.Json;

namespace TransactionSurcharge
{
    public class Surchage
    {
        /// <summary>
        /// Please Change the file Path for the configuration file
        /// file type is json
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public CustomerModel FileReader(int input)
        {

            //incase the input value is less than minimum transaction level
            if (input <= 0)
            {
                return null;
            }

            //please change the file path to use this program
            string path= @"C:\Users\adeba\desktop\parkwaypath\fees.config.json";
            //path to where configuration file is located

            string filePath = path;

            string fileContent = String.Empty;

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

            
            var customer = new CustomerModel();

            int checker = 0;

            foreach (var elem in serialized.fees)
            {
                //case of where customer payment amount is less than charges
                if (input <= elem.feeAmount)
                {
                    customer = null;
                    break;
                }
                if (input >= elem.minAmount && input <= elem.maxAmount)
                {
                    checker = input - elem.feeAmount;
                }

                customer.Amount = input;

                customer.TransferAmount = checker;
            }

            foreach (var elem in serialized.fees)
            {

                if ( checker>= elem.minAmount && checker <= elem.maxAmount)
                {


                 
                    customer.Charge = elem.feeAmount;
                 
                    customer.DebitAmount = customer.TransferAmount + elem.feeAmount;
                    
                }

            }

            return customer;
        }
    }
}