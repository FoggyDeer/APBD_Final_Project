namespace APBD_Final_Project.Exceptions.ClientsException.Individual;

public class ClientDeletedException : Exception
{
    public static string SelfMessage = "This client has been deleted";
    public ClientDeletedException(int clientId) : base($"Client with id {clientId} has been deleted")
    {
    }
    
    public ClientDeletedException(string message) : base(message)
    {
    }
}