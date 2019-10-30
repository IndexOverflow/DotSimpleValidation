# DotSimpleValidation
[![DotSimpleValidation](https://img.shields.io/nuget/v/DotSimpleValidation )](https://www.nuget.org/packages/DotSimpleValidation/)

You want validation, but you don't want an entire framework? This tiny project may be of help.
Meant to be used in constructors it will ensure that your objects don't contain invalid data. 
Works well with Domain primitives.

If a property fails validation the Validator will throw `ValidationException` (which extends `ArgumentException`). If you don't want to put validation in your constructors the project exposes its internal helper class which will `Either` contain a `Left` (invalid) or `Right` (valid) side. 

### Validation
```C#
using System.Text.RegularExpressions;
using static DotSimpleValidation.Validators; // using static allows less verbose usage

namespace DotSimpleValidation.Tests
{
        public class SomeClass
        {
            public readonly string ValidDataString;
            public readonly int ValidNumber;
            public readonly string SortaOptional;

            public SomeClass(string data, int aNumber, string message)
            {
                ValidDataString = data.MustBe(Match(new Regex("([a-zA-Z0-9])")));
                ValidNumber = aNumber.MustBe(Between<int>(1, 10));
                SortaOptional = message.MustBe(NotNullOrBlank());
            }
        }
    }
}
```
### Either
```C#
public void SaveIfValid(string untrustworthy)
{
    var result = untrustworthy.EitherMustBe(Match(new Regex("(safe)")));
                    
    if (result is Either<string, string>.Right right)
    {
        Repo.SaveValidData(right.Data);
    }
    else
    {
        throw new ArgumentException(((Either<string,string>.Left) result).Error);    
    } 
}
```
