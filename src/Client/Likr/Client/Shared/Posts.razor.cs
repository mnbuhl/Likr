using Likr.Client.Dtos;
using Likr.Client.Services;
using Microsoft.AspNetCore.Components;

namespace Likr.Client.Shared;

public partial class Posts : ComponentBase
{
    [Inject]
    public AuthService? AuthService { get; set; }
    
    [Inject]
    public IPostService? PostService { get; set; }

    private List<PostDto> _posts = new();

    protected override async Task OnInitializedAsync()
    {
        if (PostService == null)
            return;
        
        _posts = await PostService.GetPosts(10, 1);
    }
}