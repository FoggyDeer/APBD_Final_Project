namespace APBD_Final_Project.Exceptions.ContractExceptions;

public class ContractExpiredException(int contractId) : Exception($"Contract with id: '{contractId}' has expired");