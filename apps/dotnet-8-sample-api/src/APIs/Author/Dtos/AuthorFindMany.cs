namespace Author.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class AuthorFindMany : FindManyInput<Author, AuthorWhereInput> { }
