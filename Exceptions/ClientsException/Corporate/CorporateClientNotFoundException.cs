namespace APBD_Final_Project.Exceptions.ClientsException.Corporate;

public class CorporateClientNotFoundException(int clientId) : Exception($"Corporate client with id: '{clientId}' not found");