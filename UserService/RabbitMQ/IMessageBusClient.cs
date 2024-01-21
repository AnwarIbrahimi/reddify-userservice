using System.Security.Cryptography;
using UserService.DTO;

namespace UserService.RabbitMQ
{
    public interface IMessageBusClient
    {
        void PublishUserDeletion(DeleteUserDTO deleteUserDto);
    }
}
