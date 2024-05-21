namespace Workspace.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class WorkspaceFindMany : FindManyInput<Workspace, WorkspaceWhereInput> { }
