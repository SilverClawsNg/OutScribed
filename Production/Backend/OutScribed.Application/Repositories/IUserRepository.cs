using OutScribed.Application.Features.UserManagement.Commands.LoginUser;
using OutScribed.Application.Features.UserManagement.Common;
using OutScribed.Application.Features.UserManagement.Queries.LoadActivities;
using OutScribed.Application.Features.UserManagement.Queries.LoadAdmins;
using OutScribed.Application.Features.UserManagement.Queries.LoadUser;
using OutScribed.Application.Features.UserManagement.Queries.LoadUserProfile;
using OutScribed.Domain.Enums;
using OutScribed.Domain.Models.UserManagement.Entities;
using OutScribed.Application.Features.UserManagement.Queries.LoadUsernameProfile;
using OutScribed.Application.Features.UserManagement.Queries.LoadWriters;

namespace OutScribed.Application.Repositories
{
    public interface IUserRepository
    {
        /// <summary>
        /// Checks if a username exists
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Task<bool> CheckIfUsernameExists(string username);

        /// <summary>
        /// Get account by Id
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        Task<Account?> GetAccountById(Guid accountId);

        /// <summary>
        /// Check if email address exists
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        Task<bool> CheckIfEmailAddressExists(string emailAddress);

        /// <summary>
        /// Checks if phone number already exists
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        Task<bool> CheckIfPhoneNumberExists(string phoneNumber);

        /// <summary>
        /// Loads user details upon login
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        Task<LoginUserResponse?> LoadLoginUserResponse(Guid accountId);

        /// <summary>
        /// Loads an account from username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Task<Account?> GetAccountByUsername(string username);

        /// <summary>
        /// Gets a valid user from refresh token
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        Task<Account?> GetValidAccountFromRefreshToken(string refreshToken);

        /// <summary>
        /// Gets a user from refresh token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<Account?> GetUserFromRefreshToken(string token);

        /// <summary>
        /// Loads a user's profile
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        Task<LoadUserProfileQueryResponse?> LoadUserProfile(Guid id, Guid accountId);

        /// <summary>
        /// Loads a user's profile by its username
        /// </summary>
        /// <param name="id"></param>
        /// <param name="accountId"></param>
        /// <returns></returns>
        Task<LoadUsernameProfileQueryResponse?> LoadUsernameProfile(string id, Guid accountId);

        /// <summary>
        /// Loads all admins
        /// </summary>
        /// <param name="role"></param>
        /// <param name="country"></param>
        /// <param name="sort"></param>
        /// <param name="username"></param>
        /// <param name="pointer"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        Task<LoadAdminsQueryResponse> LoadAllAdmins(RoleTypes? role,
          Countries? country, SortTypes? sort, string? username, int pointer,
         int size);

        /// <summary>
        /// Gets a user mailing list
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        Task<List<MailingList>?> GetMailingList(Guid accountId);

        /// <summary>
        /// Gets a username by Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<string?> GetUsernameById(Guid userId);


        /// <summary>
        /// Load user activities
        /// </summary>
        /// <param name="AccountId"></param>
        /// <param name="type"></param>
        /// <param name="sort"></param>
        /// <param name="pointer"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        Task<LoadActivitiesQueryResponse> LoadActivities(Guid AccountId, ActivityTypes? type, bool? hasRead, string? keyword, SortTypes? sort, int pointer, int size);

        /// <summary>
        /// Load user by username
        /// </summary>
        /// <param name="id"></param>
        /// <param name="accountId"></param>
        /// <returns></returns>
        Task<LoadUserQueryResponse?> LoadUser(string id, Guid accountId);

        /// <summary>
        /// Load recent writers
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        Task<List<WriterSummary>> LoadMostRecentWriters(Guid accountId);

        /// <summary>
        /// Load recent writers
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        Task<List<WriterSummary>> LoadMostPopularWriters(Guid accountId);

        /// <summary>
        /// Loads page of writers
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="country"></param>
        /// <param name="sort"></param>
        /// <param name="username"></param>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        Task<LoadWritersQueryResponse> LoadAllWriters(Guid accountId, Countries? country, SortTypes? sort, string? username, int v1, int v2);
    }


}
