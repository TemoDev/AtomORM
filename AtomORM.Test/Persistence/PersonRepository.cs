namespace AtomORM.Test.Persistence;

public class PersonRepository
{
    private readonly TestAtomContext _testAtomContext;
    public PersonRepository(TestAtomContext testAtomContext)
    {
        _testAtomContext = testAtomContext;
    }

    public void ReadValues()
    {
        _testAtomContext.stringProperty.LoadEntity();
    }
    
}