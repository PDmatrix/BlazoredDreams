using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using BlazoredDreams.Application.Posts.Models;
using BlazoredDreams.API.IntegrationTest.Infrastructure;
using FluentAssertions;
using Xunit;

namespace BlazoredDreams.API.IntegrationTest.Post
{
	public class QueryTest : BaseTest
	{
		public QueryTest(TestFactory factory) : base(factory)
		{
		}
		
		[Theory]
		[InlineData("api/posts")]
		public async Task GetHttpRequest(string url)
		{
			var allPosts = await HttpHandler.CallAsync<IEnumerable<PostPreviewDto>>(HttpHandler.CreateHttpRequestMessage(HttpMethod.Get, url));
			allPosts.Should().NotBeNull();
		}
	}
}