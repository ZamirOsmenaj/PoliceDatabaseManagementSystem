using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceDatabaseManagementSystem {
    internal sealed class Accusation : IAccusation {
        public CrimeType Type { get; }
        public CrimeSeverity Severity { get; }
        public string Description { get; }
        public DateTime DateOccurred { get; }

        public Accusation(CrimeType type, CrimeSeverity severity, string description, DateTime dateOccurred) {
            Type = type;
            Severity = severity;
            Description = description;
            DateOccurred = dateOccurred;
        }
    }
}
