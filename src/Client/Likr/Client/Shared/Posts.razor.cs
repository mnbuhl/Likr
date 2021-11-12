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
    private int _page = 1;

    protected override async Task OnInitializedAsync()
    {
        await LoadPosts();
    }

    public async Task LoadPosts()
    {
        if (PostService == null)
            return;
        
        _posts.AddRange(await PostService.GetPosts(8, _page));
        _page++;
    }

    public async Task OnPostCreated(PostDto postDto)
    {
        var post = await PostService?.GetById(Guid.Parse(postDto.Id))!;

        _posts.Insert(0, post);
    }
    
    public async Task OnPostDeleted(PostDto postDto)
    {
        if (PostService == null)
            return;
        
        await PostService.DeletePost(Guid.Parse(postDto.Id));
        _posts.Remove(_posts.First(x => x.Id == postDto.Id));
    }
}