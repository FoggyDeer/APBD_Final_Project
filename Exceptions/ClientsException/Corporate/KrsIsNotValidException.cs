namespace APBD_Final_Project.Exceptions.ClientsException.Corporate;

public class KrsIsNotValidException(string krs) : Exception($"Client with KRS: '{krs}' already exists");