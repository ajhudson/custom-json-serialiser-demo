using Newtonsoft.Json;
using Randolph.PolymorphicJsonDemo.Constants;
using Randolph.PolymorphicJsonDemo.Converters;

namespace Randolph.PolymorphicJsonDemo.Models;

[JsonConverter(typeof(PersonConverter))]
public class Person
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public IList<InsuranceKind> InsuranceKinds { get; set; } = new List<InsuranceKind>();
}