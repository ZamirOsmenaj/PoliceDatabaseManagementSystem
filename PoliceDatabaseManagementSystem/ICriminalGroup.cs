using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceDatabaseManagementSystem {
    internal interface ICriminalGroup {
        string Name { get; }
        IEnumerable<ICriminalRecord> ParticipatingCriminals { get; }
    }
}
