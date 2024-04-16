using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceDatabaseManagementSystem {
    internal interface IPoliceCriminalDatabase {
        IEnumerable<ICriminalRecord> CriminalRecords { get; }
        IEnumerable<ICriminalGroup> CriminalGroups { get; }
    }
}

/*

 Using `IEnumerable<T>` instead of concrete collection types like `List<T>` or `ICollection<T>` provides several benefits:

1. Flexibility: By using `IEnumerable<T>`, we're not tied to a specific collection implementation. 
                This allows clients to use any collection type they prefer when implementing the interface. 
                They can use arrays, lists, sets, or any other collection type that implements `IEnumerable<T>`.

2. Abstraction: Interfaces should define the minimum contract required for interaction. 
                Using `IEnumerable<T>` keeps the interface minimalistic and abstract, focusing on iteration 
                over the elements rather than the specific implementation details of the collection.

3. Readability:`IEnumerable<T>` communicates the intent clearly - that the object is enumerable, meaning it can be iterated over. 
                This enhances code readability and makes it easier for developers to understand the purpose of the property or method.

4. Compatibility: `IEnumerable<T>` is widely supported and understood across .NET libraries and frameworks. 
                   It allows for easy integration with LINQ queries, foreach loops, and other standard language constructs.

5. Ease of Extension: If in the future you decide to change the underlying collection type (for example, from a list to a set), 
                      you won't need to update the interface or any implementing classes. 
                      This makes your code more maintainable and adaptable to future changes.

6. Lazy Evaluation: IEnumerable<T> supports lazy evaluation, meaning elements are retrieved only when needed. 
                    This can be beneficial for performance, especially when dealing with large collections 
                    or when querying a data source where loading all elements at once may be impractical.

In the context of the provided interfaces, using `IEnumerable<ICriminalRecord>` and `IEnumerable<ICriminalGroup>` 
allows for easy iteration over the collection of criminal records and groups, respectively, without imposing any specific implementation 
requirements. It allows clients to iterate over these collections using standard language constructs, 
enhancing code flexibility and maintainability.
 
 */
