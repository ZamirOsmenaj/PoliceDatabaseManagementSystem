using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceDatabaseManagementSystem {
    // This class represents a database managed by the police department to store criminal records and criminal groups.
    internal sealed class PoliceCriminalDatabase : IPoliceCriminalDatabase {

        // Private fields to store the lists of criminal records and criminal groups.
        private List<CriminalRecord> _criminalRecords;
        private List<CriminalGroup> _criminalGroups;

        // Public properties exposing the collections of criminal records and criminal groups as IEnumerable.
        public IEnumerable<ICriminalRecord> CriminalRecords => _criminalRecords;
        public IEnumerable<ICriminalGroup> CriminalGroups => _criminalGroups;

        public PoliceCriminalDatabase() {
            _criminalRecords = new List<CriminalRecord>();
            _criminalGroups = new List<CriminalGroup>();
        }

        // Methods to modify database's criminal records.
        public void InsertCriminalRecord(CriminalRecord record) {
            _criminalRecords.Add(record);
        }

        public void DeleteCriminalRecord(CriminalRecord record) {
            _criminalRecords.Remove(record);
        }
        
        // Methods to modify database's criminal groups
        public void InsertCriminalGroup(CriminalGroup group) {
            _criminalGroups.Add(group);
        }

        public void DeleteCriminalGroup(CriminalGroup group) {
            _criminalGroups.Remove(group);
        }

        public void ChangeGroupMembership(CriminalRecord record, CriminalGroup newGroup) {
            // Iterate through all criminal groups to find the current group membership of the record.
            foreach (var group in _criminalGroups) {
                if (group.Contains(record)) {
                    group.RemoveCriminalFromCriminalGroup(record);
                    break; // Exit the loop after removing the criminal
                }
            }

            newGroup.AddCriminalToCriminalGroup(record);
        }

        // Method to set accusation details for a criminal record.
        public void SetAccusationDetails(CriminalRecord record, Accusation accusationDetails) {
            record.UpdateAccusation(accusationDetails);
        }

        // Method to set group affiliation for a criminal record.
        public void SetGroupAffiliation(CriminalRecord record, CriminalGroup groupAffiliation) {
            record.UpdateCriminalGroup(groupAffiliation);
        }
    }
}
