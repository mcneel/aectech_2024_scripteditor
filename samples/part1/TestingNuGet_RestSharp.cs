#r "nuget: RestSharp, 106.11.7"

using System;
using Rhino;

using RestSharp;
using RestSharp.Authenticators;

var client = new RestClient("https://httpbin.org");
var request = new RestRequest("get", DataFormat.Json);
var response = client.Get(request);

Console.WriteLine(response.Content);