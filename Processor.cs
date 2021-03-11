using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atlas2_0ML.Model;

namespace Atlas2._0
{
    static class Processor
    {
        
        
        //assign native date and time variables from home(View) to the local class variables of the same thing
        //load a machine learning algorithm stored on file
        //perform inference using a trained machine learning algorithm to predict temperature using the data from our class variables
        //Return the inference to the view for rendering logic
        // Add input data

        public static float ConvertTime(String Time)
        {
            string[] words = Time.Split(':');
            List<int> Nums = new List<int>();
            foreach (var word in words)
            {
                System.Console.WriteLine(word);
                Nums.Add(Int16.Parse(word));
            }

            return (Nums[0] / 24) + (Nums[1] / 1440) + (Nums[2] / 86400);
        }

        public static ModelOutput Predict(String Time)
        {
            
            // Add input data
            var input = new ModelInput
            {
                Time = ConvertTime(Time)
            };
            
            // Load model and predict output of sample data
            ModelOutput TempResult = ConsumeModel.Predict(input);
            

            return TempResult;

        }
        

        
        

    }
}
