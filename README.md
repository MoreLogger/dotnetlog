
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
Task => The logs will go to the storages (persisters) that are indicated in the config file.

```xml
<configSections>
	<section name="Logging.Config"
			 type="Logging.Config, Logging" />
	<section name="PlainFile.Config"
			 type="Logging.Persisters.PlainFile.Config, Logging.Persisters.PlainFile" />
</configSections>
```

```xml
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

### Example Two
Task => Hooking into System.Diagnostics and redirect those logs to a text file.

```xml
<configSections>
	<section name="Logging.Config"
			 type="Logging.Config, Logging" />
	<section name="PlainFile.Config"
			 type="Logging.Persisters.PlainFile.Config, Logging.Persisters.PlainFile" />
</configSections>
```

```xml
<system.diagnostics>
	<trace autoflush="true" indentsize="4">
		<listeners>
			<add name="TraceListener.Logger"
				 type="Logging.Adapters.In.TraceListener.Logger, Logging.Adapters.In.TraceListener" />
		</listeners>
	</trace>
</system.diagnostics>
```

```xml
<Logging.Config
	verbosity="eDebugs"
	persister="Logging.Persisters.PlainFile" />

<PlainFile.Config
	logFile="C:\\log.log" />
```

```c#
using System.Diagnostics;
...
public static void Main()
{
		Trace.WriteLine( "Start" );
		Trace.Indent();
			Trace.WriteLine( "aaaaaaaa" );
			Trace.WriteLine( "aaaaaaaa" );
			Trace.WriteLine( "aaaaaaaa" );
			Trace.Indent();
				Trace.WriteLine( "bbbbbbbb" );
				Trace.WriteLine( "bbbbbbbb" );
				Trace.WriteLine( "bbbbbbbb" );
			Trace.Unindent();
		Trace.Unindent();
		Trace.WriteLine( "End" );
}
```

### Example Three
Task => Asynchronous logging to different storages(persisters) + Email notification (just another persister).

What happens with a config like this is that you have in your hands the control to build and shape the pipe through which the logs flow. 
<table>
	<tr><td>Step 1:</td>
		<td>We want an non blocking call to log API for that reason we use Async module</td></tr>
	<tr><td>Step 2:</td>
		<td>Then by using Multi we want to stock the logs into several storages ( a practical use would be log file and send an email with the error ) </td></tr>
	<tr><td>Step 3:</td>
		<td>Each module specified in Multi gets its specific parameters it need to function</td></tr>
	<tr><td>Step 4:</td>
		<td>Done, we start log things we need at Trace level, Debug level and other intermediary levels</td></tr>
</table></tr>


```xml
<configuration>
    <!-- Logging Definition 
    ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ --> 
    <configSections> 
        
        <section name="Logging.Config" 
                 type="Logging.Config, Logging" /> 

        <section name="Async.Config" 
                 type="Logging.Modifiers.Async.Config, Logging.Modifiers.Async" /> 

        <section name="Multi.Config" 
                 type="Logging.Modifiers.Multi.Config, Logging.Modifiers.Multi" /> 
        
        <section name="PlainFile.Config" 
                 type="Logging.Persisters.PlainFile.Config, Logging.Persisters.PlainFile" /> 

        <section name="WindowsEventLog.Config" 
                 type="Logging.Persisters.WindowsEventLog.Config, Logging.Persisters.WindowsEventLog" /> 

        <section name="Email.Config" 
                 type="Logging.Persisters.Email.Config, Logging.Persisters.Email" /> 

    </configSections> 

    
    <!-- Logging Configuration 
    ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ --> 
    <Logging.Config 
        verbosity="eDebugs" 
        persister="Logging.Modifiers.Async" /> <!-- Means we want all logs till levele "Debug" to flow to the psersister "Async" --> 

    <Async.Config 
        persister="Logging.Modifiers.Multi" /> <!-- Means the "Async" will pass what it gets to the "Multi" in an asynchronous way --> 

    <Multi.Config                              <!-- Multi is specialized to pass what he gets to a list of persisters so they can actually store the logs --> 
        persisters="Logging.Persisters.PlainFile;Logging.Persisters.SystemConsole;Logging.Persisters.WindowsEventLog;Logging.Persisters.Email" /> 

    <PlainFile.Config 
        logFile="C:\\log.log" /> 

    <WindowsEventLog.Config 
        source="Logging.Tester" /> 

    <Email.Config 
        mailServer  ="192.168.1.59" 
        fromMail    ="user@domain.com" 
        fromUser    ="user" 
        fromPassword="password" 
        to          ="targetUser1@domain.com:targetUser2@domain.com" /> 

</configuration> 
```



### Existing
![](https://raw.github.com/ukoreh/dotnetlog/master/existing.png) 

