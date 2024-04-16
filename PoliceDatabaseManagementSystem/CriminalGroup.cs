using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceDatabaseManagementSystem {
    internal sealed class CriminalGroup : ICriminalGroup {

        public string Name { get; }
        private List<ICriminalRecord> _participatingCriminals;

        public IEnumerable<ICriminalRecord> ParticipatingCriminals => _participatingCriminals;

        public CriminalGroup(string name, IEnumerable<ICriminalRecord> participatingCriminals) {
            Name = name;
            _participatingCriminals = new List<ICriminalRecord>(participatingCriminals);
        }

        // Method to check if the group contains a specific criminal
        public bool Contains(CriminalRecord criminal) {
            return _participatingCriminals.Contains(criminal);
        }

        // Methods to modify criminals list
        public void AddCriminalToCriminalGroup(CriminalRecord record) {
            _participatingCriminals.Add(record);
        }

        public void RemoveCriminalFromCriminalGroup(CriminalRecord record) {
            _participatingCriminals.Remove(record);
        }
    }
}
