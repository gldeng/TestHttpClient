using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.ClearScript.V8;
using Newtonsoft.Json;
using TestJs;

// Serialize C# objects to JSON
var traits = new List<Trait>
{
    new Trait { Name = "a", Value = "a001" },
    new Trait { Name = "b", Value = "b002" }
};

var jsonPayload = JsonConvert.SerializeObject(traits);

// Load and execute the JavaScript file
var scriptPath = "/app/TestJs/createPrompt.js";
var scriptContent = File.ReadAllText(scriptPath);

using (var engine = new V8ScriptEngine())
{
    engine.Execute(scriptContent);

    // Pass the JSON string to JavaScript, parse it, and call 'myFunction'
    var configText =
        "{\"prefix\": \"I NEED to test how the tool works with extremely simple prompts. DO NOT add any detail, just use it AS-IS: A medium resolution pixel art image of a cat in an upright, bipedal stance, facing directly at the viewer,\"}";
    var traitArgs = "[{\"name\": \"Breed\", \"value\": \"Quantum\"}, {\"name\": \"Background\", \"value\": \"Cyberpunk Cityscape\"}]";
    engine.Execute($"var config = JSON.parse('{configText.Replace("'", "\\'")}')");
    engine.Execute($"var traitArgs = JSON.parse('{traitArgs.Replace("'", "\\'")}');");
    var result = engine.Script.createPrompt(engine.Script.config, engine.Script.traitArgs);

    // Use the result
    Console.WriteLine(result);
}