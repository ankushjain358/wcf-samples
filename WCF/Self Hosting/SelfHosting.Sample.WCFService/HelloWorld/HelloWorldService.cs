using Newtonsoft.Json;

namespace SelfHosting.Sample.WCFService
{
    public partial class WcfEntryPoint : IHelloWorldService
    {
        public string SayHello(string name)
        {
            return $"Hello {name}!";
        }

        public string SaveUser(User user)
        {
            return JsonConvert.SerializeObject(user);
        }
    }
}
