using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistribuidorDeColas
{
    public class GestorColasAzure
    {
        CloudStorageAccount cuenta;
        CloudQueueClient cliente;
        CloudQueue cola;

        public GestorColasAzure(string nombreCola)
        {
            cuenta = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            cliente = cuenta.CreateCloudQueueClient();
            cola = cliente.GetQueueReference(nombreCola);
            cola.CreateIfNotExists();
        }

        public void Agregar(Notificacion notificacion)
        {
            CloudQueueMessage message = new CloudQueueMessage(notificacion.TextoMensaje);
            cola.AddMessage(message);
        }

        public void Agregar(List<Notificacion> notificaciones)
        {
            foreach (var n in notificaciones)
            {
                CloudQueueMessage message = new CloudQueueMessage(n.TextoMensaje);
                cola.AddMessage(message);
            }
        }

        public bool TieneElementosEnCola()
        {
            int? count = cola.ApproximateMessageCount;
            return count.HasValue && count.Value > 0;
        }


    }
}

/*
     // Retrieve storage account from connection string.
    CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
        CloudConfigurationManager.GetSetting("StorageConnectionString"));

    // Create the queue client.
    CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

    // Retrieve a reference to a queue.
    CloudQueue queue = queueClient.GetQueueReference("myqueue");

    // Create the queue if it doesn't already exist.
    queue.CreateIfNotExists();

    // Create a message and add it to the queue.
    CloudQueueMessage message = new CloudQueueMessage("Hello, World");
    queue.AddMessage(message);
     
*/
