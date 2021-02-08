namespace VotingSystem.ExternalServices
{
    public class MockPeselService : IPeselService
    {
        public bool IsCitizenPesel(string pesel)
        {
            return int.Parse(pesel[1].ToString()) % 2 == 0;
        }
    }

}
