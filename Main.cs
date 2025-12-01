using Advent_of_Code.Utility;

var today = DateTime.Today;
var year  = today.Year;
var day   = today.Day;
var part = File.ReadAllText($"../../../{today.Year}/{today.Day}/Part2.cs").Contains("{\n        return \"\";\n    }")
               ? 1
               : 2;
var submit = today is { Month: 12, Day: <= 12 };

Runner.Run(year, day, part, submit);