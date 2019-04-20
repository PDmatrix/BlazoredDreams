using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using BlazoredDreams.Application.Posts.Models;
using BlazoredDreams.API.Features.Post;
using BlazoredDreams.API.IntegrationTest.Infrastructure;
using FluentAssertions;
using Xunit;

namespace BlazoredDreams.API.IntegrationTest.Post
{
	public class CommandTest : BaseTest
	{
		public CommandTest(TestFactory factory) : base(factory)
		{
		}

		[Theory]
		[InlineData("api/posts")]
		public async Task AddPostTheory(string url)
		{
			var createdPostObject = await AddPost(url);
			var createdPost = await HttpHandler.CallAsync<PostDto>(HttpHandler.CreateHttpRequestMessage(HttpMethod.Get, $"{url}/{createdPostObject.Id}"));
			createdPost.Content.Should().Be("<p>foo</p>");
			createdPost.Title.Should().Be("foo");
		}
		
		private class CreatedPost
		{
			public int Id { get; set; }
		}

		private Task<CreatedPost> AddPost(string url)
		{
			var createPostMessage = HttpHandler.CreateHttpRequestMessage(HttpMethod.Post, 
				new PostRequest {Title = "foo", Excerpt = "foo", DreamId = 1}, url);
			return HttpHandler.CallAsync<CreatedPost>(createPostMessage);
		}
		
		[Theory]
		[InlineData("api/posts")]
		public async Task UpdatePostTheory(string url)
		{
			var createdPostObject = await AddPost(url);
			var updatePostMessage = HttpHandler.CreateHttpRequestMessage(HttpMethod.Put,
				new PostRequest {Title = "bar", Excerpt = "bar", DreamId = 1}, $"{url}/{createdPostObject.Id}");
			await HttpHandler.CallAsync(updatePostMessage);
			
			var updatedPost = await HttpHandler.CallAsync<PostDto>(HttpHandler.CreateHttpRequestMessage(HttpMethod.Get, $"{url}/{createdPostObject.Id}"));
			updatedPost.Content.Should().Be("<p>bar</p>");
			updatedPost.Title.Should().Be("bar");
		}
		
		[Theory]
		[InlineData("api/posts")]
		public async Task DeletePostTheory(string url)
		{
			var createdPostObject = await AddPost(url);
			var deletePostMessage = HttpHandler.CreateHttpRequestMessage(HttpMethod.Delete, $"{url}/{createdPostObject.Id}");
			await HttpHandler.CallAsync(deletePostMessage);

			var allPosts = await HttpHandler.CallAsync<IEnumerable<PostDto>>(
				HttpHandler.CreateHttpRequestMessage(HttpMethod.Get, url));
			allPosts.Should().BeEmpty();
		}
	}
}