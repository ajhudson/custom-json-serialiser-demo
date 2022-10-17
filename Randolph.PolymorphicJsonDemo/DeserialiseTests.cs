using Newtonsoft.Json;
using Randolph.PolymorphicJsonDemo.Constants;
using Randolph.PolymorphicJsonDemo.Models;
using Shouldly;

namespace Randolph.PolymorphicJsonDemo;

public class DeserialisationTests
{
    [Fact]
    public void ShouldDeserialiseArrayOfInsuranceClaims()
    {
        // Arrange
        const string InputJson = @"{
        ""firstName"":""Andy"",
        ""lastName"":""Hudson"",
        ""insuranceKinds"": [
            ""Car"",
            ""House""
            ]
        }";

        // Act
        var person = JsonConvert.DeserializeObject<Person>(InputJson);

        // Assert
        person.ShouldNotBeNull();
        person.FirstName.ShouldBe("Andy");
        person.LastName.ShouldBe("Hudson");
        person.InsuranceKinds.Count.ShouldBe(2);
        person.InsuranceKinds[0].ShouldBe(InsuranceKind.Car);
        person.InsuranceKinds[1].ShouldBe(InsuranceKind.House);
    }

    [Fact]
    public void ShouldDeserialiseSingleInsuranceClaimIntoArray()
    {
        // Arrange
        const string InputJson = @"{
        ""firstName"":""Andy"",
        ""lastName"":""Hudson"",
        ""insuranceKinds"": ""Car""
        }";

        // Act
        var person = JsonConvert.DeserializeObject<Person>(InputJson);

        // Assert
        person.ShouldNotBeNull();
        person.FirstName.ShouldBe("Andy");
        person.LastName.ShouldBe("Hudson");
        person.InsuranceKinds.Count.ShouldBe(1);
    }
}