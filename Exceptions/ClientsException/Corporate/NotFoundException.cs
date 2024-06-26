namespace APBD_Final_Project.Exceptions.ClientsException.Corporate;

public class NotFoundException(int clientId) : Exception($"Corporate client with id: '{clientId}' not found");