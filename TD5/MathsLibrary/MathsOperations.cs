using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
namespace MathsLibrary
{
    public class MathsOperations : IMathsOperations
    {
        public int Add(int num1, int num2)
        {
            return (num1 + num2);
        }

        public int Divide(int num1, int num2)
        {
            if (num2 == 0)
            {
                CustomFaultDetails ex = new CustomFaultDetails();
                ex.ErrorID = "12345";
                ex.ErrorDetails = "Specific error details here.";
                throw new FaultException<CustomFaultDetails>(ex, "Reason: Testing");
            }
            return num1 / num2;
        }

        public int Multiply(int num1, int num2)
        {
            return (num1 * num2);
        }
    }
}