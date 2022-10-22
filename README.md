# WCF Samples
This repository has samples for both WCF and CoreWCF. All samples include both the Service and Client implementation.

Repository structure
- WCF
  - Self Hosting (BasicHttpBinding, WebHttpBinding)
  - IIS Hosting
- CoreWCF

Please refer to README file in each folder for more specific instructions.

## Understanding WCF Basics
Let's quickly understand WCF basic concepts by creating a WCF service in Visual Studio.

Open **Visual Studio**, select a **New Project**, and then select **WCF Service Library** template.

![image](https://user-images.githubusercontent.com/13661966/196370837-63eba4d5-b1e5-4c4a-bebd-ae6590e50f81.png)

## `system.serviceModel` section
The new WCF project that we just created, generates an **app.config** file, that contains **system.serviceModel** section. This section is the most important section in the configuration file from WCF point of view. This section is container for all WCF related configurations.

This sction further includes 3 major sections - `bindings`, `services` and `behavious`.
```xml
<configuration>  
    <system.serviceModel>  
        <bindings>  
        </bindings>  
        <services>  
        </services>  
        <behaviors>  
        </behaviors>  
    </system.serviceModel>  
</configuration>
```
Let's understand them one by one.

### `services` section
When you create a WCF Service, you basically create a C# class, known as `ServiceContract`, that ServiceContract further has a few methods known as **OperationContracts**. 

The `<services/>` section defines all the service contracts that you have in your application, where each service contract has an **endpoint**.

An **EndPoint** includes 3 things, which are also known as ABC of WCF.
- Address
- Binding
- Contract

An example illustrates how to specify an endpoint configured with an address, a contract, and a binding.
```
<service name="HelloWorld, IndigoConfig, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null">  
  <!-- This section is optional with the default configuration introduced  
       in .NET Framework 4. -->  
  <endpoint
      address="/HelloWorld2/"  
      contract="HelloWorld, IndigoConfig, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null"  
      binding="basicHttpBinding" />
</service>
```

#### Address
Basically URL, specifies where this WCF service is hosted. Client will use this url to connect to the service.
```
http://localhost:8090/MyService/SimpleCalculator.svc
```

#### Binding
Bindings specify the communication mechanism to use when talking to an endpoint and indicate how to connect to an endpoint.

> Bindings are also very important when we have customers wanted to use same WCF Service with different transports. Consider a scenario say, I am creating a service that has to be used by two type of client. One of the client will access SOAP using http and other client will access Binary using TCP. How it can be done? With Web service it is very difficult to achieve, but in WCF its just we need to add extra endpoint in the configuration file.

A binding contains the following elements:
- **Transport** - Determines the underlying transport protocol to use (for example, TCP or HTTP).
- **Encoding (Optional)** - Determines the message encoding (for example, text or binary).
- **Protocol(Optional)** - Determines the security mechanism being used (for example, reliable messaging, transaction support)

An endpoint that does not explicitly select a binding uses the default binding selection, which is `BasicHttpBinding`.

#### Contract
The contract attribute specifies the contract this endpoint is exposing. The service implementation type must implement the contract type. 


### `behaviors` section
With the help of behaviours, we can attach extra features and functionalities to an existing service. It allows you to control things such as: 
- Concurrency
- Throttling
- Transaction
- Session Management
- Thread Behavior

Behaviour can be applied at Service, Operation, Contract or EndPoint level. 

Foe example, `serviceMetadata` behavior to instruct the host to publish the service’s metadata over HTTP-GET or to implement the MEX endpoint.

### `bindings` section
The bindings element contains the specifications for all bindings that can be used by any endpoint defined in any service.

Here is a list of [System-provided bindings](https://learn.microsoft.com/en-us/dotnet/framework/wcf/system-provided-bindings).

## Hosting a WCF Service in a Console Application
To self-host a WCF in Console application, you can use **ServiceHost** class and configure it like below.
```
class Program
{
    static void Main(string[] args)
    {
        Uri baseAddress = new Uri("http://localhost:8080/hello");

        // Create the ServiceHost.
        using (ServiceHost host = new ServiceHost(typeof(HelloWorldService), baseAddress))
        {
            // Enable metadata publishing.
            ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
            smb.HttpGetEnabled = true;
            smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
            host.Description.Behaviors.Add(smb);

            // Open the ServiceHost to start listening for messages. Since
            // no endpoints are explicitly configured, the runtime will create
            // one endpoint per base address for each service contract implemented
            // by the service.
            host.Open();

            Console.WriteLine("The service is ready at {0}", baseAddress);
            Console.WriteLine("Press <Enter> to stop the service.");
            Console.ReadLine();

            // Close the ServiceHost.
            host.Close();
        }
    }
}
```
Here are a few links that shows how to host a WCF application inside a a Console application:
- [Self-Host](https://learn.microsoft.com/en-us/dotnet/framework/wcf/samples/self-host)
- [Create Simple WCF Service And Host It On Console Application](https://www.c-sharpcorner.com/article/create-simple-wcf-service-and-host-it-on-console-application/)
- [How to: Host a WCF service in a managed app](https://learn.microsoft.com/en-us/dotnet/framework/wcf/how-to-host-a-wcf-service-in-a-managed-application)

## Configure WCF service to support multiple bindings
A WCF application can be configured to support multiple bindings. All we need to do is just add an extra endpoint with different binding.

Configurations in WCF can be managed either through the config file or programatically through the code.

## Creating RestFul service using `WebHttpBinding` binding
- [WCF - Using WebHttpBinding for REST services](https://weblogs.asp.net/kiyoshi/wcf-using-webhttpbinding-for-rest-services)
- [How to create RESTFul services](http://wcftutorial.net/How_to_create_RESTful_Service.aspx)