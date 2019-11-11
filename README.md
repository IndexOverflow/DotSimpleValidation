# DotSimpleValidation

[![DotSimpleValidation](https://img.shields.io/nuget/v/DotSimpleValidation )](https://www.nuget.org/packages/DotSimpleValidation/)

You want validation, but you don't want an entire framework? This tiny project may be of help.
Meant to be used in constructors it will ensure that your objects don't contain invalid data. 
Works well with Domain primitives.

If a property fails validation the Validator will throw `ValidationException` (which extends `ArgumentException`). If you don't want to put validation in your constructors the project exposes its internal helper class which will `Either` contain a `Invalid` (invalid) or `Valid` (valid) side. 

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
        public string AnotherField { get; }

        public TestClass(string alphanumeric, int aNumber, string another)
        {
            TextField = alphanumeric.MustBe(Match(new Regex("([a-zA-Z0-9])")));
            NumberField = aNumber.MustBe(Between<int>(1, 10));
            AnotherField = another.MustBe(NotNullOrBlank());
        }
    }
```

### Result

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
"normal".MustBe(Match(new Regex("(normal)")));
```

```C#
"Aa".MustBe(Equal("Aa"));
```
