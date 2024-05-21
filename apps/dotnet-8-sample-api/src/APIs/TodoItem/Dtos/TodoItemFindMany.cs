namespace TodoItem.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class TodoItemFindMany : FindManyInput<TodoItem, TodoItemWhereInput> { }
