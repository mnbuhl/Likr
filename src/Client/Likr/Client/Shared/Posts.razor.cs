using Likr.Client.Dtos;
using Likr.Client.Services;
using Microsoft.AspNetCore.Components;

namespace Likr.Client.Shared;

public partial class Posts : ComponentBase
{
    [Inject]
    public IPostService? PostService { get; set; }
    
    [Inject]
    public ILikeService? LikeService { get; set; }
    
    [CascadingParameter(Name = "UserId")]
    public string? UserId { get; set; }

    [Parameter]
    public string Username { get; set; } = string.Empty;

    private readonly List<PostDto> _posts = new();
    private readonly List<LikeDto> _likes = new();
    private int _page = 1;
    private readonly int _pageSize = 8;
    private bool _displayLoadMore = false;

    protected override async Task OnInitializedAsync()
    {
        await LoadPosts();
    }

    public async Task LoadPosts()
    {
        if (PostService == null)
            return;

        var posts = new List<PostDto>();

        if (string.IsNullOrEmpty(Username))
        {
            posts.AddRange(await PostService.GetPosts(_pageSize, _page));
        }
        else
        {
            posts.AddRange(await PostService.GetPostsByUsername(Username, _pageSize, _page));
        }
        
        _displayLoadMore = posts.Count == _pageSize;
        
        _posts.AddRange(posts);
        _page++;
        
        await ApplyLikes();
    }

    public async Task ApplyLikes()
    {
        if (LikeService == null || string.IsNullOrEmpty(UserId))
            return;

        var likes = await LikeService.GetLikesByUserId(UserId);
        
        if (likes == null)
            return;

        foreach (var post in _posts)
        {
            _likes.AddRange(likes.Where(x => x.TargetId == post.Id).ToList());
        }
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