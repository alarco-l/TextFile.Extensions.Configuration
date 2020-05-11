# TextFile.Extensions.Configuration
Text file configuration provider implementation for Microsoft.Extensions.Configuration.  
Only make for my learning and understanding about Configuration in dotnet core.  

Dummy file configuration, one key/value per line.  
The default key/value separator is ':' but it can be overrided on `AddTextFileConfiguration` call.

#### Default Separator
```
...
SomeKey:SomeValue
SomeOtherKey:SomeOtherValue
...
```

#### Custom Separator using '#'
```
...
SomeKey#SomeValue
SomeOtherKey#SomeOtherValue
...
```