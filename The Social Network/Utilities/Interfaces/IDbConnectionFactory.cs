using System.Data;

namespace The_Social_Network.Utilities.Interfaces
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}