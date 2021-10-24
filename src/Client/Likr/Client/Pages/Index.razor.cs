using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Likr.Client.Dtos;
using Likr.Client.Extensions;
using Likr.Client.Helpers;
using Likr.Client.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace Likr.Client.Pages
{
    public partial class Index : ComponentBase
    {
        [Inject]
        public IHttpService HttpService { get; set; }
        
        [Inject]
        public IAccessTokenProvider TokenProvider { get; set; }
        
        [Inject]
        public AuthenticationStateProvider AuthStateProvider { get; set; }

        private List<PostDto> _posts = new List<PostDto>();
        private string? _userId;

        protected override async Task OnInitializedAsync()
        {
            HttpResponseWrapper<List<PostDto>?> wrapper = await HttpService.Get<List<PostDto>>("/api/v1/p/Posts");

            _posts = wrapper.Response!;

            var user = (await AuthStateProvider.GetAuthenticationStateAsync()).User;
            _userId = user.GetUserId();
        }

        private string body = "";

        private async Task CreatePost()
        {
            var tokenResult = await TokenProvider.RequestAccessToken();
            string accessToken = string.Empty;

            if (tokenResult.TryGetToken(out var token))
                accessToken = token.Value;

            var post = new CreatePostDto
            {
                Body = body,
                UserId = Guid.Parse(_userId)
            };

            var response = await HttpService.Create<CreatePostDto, PostDto>("/api/v1/p/Posts", post, accessToken);
        }
    }
}