using Newtonsoft.Json;
using Randolph.PolymorphicJsonDemo.Constants;

namespace Randolph.PolymorphicJsonDemo.Models;

public class InsuranceClaim
{
    public decimal AnnualPremium { get; set; }

    public InsuranceKind Kind { get; set; }
}