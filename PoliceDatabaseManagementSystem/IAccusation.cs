using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceDatabaseManagementSystem {
    public enum CrimeType { Theft, Assault, Fraud, Homicide }
    public enum CrimeSeverity { Low, Medium, High, LifeThreatening }

    internal interface IAccusation {
        CrimeType Type { get; }
        CrimeSeverity Severity { get; }
        string Description { get; }
        DateTime DateOccurred { get; }
    }
}
