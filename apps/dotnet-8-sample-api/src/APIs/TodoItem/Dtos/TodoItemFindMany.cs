using Dotnet_8SampleApiDotNet.APIs.Common;
using Dotnet_8SampleApiDotNet.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet_8SampleApiDotNet.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class TodoItemFindMany : FindManyInput<TodoItem, TodoItemWhereInput> { }
