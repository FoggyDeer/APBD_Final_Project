namespace APBD_Final_Project.Exceptions.ClientsException.Individual;

public class ClientDeletedException(int clientId) : Exception($"Client with id {clientId} has been deleted");