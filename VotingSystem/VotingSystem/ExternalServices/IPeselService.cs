using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingSystem.ExternalServices
{
    public interface IPeselService
    {
        bool IsCitizenPesel(string pesel);
    }

}
