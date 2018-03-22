using System.Runtime.Serialization;

namespace MathsLibrary
{
    public class CustomFaultDetails
    {
        [DataMember]
        public string ErrorID { get; set; }
        public string ErrorDetails { get; set; }
    }
}