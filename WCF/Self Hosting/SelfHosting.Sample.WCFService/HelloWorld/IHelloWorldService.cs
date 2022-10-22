using System;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace SelfHosting.Sample.WCFService
{
    [ServiceContract]
    public interface IHelloWorldService
    {
        [OperationContract]
        string SayHello(string name);

        [OperationContract]
        string SaveUser(User user);
    }


    [DataContract]
    public class User
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public DateTime DateOfBirth { get; set; }
    }
}
