
### What is it
<table><tr>
	<td width="180">![](https://raw.github.com/ukoreh/dotnetlog/master/logo.png)</td>
	<td>
		A fast and flexible logging and/or diagnostics framework for .NET
		It allows you to plug it into an existing or new application with minimal changes to the code.
		Or it can be used as in classic scenarios of logging things along the way at different levels.	
	</td>
</table></tr>

### Design
![](https://raw.github.com/ukoreh/dotnetlog/master/arch.png) 

### Output for "SimpleLogFormatter"
![](https://raw.github.com/ukoreh/dotnetlog/master/SimpleLogFormatterOutput.png) 

### Example One
```xml
<configSections>
	<section name="Logging.Config"
			 type="Logging.Config, Logging" />
	<section name="PlainFile.Config"
			 type="Logging.Persisters.PlainFile.Config, Logging.Persisters.PlainFile" />
</configSections>

<Logging.Config
        verbosity="eDebugs"
        persister="Logging.Persisters.PlainFile" />

<PlainFile.Config
	logFile="C:\\log.log" />    
```

```c#
using Logging;
...
public static void Main()
{ 
	 ILogger logger = LogManager.GetLogger();
			 logger.LogInfo( "Message of logical category" );
			 logger.LogTechnicalInfo( "Message of technical category" );
}
```

### Existing
![](https://raw.github.com/ukoreh/dotnetlog/master/existing.png) 

