namespace APBD_Final_Project.UnitTests;

public class UnitTests
{
    //--NOT IMPLEMENTED--
    
    /*[Fact]
    public void PriceValueCalculatingTest()
    {
        FakeApplicationContext context = new FakeApplicationContext();
        IContractsRepository contractsRepository = new ContractsRepository(context);
        ISoftwareRepository softwareRepository = new SoftwareRepository(context);
        IContractsService contractsService = new ContractsService(contractsRepository, softwareRepository);
        
        CreateContractRequestModel requestModel = new CreateContractRequestModel
        {
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(10),
            SoftwareId = 2,
            SupportPeriodYears = 1
        };
        
        contractsService.CreateContract(1, requestModel);
        
        Assert.Equal(750, context.Contracts.First(c => c.SoftwareId == 2 && c.ClientId == 1).Price);
    }*/
    
}