using Likr.Client.Dtos;
using Likr.Client.Services;
using Microsoft.AspNetCore.Components;

namespace Likr.Client.Pages;

public partial class Post : ComponentBase
{
    [Parameter]
    public Guid Id { get; set; }
        
    [Inject]
    public IPostService? PostService { get; set; }

    private PostDto? _post;

    protected override async Task OnParametersSetAsync()
    {
        if (PostService == null)
            return;

        _post = await PostService.GetById(Id);
    }
}