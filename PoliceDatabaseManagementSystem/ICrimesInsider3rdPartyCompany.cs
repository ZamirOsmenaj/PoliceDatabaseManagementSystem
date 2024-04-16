using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceDatabaseManagementSystem {
    internal interface ICrimesInsider3rdPartyCompany {
        ICriminalRecord GetMostDangerousCriminal();
        IEnumerable<ICriminalRecord> GetCriminalsSortedBySeverity();
        ICriminalRecord GetMostConnectedCriminal();
        double GetAverageAgeOfCriminals();
    }
}
