using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceDatabaseManagementSystem {
    internal class CrimesInsider3rdPartyCompany : ICrimesInsider3rdPartyCompany {

        // Readonly field to store the reference to the police criminal database.
        private readonly IPoliceCriminalDatabase policeDatabase;

        public CrimesInsider3rdPartyCompany(IPoliceCriminalDatabase policeDatabase) {
            this.policeDatabase = policeDatabase;
        }

        public ICriminalRecord GetMostDangerousCriminal() {
            // Orders the criminal records by accusation severity in descending order and returns the first record.
            return policeDatabase.CriminalRecords.OrderByDescending(c => c.AccusationDetails.Severity).FirstOrDefault();
        }

        public IEnumerable<ICriminalRecord> GetCriminalsSortedBySeverity() {
            // Orders the criminal records by accusation severity and returns the sorted collection.
            return policeDatabase.CriminalRecords.OrderBy(c => c.AccusationDetails.Severity);
        }

        // Inside the method, average age is calculated using LINQ's Select method.
        // It iterates over each CriminalRecord in the database.CriminalRecords collection.
        // For each criminal record c, it calculates the difference between the current date (DateTime.Now)
        // and the criminal's birth date(c.BirthDate) and converts it to total days using TotalDays.
        // This gives the total age of all criminals in days.
        // As it is difficult to figure out someone's age by saying how many days have passed since their birth (e.g. 14600 days),
        // we convert days to years by dividing by 365.25
        public double GetAverageAgeOfCriminals() {
            return policeDatabase.CriminalRecords.Select(c => (DateTime.Now - c.BirthDate).TotalDays / 365.25).Average();
        }

        public ICriminalRecord GetMostConnectedCriminal() {
            // Dictionary to store the count of connections for each criminal.
            Dictionary<ICriminalRecord, int> connectionsCount = new Dictionary<ICriminalRecord, int>();

            // Count connections for each criminal
            foreach (var criminal in policeDatabase.CriminalRecords) {
                int connections = CountGroupConnections(criminal);
                connectionsCount.Add(criminal, connections);
            }

            // Find criminal with maximum connections
            var mostConnectedCriminal = connectionsCount.OrderByDescending(x => x.Value).FirstOrDefault();

            return mostConnectedCriminal.Key;
        }

        // Helper method to count group connections for a specific criminal.
        private int CountGroupConnections(ICriminalRecord criminal) {
            // Count how many groups the criminal is affiliated with
            int connections = 0;
            foreach (var otherCriminal in policeDatabase.CriminalRecords) {
                if (otherCriminal != criminal && otherCriminal.GroupAffiliation != null && otherCriminal.GroupAffiliation.ParticipatingCriminals.Contains(criminal)) {
                    connections++;
                }
            }
            return connections;
        }
    }
}
