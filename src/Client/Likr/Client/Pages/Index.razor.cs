using Likr.Client.Dtos;
using Likr.Client.Services;
using Microsoft.AspNetCore.Components;

namespace Likr.Client.Pages;

public partial class Index : ComponentBase
{
    [Inject]
    public AuthService? AuthService { get; set; }
    
    [Inject]
    public IPostService? PostService { get; set; }
    
    [Inject]
    public IHttpService? HttpService { get; set; }

    private List<PostDto> _posts = new List<PostDto>();
    private List<UserDto> _users = new List<UserDto>();

    protected override async Task OnInitializedAsync()
    {
        if (PostService == null)
            return;
        
        _posts = await PostService.GetPosts();
        List<string> userIds = _posts.Select(x => x.UserId).ToList();

        foreach (var id in userIds.Distinct().ToList())
        {
            _users.Add((await HttpService.Get<UserDto>($"/api/v1/u/Users/{id}")).Response);
        }
    }
}