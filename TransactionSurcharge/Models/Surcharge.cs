﻿using System;
using System.IO;
using Newtonsoft.Json;

namespace TransactionSurcharge
{
    public class Surchage
    {
        /// <summary>
        /// Please Change the file Path for the configuration file
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
            string path= @"C:\Users\adeba\Desktop\parkwaypath\fees.config.json";
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


            foreach (var elem in serialized.fees)
            {
                //case of where customer payment amount is less than charges
                if (input<elem.feeAmount)
                {
                    customer = null;
                }

                if (input >= elem.minAmount && input <= elem.maxAmount)
                {
                    customer.Amount = input;
                    customer.Charge = elem.feeAmount;
                    customer.TransferAmount = input - elem.feeAmount;
                    customer.DebitAmount = customer.TransferAmount + elem.feeAmount;
                    
                }

            }
            return customer;
        }
    }
}