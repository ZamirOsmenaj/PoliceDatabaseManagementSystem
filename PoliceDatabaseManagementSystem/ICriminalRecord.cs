using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceDatabaseManagementSystem {
    internal interface ICriminalRecord {
        string Nickname { get; }
        DateTime BirthDate { get; }
        IAccusation AccusationDetails { get; }
        ICriminalGroup GroupAffiliation { get; }
    }
}

// This interface, ICriminalRecord, is created to enforce privacy regarding the name of a criminal within the context
// of the CrimesInsider3rdPartyCompany class. By defining this interface, the CrimesInsider3rdPartyCompany class can access
// all other fields of a criminal record except for the name variable. This design decision ensures that the CrimesInsider3rdPartyCompany
// class cannot directly access the name of a criminal, promoting data privacy and encapsulation.
// Additionally, this interface facilitates polymorphism, allowing for comparisons of criminal records without directly
// exposing the implementation details of the CriminalRecord class.
// This approach contrasts with the requirements of the PoliceCriminalDatabase class, which necessitates access to all fields
// of a criminal record for functionalities such as updating and modifying criminal data, but it gains access to the variable of name
// via methods that are implemented within the CriminalRecord class.
