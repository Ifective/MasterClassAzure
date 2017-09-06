using System.Threading.Tasks;
using Logic.Model;
using Logic.Settings;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;

namespace Logic.Services
{
    public class QueueLogic : IQueueLogic
    {
        private readonly IOptions<StorageSettings> _storageSettings;

        public QueueLogic(IOptions<StorageSettings> storageSettings)
        {
            _storageSettings = storageSettings;
        }

        public async Task AddStudent(Student student)
        {
            var storageAccount = CloudStorageAccount.Parse(_storageSettings.Value.ConnectionString);

            var queueClient = storageAccount.CreateCloudQueueClient();

            var queue = queueClient.GetQueueReference("queue");

            await queue.CreateIfNotExistsAsync();
            var serializedStudent = JsonConvert.SerializeObject(student);
            var message = new CloudQueueMessage(serializedStudent);
            await queue.AddMessageAsync(message);
        }
    }
}
