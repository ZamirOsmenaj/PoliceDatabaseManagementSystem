using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceDatabaseManagementSystem {
    internal sealed class CriminalRecord : ICriminalRecord {
        
        private readonly string name;
        private IAccusation _accusationDetails;
        private ICriminalGroup _groupAffiliation;

        public string Nickname { get; }
        public DateTime BirthDate { get; }
        public IAccusation AccusationDetails => _accusationDetails;
        public ICriminalGroup GroupAffiliation => _groupAffiliation;

        public CriminalRecord(string name, string nickname, DateTime birthDate, IAccusation accusation, ICriminalGroup criminalGroup) {
            this.name = name;
            Nickname = nickname;
            BirthDate = birthDate;
            _accusationDetails = accusation;
            _groupAffiliation = criminalGroup;
        }

        // The name of the criminal is only retrievable through this method to maintain privacy.
        // It is inaccessible otherwise to ensure that the CrimesInsider3rdPartyCompany class cannot access it.
        // This measure is necessary because the CrimesInsider3rdPartyCompany class should not have visibility of the criminal's name,
        // and immutability alone does not suffice for this purpose.
        // The name variable is private, and there are no public getters or setters to enforce this restriction.
        public string GetName() => name;

        // Methods for modifying the accusation and group affiliation of a criminal.
        // While the class remains immutable to the CrimesInsider3rdPartyCompany class,
        // it is not immutable to the PoliceCriminalDatabase class.
        public void UpdateAccusation(IAccusation accusation) {
            _accusationDetails = accusation;
        }
        public void UpdateCriminalGroup(ICriminalGroup groupAffiliation) {
            _groupAffiliation = groupAffiliation;
        }

    }
}
