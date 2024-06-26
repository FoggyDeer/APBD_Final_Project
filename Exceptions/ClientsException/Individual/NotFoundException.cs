namespace APBD_Final_Project.Exceptions.ClientsException.Individual;

public class NotFoundException(int clientId) : Exception($"Individual client with id: '{clientId}' not found");