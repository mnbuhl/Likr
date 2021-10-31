using Likr.Client.Services;
using Microsoft.AspNetCore.Components;

namespace Likr.Client.Pages;

public partial class Index : ComponentBase
{
    [Inject]
    public AuthService? AuthService { get; set; }
    
    [Inject]
    public IPostService? PostService { get; set; }
}