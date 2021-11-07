using System.Security.Claims;
using Likr.Client.Dtos;
using Likr.Client.Extensions;
using Likr.Client.Services;
using Microsoft.AspNetCore.Components;

namespace Likr.Client.Shared;

public partial class PostForm : ComponentBase
{
    [Inject]
    public IPostService? PostService { get; set; }

    [Inject]
    public AuthService? AuthService { get; set; }

    private CreatePostDto _post = new CreatePostDto();

    private ClaimsPrincipal? _user;

    protected override async Task OnInitializedAsync()
    {
        if (AuthService == null)
            return;

        _user = await AuthService.GetCurrentUser();
    }

    private async Task HandleValidSubmit()
    {
        if (AuthService == null || PostService == null)
            return;
        
        var user = await AuthService.GetCurrentUser();
        
        if (user == null)
            return;

        _post.UserId = Guid.Parse(user.GetUserId());
        await PostService.CreatePost(_post, "");
    }
}