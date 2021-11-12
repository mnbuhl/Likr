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

    [CascadingParameter(Name = "UserId")]
    public string? UserId { get; set; } = "Default Value";
    
    [Parameter]
    public Guid Id { get; set; }

    private PostDto? _post;

    protected override async Task OnParametersSetAsync()
    {
        if (PostService == null || CommentService == null)
            return;

        _post = await PostService.GetById(Id);
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