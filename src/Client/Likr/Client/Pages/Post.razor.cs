using Likr.Client.Dtos;
using Likr.Client.Extensions;
using Likr.Client.Services;
using Microsoft.AspNetCore.Components;

namespace Likr.Client.Pages;

public partial class Post : ComponentBase
{
    [Inject]
    public IPostService? PostService { get; set; }
    
    [Inject]
    public ICommentService? CommentService { get; set; }
    
    [Inject]
    public NavigationManager? NavigationManager { get; set; }
    
    [Inject]
    public AuthService? AuthService { get; set; }
    
    [Parameter]
    public Guid Id { get; set; }

    private PostDto? _post;
    private readonly Dictionary<string ,List<CommentDto>> _comments = new();
    private string _userId = "Default Value";

    protected override async Task OnParametersSetAsync()
    {
        if (PostService == null || CommentService == null || AuthService == null)
            return;

        _post = await PostService.GetById(Id);
        var user = await AuthService.GetCurrentUser();

        if (user != null)
            _userId = user.GetUserId();
        
        if (_post == null)
            return;

        foreach (var comment in _post.Comments)
        {
            if (comment.CommentsCount > 0)
            {
                var commentWithNested = await CommentService.GetCommentById(Guid.Parse(comment!.Id));
                _comments[comment.Id] = commentWithNested!.Comments!.ToList();
            }
        }
    }
    
    public async Task OnCommentCreated(CommentDto commentDto)
    {
        if (_post == null || commentDto.Id == null)
            return;

        var comment = await CommentService?.GetCommentById(Guid.Parse(commentDto.Id))!;
        comment.CommentsCount = 0;
        
        _post.Comments.Add(comment);
    }
    
    public async Task OnPostDeleted(PostDto postDto)
    {
        if (PostService == null || NavigationManager == null)
            return;
        
        await PostService.DeletePost(Guid.Parse(postDto.Id));
        NavigationManager.NavigateTo("/");
    }
    
    public async Task OnCommentDeleted(CommentDto commentDto)
    {
        if (CommentService == null || _post == null)
            return;

        bool deleted = await CommentService.DeleteComment(Guid.Parse(commentDto.Id!));

        if (deleted)
        {
            _post.Comments.Remove(_post.Comments.First(x => x.Id == commentDto.Id));
        }
    }
}