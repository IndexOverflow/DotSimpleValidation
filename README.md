# DotSimpleValidation

[![DotSimpleValidation](https://img.shields.io/nuget/v/DotSimpleValidation )](https://www.nuget.org/packages/DotSimpleValidation/)

You want validation, but you don't want an entire framework? This tiny project may be of help.
Meant to be used in constructors it will ensure that your objects don't contain invalid data. 
Works well with Domain primitives.

If a property fails validation the Validator will throw `DotSimpleValidation.ValidationException` (which extends `System.ArgumentException`).  
 
 If you don't want to put validation in your constructors 
 a few convience methods are also provided. `TryValidate` which works similar to `TryParse` methods
 found elsewhere in the language, and the more FP inspired `IsValid` & `IfInvalid`.    
 
 `IsValid` exposes the project's internal helper class `Result` which either contains an `Invalid` (invalid) or `Valid` (valid) side.   

### Validation

```C#
using System.Text.RegularExpressions;
using static DotSimpleValidation.Validators; // using static allows less verbose usage

namespace DotSimpleValidation.Tests
{
    internal class TestClass
    {
        public string TextField { get; }
        public int NumberField { get; }
        public string? AnotherField { get; }
        public string? OptionalField { get; }

        public TestClass(string alphanumeric, int aNumber, string? another, string optional = null)
        {
            TextField = alphanumeric.MustBe(OfLength(4,16), Match(new Regex("([a-zA-Z0-9])")));
            NumberField = aNumber.MustBe(Between<int>(1, 10));
            AnotherField = another.NotNull().MustBe(NotBlankOrEmpty());
            OptionalField = optional?.MustBe(Equal("value-if-set"));
        }
    }
}
```

### TryValidate
Inspired by the "TryParse" methods found on many Types.  
Note that nullable Types are supported (`null` will return `false`), but  
supplied validators must be suppressed with non-null (!). 

```C#
public IActionResult MyWebMethod(string? untrustworthy)
{
    if (Validator.TryValidation(untrustworthy, out var validString, Equal("good data")!)) 
    {
        return Ok($"{validString} is the best data!");
    } 
 
    return BadRequest("Sorry, you can't be trusted");     
}
```

### IsValid()...IfInvalid()
Useful if you prefer the new pattern matching from C#8.

```C#
public ISomeResult SafetyFirst(string untrustworthy)
{
    var result = untrustworthy
        .IsValid(OfLength(5,20), Match(new Regex("Friend-O")))
        .IfInvalid(new ErrorMessage("You are evil!"));

    return result switch
    {
        Result<ErrorMessage, string>.Valid valid => Ok("Thanks for being our " + valid.Data),
        Result<ErrorMessage, string>.Invalid invalid => BadRequest(invalid.Error)
    }; 
}
```

### Nullable

Nullable reference types (and `Nullable<T>`) from C#8 are supported via a new helper, `NotNull`. 

```C#
string? test = null;
// Will throw ValidationException
test.NotNull().MustBe(TrueFor<string>((s) => s.Contains("test"))); 
```

*This was deemed the best compromise due to the conflict between `T? where : class` and `T where : struct`. A PR is welcome for an alternate approach.* 

```C#
internal class NullableTestClass
{
    public string? NullableField;
    public string NotNullableField;

    public NullableTestClass(string? nullableField, string notNullableField)
    {
        NullableField = nullableField?.MustBe(NotBlankOrEmpty());
        NotNullableField = notNullableField.NotNull().MustBe(NotBlankOrEmpty());
    }
}
```


### Included validators

```C#
"is a test".MustBe(BeTrue<string>((s) => s.Contains("test")));
```

```C#
3.MustBe(Between<int>(1, 2));
```

```C#
"https://github.com".MustBe(CreatableUsing<string,Uri>(s => new Uri(s)));
```

```C#
"normal".MustBe(NotNullOrBlank());
```

```C#
"normal".MustBe(Match(new Regex("^[a-z]{3,6}$")));
```

```C#
"Aa".MustBe(Equal("Aa"));
```

### Result (Obsolete)
Will be removed in next major version.
```C#
public void SaveIfValid(string untrustworthy)
{
    var result = untrustworthy.ResultMustBe(Match(new Regex("(safe)")));

    if (result is Result<string, string>.Valid valid)
    {
        Repo.SaveValidData(valid.Data);
    }
    else
    {
        throw new ArgumentException(((Result<string,string>.Invalid) result).Error);    
    } 
}
```

### Domain primitives example

This is our domain entity - we only want it to contain valid data.

```C#
internal class ExampleClassB
{
    public readonly Name Name;
    public readonly Age Age;
    public readonly Occupation Occupation;

    public ExampleClassB(Name name, Age age, Occupation occupation)
    {
        Name = name;
        Age = age;
        Occupation = occupation;
    }
}
```

`Age` is a simple domain primitive which validates that your age can only be an integer between 1 and 100.

```C#
internal class Age
{
    public readonly int Value;

    public Age(int? age)
    {
        Value = age.NotNull().MustBe(Between<int>(1, 150));
    }
}
```
`Name` and `Occupation` are additional primitives which will hold a persons' name and occupation.

```C#
internal class Name : DomainStringPrimitive
{
    // don't ever actually use this regex pattern!
    public Name(string? value) : base(value, @"^[a-zA-Z ]{2,50}$") 
    {
    }
}
```
Regular expressions are great for performing input validation, and as you can image this is a fairly frequently used pattern. We therefore create a base class, `DomainStringPrimitive`.  

**Note:** It is recommended that you always check the length of string before applying a RegEx, see `StringValidators.OfLength`

```C#
internal abstract class DomainStringPrimitive
{
    public readonly string Value;

    public DomainStringPrimitive(string? value, string pattern, int minLength = 3, int maxLength = 30)
    {
        Value = value.NotNull().MustBe(OfLength(minLength,maxLength), Match(new Regex(pattern)));
    }

    public override string ToString()
    {
        return Value;
    }
}
```