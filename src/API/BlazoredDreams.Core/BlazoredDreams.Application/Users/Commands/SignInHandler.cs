using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using BlazoredDreams.Application.Interfaces;
using Dapper;
using MediatR;

namespace BlazoredDreams.Application.Users.Commands
{
	public class SignInCommand : IRequest
	{
		public string UserId { get; set; }
		public string AccessToken { get; set; }
		public string UserInfoEndpoint { get; set; }
	}
	
	// ReSharper disable once UnusedMember.Global
	public class SignInHandler : AsyncRequestHandler<SignInCommand>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IHttpClientFactory _httpClientFactory;
		
		public SignInHandler(IUnitOfWorkFactory unitOfWorkFactory, IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
			_unitOfWork = unitOfWorkFactory.Create();
		}
		
		protected override async Task Handle(SignInCommand request, CancellationToken cancellationToken)
		{
			const string sql =
				@"
				INSERT INTO identity_user (identifier)
				VALUES (@userId)
				ON CONFLICT (identifier) DO NOTHING
				   RETURNING identifier
				";
			var identifier = await _unitOfWork.Connection.QuerySingleOrDefaultAsync<string>(sql, request, _unitOfWork.Transaction);
			if(string.IsNullOrWhiteSpace(identifier))
				return;

			await UpdateUser(request, cancellationToken);
		}

		private async Task UpdateUser(SignInCommand request, CancellationToken ct)
		{
			var userInfo = await GetUserInfo(request.UserInfoEndpoint, request.AccessToken, ct);
			var sqlParams = new
			{
				Username = userInfo.Nickname,
				userInfo.Email,
				request.UserId
			};
			const string sql =
				@"
				UPDATE identity_user
				SET username = @username, email = @email
				WHERE identifier = @userId
				";
			await _unitOfWork.Connection.ExecuteAsync(sql, sqlParams, _unitOfWork.Transaction);
		}

		private async Task<UserInfo> GetUserInfo(string endpoint, string accessToken, CancellationToken ct)
		{
			var client = _httpClientFactory.CreateClient();
			client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(accessToken);
			var response = await client.GetAsync(new Uri(endpoint), ct);
			
			UserInfo userInfo = null;
			if (response.IsSuccessStatusCode)
				userInfo = await response.Content.ReadAsAsync<UserInfo>(ct);
			
			return userInfo;
		}
		
		private class UserInfo
		{
			public string Nickname { get; set; }
			public string Email { get; set; }
		}
	}
}