using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Text.Json;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.Azure.ServiceBus;
using System.Text;
using System;
using Azure.Storage.Blobs;

namespace Caseoppgave
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static void Run([BlobTrigger("caseoppgave/{name}", Connection = "AzureWebJobsStorage")] Stream myBlob, string name, ILogger log)
        {
            // Azure connection
            string sbConnectionString = "Endpoint=sb://caseoppgave.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=8TYbfXHXTcmeoBxtcJ5YaVC0givcjRsnHzLICn/kFIc=";
            string sbTopic = "personinfo";
            log.LogInformation($"C# Blob triggered ---- with blob: {name} ----");
            // Initializing variabels
            string lineInText;
            string jsonString;
            List<Person> personasList = new List<Person>();

            // Reading the file inserted in the blob container
            StreamReader fileText = new StreamReader(myBlob);
            
            // Skipping first line
            fileText.ReadLine();
            
            // Generating and Inserting Person into a list
            while ((lineInText = fileText.ReadLine()) != null)
            {
                string[] words = lineInText.Split(";");
                Person person = new Person { Fornavn = words[0], Etternavn = words[1], Tittel = words[2] };
                personasList.Add(person);
            }

            // Lopping through the generated list with object Person and sending each Person to Service Bus

            foreach (Person item in personasList)
            {
                jsonString = JsonSerializer.Serialize(item);
                string messageBody = jsonString;
                TopicClient topC = new TopicClient(sbConnectionString, sbTopic);
                var message = new Message(Encoding.UTF8.GetBytes(messageBody));
                log.LogInformation($"--------  Message Published: {messageBody}  --------");
                topC.SendAsync(message);
            }
        }
    }

    class Person
    {
        public string Fornavn { get; set; }
        public string Etternavn { get; set; }
        public string Tittel { get; set; }
    }
}
