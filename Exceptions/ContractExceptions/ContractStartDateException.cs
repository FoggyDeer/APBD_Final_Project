namespace APBD_Final_Project.Exceptions.ContractExceptions;

public class ContractStartDateException() : Exception("Start date cannot exceed the end date. Start date must be in the future.");