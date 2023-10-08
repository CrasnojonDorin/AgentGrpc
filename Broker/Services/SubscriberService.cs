using Broker.Models;
using Broker.Services.Interfaces;
using Grpc.Core;
using GrpcAgent;
using System;
using System.Threading.Tasks;

namespace Broker.Services
{
    public class SubscriberService:Subscriber.SubscriberBase
    {
        private readonly IConnectionStorageService _connectionStorage;
        public SubscriberService(IConnectionStorageService connectionStorage)
        {
            _connectionStorage = connectionStorage;    
        }

        public override Task<SubscribeReply> Subscribe(SubscribeRequest request, ServerCallContext context)
        {
            Console.WriteLine($"New client trying to subscribe: {request.Address} {request.Topic}");

            try
            {
                var connection = new Connection(request.Address, request.Topic);

                _connectionStorage.Add(connection);
            }

            catch(Exception ex) {
                Console.WriteLine($"Could not add the new connection {request.Address} {request.Topic} {ex.Message}");
            
            }
          

            
            return Task.FromResult(new SubscribeReply()
            {
                IsSuccess = true,
            });
        }

    }
}
