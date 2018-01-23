# Simple pipeline

This library has the concept of a set of **pipes** joint by **connectors** where pipes are just code that do a single task and connectors coordinate the execution. Each pipe receives a **load** which is just a dictionary with the state of the current task, each pipe can read/write information from that dictionary.

Simple example:

```
Task 1 -> Task 2 -> Task 3
```

```csharp
var testLoad = new Dictionary<string, object>();

var runner = new PipeLine()
    .Pipe(load => load["Task 1"] = true)
    .Connect()
    .Pipe(load => load["Task 2"] = true)
    .Connect()
    .Pipe(load => load["Task 3"] = true)
    .Finish();

runner.Run(testLoad);
```

Now the `load` dictionary should look something like this:

| Key | Value |
|-----|-------|
| Task 1 | true |
| Task 2 | true |
| Task 3 | true |

Basic condition example:

```
            -> Task 2 -
          |            |
Task 1 -> o -> Task 3 -o -> Task 5
          |            |
            -> Task 4 -
```

```cs
var pipeLine = new PipeLine()
    .Pipe(load => load["Task 1"] = true)
    .ConnectWhen((pipes, load) =>
    {
        var chosenPath = load["ChosenPipe"].ToString();
        return pipes.OfType<NamedPipe>().First(p => p.Name == chosenPath);
    })
    .Pipe("pipe2", load => load["Task 2"] = true) // Named pipe
    .Pipe("pipe3", load => load["Task 3"] = true) // Named pipe
    .Pipe("pipe4", load => load["Task 4"] = true) // Named pipe
    .Join()
    .Pipe(load => load["Task 5"] = true)
    .Finish();

// Runs Task 1 -> Task 2 -> Task 5
var path125 = new Dictionary<string, object> { ["ChosenPipe"] = "Task 2" };
pipeLine.Run(path125);

// Runs Task 1 -> Task 3 -> Task 5
var path135 = new Dictionary<string, object> { ["ChosenPipe"] = "Task 3" };
pipeLine.Run(path135);

// Runs Task 1 -> Task 4 -> Task 5
var path145 = new Dictionary<string, object> { ["ChosenPipe"] = "Task 4" };
pipeLine.Run(path145);
```