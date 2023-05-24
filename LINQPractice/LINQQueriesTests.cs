using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public List<string> Hobbies { get; set; }
}

[TestFixture]
public class LINQQueriesTests
{
    private List<Person> people;

    [SetUp]
    public void Setup()
    {
        // Initialize the list of Person objects
        people = new List<Person>
        {
            new Person { Name = "Alice", Age = 25, Hobbies = new List<string> { "Reading", "Gardening" } },
            new Person { Name = "Bob", Age = 30, Hobbies = new List<string> { "Cooking", "Painting" } },
            new Person { Name = "Charlie", Age = 35, Hobbies = new List<string> { "Gaming", "Singing" } },
            new Person { Name = "David", Age = 40, Hobbies = new List<string> { "Reading", "Writing" } },
            new Person { Name = "Eve", Age = 45, Hobbies = new List<string> { "Cooking", "Singing" } }
        };
    }

    [Test]
    public void Query1_FindPeopleWithAgeBetween30And40AndHobbiesIncludeReading_ReturnsCorrectPeople()
    {
        // Act
        var result = people.Where(p => p.Age >= 30 && p.Age <= 40 && p.Hobbies.Contains("Reading")).Select(p => p.Name).ToList();

        // Assert
        Assert.AreEqual(1, result.Count());
        Assert.Contains("David", result);
    }

    [Test]
    public void Query2_FindNamesOfPeopleWithAtLeastTwoHobbies_ReturnsCorrectNames()
    {
        // Act
        var result = people.Where(p => p.Hobbies.Count >= 2).Select(p => p.Name).ToList();

        // Assert
        Assert.AreEqual(5, result.Count());
        Assert.Contains("Bob", result);
        Assert.Contains("Charlie", result);
        Assert.Contains("Eve", result);
        Assert.Contains("David", result);
        Assert.Contains("Alice", result);
    }

    [Test]
    public void Query3_CountTotalNumberOfHobbiesAcrossAllPeople_ReturnsCorrectCount()
    {
        // Act
        var result = people.Sum(p => p.Hobbies.Count);

        // Assert
        Assert.AreEqual(10, result);
    }

    [Test]
    public void Query5_CheckIfPersonWithNameContainingAllVowelsExists_ReturnsCorrectResult()
    {
        // Act
        var vowels = new List<char> { 'a', 'e', 'i', 'o', 'u' };
        var result = people.Any(p => vowels.All(v => p.Name.ToLower().Contains(v)));

        // Assert
        Assert.IsFalse(result);
    }

    [Test]
    public void Query6_FindPeopleWhoseNameStartsWithAAndAgeIsLessThan30_ReturnsCorrectPeople()
    {
        // Act
        var result = people.Where(p => p.Name.StartsWith("A") && p.Age < 30).Select(p => p.Name).ToList();

        // Assert
        Assert.AreEqual(1, result.Count());
        Assert.Contains("Alice", result);
    }

    [Test]
    public void Query7_FindOldestPerson_ReturnsCorrectPerson()
    {
        // Act
        var result = people.OrderByDescending(p => p.Age).First();

        // Assert
        Assert.AreEqual("Eve", result.Name);
    }

    [Test]
    public void Query8_CheckIfAnyPersonHasHobbiesStartingWithG_ReturnsCorrectResult()
    {
        // Act
        var result = people.Any(p => p.Hobbies.Any(h => h.StartsWith("G")));

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public void Query10_CountPeopleWithHobbiesStartingWithS_ReturnsCorrectCount()
    {
        // Act
        var result = people.Count(p => p.Hobbies.Any(h => h.StartsWith("S")));

        // Assert
        Assert.AreEqual(2, result);
    }

    [Test]
    public void Query11_SelectPeopleWithNamesContainingExactlyFiveLetters_ReturnsCorrectPeople()
    {
        // Act
        var result = people.Where(p => p.Name.Length == 5).Select(p => p.Name).ToList();

        // Assert
        Assert.AreEqual(2, result.Count());
        Assert.Contains("Alice", result);
        Assert.Contains("David", result);
    }

    [Test]
    public void Query12_CheckIfAllPeopleAreAbove20YearsOld_ReturnsCorrectResult()
    {
        // Act
        var result = people.All(p => p.Age > 20);

        // Assert
        Assert.IsTrue(result);
    }

    
    [Test]
    public void Query13_SelectCommonHobbies_ReturnsCorrectHobbies() // hobbies that are shared by at least two people
    {
        // Act
        var commonHobbies = people.SelectMany(p => p.Hobbies).GroupBy(h => h)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key).ToList();

        // Assert
        Assert.AreEqual(3, commonHobbies.Count);
        Assert.Contains("Reading", commonHobbies);
        Assert.Contains("Cooking", commonHobbies);
        Assert.Contains("Singing", commonHobbies);
    }


    [Test]
    public void Query14_FindPeopleWithTwoDistinctHobbies_ReturnsCorrectPeople()
    {
        // Act
        var result = people.Where(p => p.Hobbies.Distinct().Count() == 2).Select(p => p.Name).ToList();

        // Assert
        Assert.AreEqual(5, result.Count());
        Assert.Contains("David", result);
        Assert.Contains("Alice", result);
        Assert.Contains("Bob", result);
        Assert.Contains("Charlie", result);
        Assert.Contains("Eve", result);
    }

    [Test]
    public void Query15_SelectHobbiesInAlphabeticalOrder_ReturnsCorrectOrder()
    {
        // Act
        var result = people.SelectMany(p => p.Hobbies).OrderBy(h => h);

        // Assert
        Assert.AreEqual(10, result.Count());
        Assert.AreEqual("Cooking", result.First());
        Assert.AreEqual("Writing", result.Last());
    }

    [Test]
    public void Query16_CheckIfAnyPersonHasNameWithDuplicateCharacters_ReturnsCorrectResult()
    {
        // Act
        var result = people.Any(p => p.Name.Distinct().Count() != p.Name.Length);

        // Assert
        Assert.IsFalse(result);
    }

    [Test]
    public void Query17_SelectPeopleWithAtLeastOneHobbyStartingWithR_ReturnsCorrectPeople()
    {
        // Act
        var result = people.Where(p => p.Hobbies.Count(h => h.StartsWith("R")) >= 1).Select(p => p.Name).ToList();

        // Assert
        Assert.AreEqual(2, result.Count());
        Assert.Contains("David", result);
        Assert.Contains("Alice", result);
    }

    [Test]
    public void Query18_FindPersonWithLongestName_ReturnsCorrectPerson()
    {
        // Act
        var result = people.OrderByDescending(p => p.Name.Length).First();

        // Assert
        Assert.AreEqual("Charlie", result.Name);
    }

    [Test]
    public void Query19_CheckIfAllPeopleHaveUniqueHobbies_ReturnsCorrectResult()
    {
        // Act
        var result = people.All(p => p.Hobbies.Distinct().Count() == p.Hobbies.Count);

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public void Query20_SelectPeopleWithAtLeastOneHobbyContainingMoreThanTwoWords_ReturnsCorrectPeople()
    {
        // Act
        var result = people.Where(p => p.Hobbies.Any(h => h.Split(' ').Length > 2)).Select(p => p.Name);

        // Assert
        Assert.AreEqual(0, result.Count());
    }

    [Test]
    public void Query21_FindPeopleWithNamesEndingWithEOrContainsVowelsAndHaveAtLeastOneHobby_ReturnsCorrectPeople()
    {
        // Act
        var result = people.Where(p => p.Name.EndsWith("e") || p.Name.ToLower().Any(c => "aeiou".Contains(c)) && p.Hobbies.Count > 0).Select(p => p.Name).ToList();

        // Assert
        Assert.AreEqual(5, result.Count());
        Assert.Contains("Alice", result);
        Assert.Contains("Bob", result);
        Assert.Contains("Charlie", result);
        Assert.Contains("David", result);
        Assert.Contains("Eve", result);
    }

    [Test]
    public void Query22_CountPeopleWithTotalAgeGreaterThan150_ReturnsCorrectCount()
    {
        // Act
        var result = people.Where(p => p.Age > 0).Sum(p => p.Age);

        // Assert
        Assert.AreEqual(175, result);
    }

    [Test]
    public void Query24_FindPeopleWithNamesContainingOnlyLetters_ReturnsCorrectPeople()
    {
        // Act
        var result = people.Where(p => p.Name.All(char.IsLetter)).Select(p => p.Name).ToList();

        // Assert
        Assert.AreEqual(5, result.Count());
        Assert.Contains("Alice", result);
        Assert.Contains("Bob", result);
        Assert.Contains("Charlie", result);
        Assert.Contains("David", result);
        Assert.Contains("Eve", result);
    }

    [Test]
    public void Query25_CheckIfAnyPersonHasAllHobbiesStartingWithC_ReturnsCorrectResult()
    {
        // Act
        var result = people.Any(p => p.Hobbies.All(h => h.StartsWith("C")));

        // Assert
        Assert.IsFalse(result);
    }

    [Test]
    public void Query26_SelectPeopleWithDistinctAges_ReturnsCorrectPeople()
    {
        // Act
        var result = people.GroupBy(p => p.Age).Where(g => g.Count() == 1).SelectMany(g => g).ToList();

        // Assert
        Assert.AreEqual(5, result.Count());
        Assert.Contains("Alice", result.Select(p => p.Name).ToList());
        Assert.Contains("Bob", result.Select(p => p.Name).ToList());
        Assert.Contains("Charlie", result.Select(p => p.Name).ToList());
        Assert.Contains("David", result.Select(p => p.Name).ToList());
        Assert.Contains("Eve", result.Select(p => p.Name).ToList());
    }

    [Test]
    public void Query27_CountPeopleWithNameContainingVowelsInReverseOrder_ReturnsCorrectCount()
    {
        // Act
        var vowels = new List<char> { 'a', 'e', 'i', 'o', 'u' };
        var result = people.Count(p => p.Name.ToLower().Reverse().Any(c => vowels.Contains(c)));

        // Assert
        Assert.AreEqual(5, result);
    }

    [Test]
    public void Query28_SelectPeopleWithNamesOfEvenLength_ReturnsCorrectPeople()
    {
        // Act
        var result = people.Where(p => p.Name.Length % 2 == 0).Select(p => p.Name).ToList();

        // Assert
        Assert.AreEqual(0, result.Count());
    }

    [Test]
    public void Query29_CheckIfAllPeopleHaveUniqueNames_ReturnsCorrectResult()
    {
        // Act
        var result = people.All(p => people.Count(p2 => p2.Name == p.Name) == 1);

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public void Query30_FindPeopleWithNamesContainingDuplicateLetters_ReturnsCorrectPeople()
    {
        // Act
        var result = people.Where(p => p.Name.ToLower().Distinct().Count() < p.Name.Length).Select(p => p.Name);

        // Assert
        Assert.AreEqual(3, result.Count());
        Assert.Contains("Bob", result.ToList());
        Assert.Contains("David", result.ToList());
        Assert.Contains("Eve", result.ToList());
    }

    [Test]
    public void Query31_SelectPeopleWithMultipleHobbiesContainingTheWordIn_ReturnsCorrectPeople()
    {
        // Act
        var result = people.Where(p => p.Hobbies.Count(h => h.ToLower().Contains("in")) > 1).Select(p => p.Name);

        // Assert
        Assert.AreEqual(5, result.Count());
        Assert.Contains("Bob", result.ToList());
        Assert.Contains("David", result.ToList());
        Assert.Contains("Eve", result.ToList());
        Assert.Contains("Alice", result.ToList());
        Assert.Contains("Charlie", result.ToList());
    }

    [Test]
    public void Query33_CheckIfAnyPersonHasAgeGreaterThan50AndHobbiesStartingWithG_ReturnsCorrectResult()
    {
        // Act
        var result = people.Any(p => p.Age > 50 && p.Hobbies.Any(h => h.StartsWith("G")));

        // Assert
        Assert.IsFalse(result);
    }

    [Test]
    public void Query34_CountPeopleWithHobbiesContainingMoreThanTwoWords_ReturnsCorrectCount()
    {
        // Act
        var result = people.Count(p => p.Hobbies.Any(h => h.Split(' ').Length > 2));

        // Assert
        Assert.AreEqual(0, result);
    }

    [Test]
    public void Query36_FindPeopleWithNamesContainingOnlyUppercaseLetters_ReturnsCorrectPeople()
    {
        // Act
        var result = people.Where(p => p.Name.All(char.IsUpper)).Select(p => p.Name);

        // Assert
        Assert.AreEqual(0, result.Count());
    }

    [Test]
    public void Query37_CheckIfAllPeopleHaveAtLeastTwoHobbies_ReturnsCorrectResult()
    {
        // Act
        var result = people.All(p => p.Hobbies.Count >= 2);

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public void Query38_SelectPeopleWithNamesContainingMoreThanTwoWords_ReturnsCorrectPeople()
    {
        // Act
        var result = people.Where(p => p.Name.Split(' ').Length > 2).Select(p => p.Name);

        // Assert
        Assert.AreEqual(0, result.Count());
    }

    [Test]
    public void Query40_FindPersonWithShortestName_ReturnsCorrectPerson()
    {
        // Act
        var result = people.OrderBy(p => p.Name.Length).First();

        // Assert
        Assert.AreEqual("Bob", result.Name);
    }

    [Test]
    public void Query41_SelectPeopleWithAtLeastOneHobbyContainingMoreThanEightCharacters_ReturnsCorrectPeople()
    {
        // Act
        var result = people.Where(p => p.Hobbies.Any(h => h.Length > 8)).Select(p => p.Name).ToList();

        // Assert
        Assert.AreEqual(1, result.Count());
        Assert.Contains("Alice", result);
    }

    [Test]
    public void Query42_CheckIfAnyPersonHasAllHobbiesContainingTheLetterO_ReturnsCorrectResult()
    {
        // Act
        var result = people.Any(p => p.Hobbies.All(h => h.ToLower().Contains("o")));

        // Assert
        Assert.IsFalse(result);
    }

    [Test]
    public void Query44_FindPeopleWithNamesContainingDigits_ReturnsCorrectPeople()
    {
        // Act
        var result = people.Where(p => p.Name.Any(char.IsDigit)).Select(p => p.Name);

        // Assert
        Assert.AreEqual(0, result.Count());
    }

    [Test]
    public void Query45_CheckIfAllPeopleHaveDistinctHobbies_ReturnsCorrectResult()
    {
        // Act
        var result = people.All(p => p.Hobbies.Distinct().Count() == p.Hobbies.Count);

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public void Query49_CheckIfAnyPersonHasAgeGreaterThan60AndNameContainingLetterX_ReturnsCorrectResult()
    {
        // Act
        var result = people.Any(p => p.Age > 60 && p.Name.ToLower().Contains("x"));

        // Assert
        Assert.IsFalse(result);
    }

    [Test]
    public void Query50_SelectPeopleWithNamesContainingDuplicateWords_ReturnsCorrectPeople()
    {
        // Act
        var result = people.Where(p => p.Name.Split(' ').Distinct().Count() < p.Name.Split(' ').Length).Select(p => p.Name);

        // Assert
        Assert.AreEqual(0, result.Count());
    }
}