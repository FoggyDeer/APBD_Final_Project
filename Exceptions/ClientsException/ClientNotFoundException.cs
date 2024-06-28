namespace APBD_Final_Project.Exceptions.ClientsException;

public class ClientNotFoundException(int clientId) : Exception($"You aren not a client.");