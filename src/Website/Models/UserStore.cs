using BrickAtHeart.Communities.Data;
using BrickAtHeart.Communities.Data.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace BrickAtHeart.Communities.Models
{
    public class UserStore : IUserPasswordStore<User>, IUserEmailStore<User>, IUserClaimStore<User>, IUserLoginStore<User>, IUserPhoneNumberStore<User>
    {
        public UserStore( IUserDataClient userDataClient,
                          ILookupNormalizer normalizer,
                          ILogger<UserStore> logger)
        {
            this.userDataClient = userDataClient;
            this.normalizer = normalizer;
            this.logger = logger;
        }

        public Task AddClaimsAsync(User user, IEnumerable<Claim> claims, CancellationToken cancellationToken = new())
        {
            logger.LogInformation("Entered AddClaimsAsync");

            foreach (Claim c in claims)
            {
                user.Claims.Add(c);
            }

            logger.LogInformation("Successfully Leaving AddClaimsAsync");
            return Task.CompletedTask;
        }

        public async Task AddLoginAsync(User user, UserLoginInfo login, CancellationToken cancellationToken = new())
        {
            logger.LogInformation("Entered AddLoginAsync");

            ILoginEntity loginEntity = new LoginEntity(login.LoginProvider, login.ProviderKey)
            {
                ProviderDisplayName = login.ProviderDisplayName,
                UserId = user.Id
            };

            await userDataClient.CreateLoginAsync(loginEntity, cancellationToken);

            logger.LogInformation("Successfully Leaving AddLoginAsync");
        }

        public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken = new())
        {
            logger.LogInformation("Entered CreateAsync");

            IUserEntity entity = LoadEntity(user);

            try
            {
                await userDataClient.CreateUserAsync(entity, cancellationToken);
                user.Id = entity.Id;
                logger.LogInformation("Successfully Leaving CreateAsync");
                return IdentityResult.Success;
            }
            catch (Exception e)
            {
                logger.LogWarning(e, $"Error in CreateAsync {e.HResult}, {e.Message}");

                IdentityError[] errs =
                {
                    new ()
                    {
                        Code = e.HResult.ToString(),
                        Description = e.Message
                    }
                };

                return IdentityResult.Failed(errs);
            }
        }

        public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken = new())
        {
            logger.LogInformation("Entered DeleteAsync");

            try
            {
                await userDataClient.DeleteUserAsync(LoadEntity(user), cancellationToken);
                logger.LogInformation("Successfully Leaving DeleteAsync");
                return IdentityResult.Success;
            }
            catch (Exception e)
            {
                logger.LogWarning(e, $"Error in DeleteAsync {e.HResult}, {e.Message}");

                IdentityError[] errs =
                {
                    new ()
                    {
                        Code = e.HResult.ToString(),
                        Description = e.Message
                    }
                };

                return IdentityResult.Failed(errs);
            }
        }

        public void Dispose()
        {
            logger.LogInformation("Entered Dispose");
            logger.LogInformation("Successfully Leaving Dispose");
        }

        public async Task<User> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken = new())
        {
            logger.LogInformation("Entered FindByEmailAsync");

            IUserEntity entity = await userDataClient.RetrieveUserByEmailAsync(normalizedEmail, cancellationToken);

            return LoadModel(entity);
        }

        public async Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken = new())
        {
            logger.LogInformation("Entered FindByIdAsync");

            IUserEntity entity = await userDataClient.RetrieveUserByUserIdAsync(long.Parse(userId), cancellationToken);

            return LoadModel(entity);
        }

        public async Task<User> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken = new())
        {
            logger.LogInformation("Entered FindByLoginAsync");

            IUserEntity entity = await userDataClient.RetrieveUserByLoginAsync(loginProvider, providerKey, cancellationToken);

            return LoadModel(entity);
        }

        public async Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken = new())
        {
            logger.LogInformation("Entered FindByNameAsync");

            IUserEntity entity = await userDataClient.RetrieveUserByUserNameAsync(normalizedUserName, cancellationToken);

            return LoadModel(entity);
        }

        public Task<IList<Claim>> GetClaimsAsync(User user, CancellationToken cancellationToken = new())
        {
            logger.LogInformation("Entered GetClaimsAsync");

            logger.LogInformation("Successfully Leaving GetClaimsAsync");
            return Task.FromResult(user.Claims);
        }

        public Task<string> GetEmailAsync(User user, CancellationToken cancellationToken = new())
        {
            logger.LogInformation("Entered GetEmailAsync");

            logger.LogInformation("Successfully Leaving GetEmailAsync");
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancellationToken = new())
        {
            logger.LogInformation("Entered GetEmailConfirmedAsync");

            logger.LogInformation("Successfully Leaving GetEmailConfirmedAsync");
            return Task.FromResult(user.EmailConfirmed);
        }

        public async Task<IList<UserLoginInfo>> GetLoginsAsync(User user, CancellationToken cancellationToken = new())
        {
            logger.LogInformation("Entered GetLoginsAsync");

            IList<ILoginEntity> loginEntities = await userDataClient.RetrieveLoginsByUserIdAsync(user.Id, cancellationToken);

            if (loginEntities.Any())
            {
                List<UserLoginInfo> logins = new List<UserLoginInfo>();

                foreach (ILoginEntity loginEntity in loginEntities)
                {
                    logins.Add(new(loginEntity.ProviderId, loginEntity.ProviderKey, loginEntity.ProviderDisplayName));
                }

                logger.LogInformation("Successfully Leaving GetLoginsAsync");
                return logins;
            }

            logger.LogInformation("Successfully Leaving GetLoginsAsync");
            return new List<UserLoginInfo>();
        }

        public Task<string> GetNormalizedEmailAsync(User user, CancellationToken cancellationToken = new())
        {
            logger.LogInformation("Entered GetNormalizedEmailAsync");

            logger.LogInformation("Successfully Leaving GetNormalizedEmailAsync");
            return Task.FromResult(normalizer.NormalizeEmail(user.Email));
        }

        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken = new())
        {
            logger.LogInformation("Entered GetNormalizedUserNameAsync");

            logger.LogInformation("Successfully Leaving GetNormalizedUserNameAsync");
            return Task.FromResult(user.NormalizedUserName);
        }

        public Task<string> GetPhoneNumberAsync(User user, CancellationToken cancellationToken = new())
        {
            logger.LogInformation("Entered GetPhoneNumberAsync");

            logger.LogInformation("Successfully Leaving GetPhoneNumberAsync");
            return Task.FromResult(user.PhoneNumber);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(User user, CancellationToken cancellationToken = new())
        {
            logger.LogInformation("Entered GetPhoneNumberConfirmedAsync");

            logger.LogInformation("Successfully Leaving GetPhoneNumberConfirmedAsync");
            return Task.FromResult(user.PhoneNumberConfirmed);
        }

        public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken = new())
        {
            logger.LogInformation("Entered GetPasswordHashAsync");

            logger.LogInformation("Successfully Leaving GetPasswordHashAsync");
            return Task.FromResult(string.Empty);
        }

        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken = new())
        {
            logger.LogInformation("Entered GetUserIdAsync");

            logger.LogInformation("Successfully Leaving GetUserIdAsync");
            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken = new())
        {
            logger.LogInformation("Entered GetUserNameAsync");

            logger.LogInformation("Successfully Leaving GetUserNameAsync");
            return Task.FromResult(user.DisplayName);
        }

        public Task<IList<User>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken = new())
        {
            logger.LogInformation("Entered GetUsersForClaimAsync");

            logger.LogInformation("Successfully Leaving GetUsersForClaimAsync");

            IList<User> result = new List<User>();
            return Task.FromResult(result);
        }

        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken = new())
        {
            logger.LogInformation("Entered HasPasswordAsync");

            logger.LogInformation("Successfully Leaving HasPasswordAsync");
            return Task.FromResult(false);
        }

        public Task RemoveClaimsAsync(User user, IEnumerable<Claim> claims, CancellationToken cancellationToken = new())
        {
            logger.LogInformation("Entered RemoveClaimsAsync");

            foreach (Claim claim in claims)
            {
                user.Claims.Remove(claim);
            }

            logger.LogInformation("Successfully Leaving RemoveClaimsAsync");
            return Task.CompletedTask;
        }

        public Task RemoveLoginAsync(User user, string loginProvider, string providerKey, CancellationToken cancellationToken = new())
        {
            logger.LogInformation("Entered RemoveLoginAsync");

            ILoginEntity loginEntity = new LoginEntity(loginProvider, providerKey)
            {
                UserId = user.Id
            };

            return userDataClient.DeleteLoginAsync(loginEntity, cancellationToken);
        }

        public Task ReplaceClaimAsync(User user, Claim claim, Claim newClaim, CancellationToken cancellationToken = new())
        {
            logger.LogInformation("Entered ReplaceClaimAsync");

            user.Claims.Remove(claim);
            user.Claims.Add(newClaim);

            logger.LogInformation("Successfully Leaving ReplaceClaimAsync");
            return Task.CompletedTask;
        }

        public Task SetEmailAsync(User user, string email, CancellationToken cancellationToken = new())
        {
            logger.LogInformation("Entered SetEmailAsync");

            user.Email = email;

            logger.LogInformation("Successfully Leaving SetEmailAsync");
            return Task.CompletedTask;
        }

        public Task SetEmailConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken = new())
        {
            logger.LogInformation("Entered SetEmailConfirmedAsync");

            user.EmailConfirmed = true;

            logger.LogInformation("Successfully Leaving SetEmailConfirmedAsync");
            return Task.CompletedTask;
        }

        public Task SetNormalizedEmailAsync(User user, string normalizedEmail, CancellationToken cancellationToken = new())
        {
            logger.LogInformation("Entered SetNormalizedEmailAsync");

            user.NormalizedEmail = normalizedEmail;

            logger.LogInformation("Successfully Leaving SetNormalizedEmailAsync");
            return Task.CompletedTask;
        }

        public Task SetNormalizedUserNameAsync(User user, string normalizedUserName, CancellationToken cancellationToken = new())
        {
            logger.LogInformation("Entered SetNormalizedUserNameAsync");

            user.NormalizedUserName = normalizedUserName;

            logger.LogInformation("Successfully Leaving SetNormalizedUserNameAsync");
            return Task.CompletedTask;
        }

        public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken = new())
        {
            logger.LogInformation("Entered SetPasswordHashAsync");

            logger.LogInformation("Successfully Leaving SetPasswordHashAsync");
            return Task.CompletedTask;
        }

        public Task SetPhoneNumberAsync(User user, string phoneNumber, CancellationToken cancellationToken = new())
        {
            logger.LogInformation("Entered SetPhoneNumberAsync");

            user.PhoneNumber = phoneNumber;

            logger.LogInformation("Successfully Leaving SetPhoneNumberAsync");
            return Task.CompletedTask;
        }

        public Task SetPhoneNumberConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken = new())
        {
            logger.LogInformation("Entered SetPhoneNumberConfirmedAsync");

            user.PhoneNumberConfirmed = confirmed;

            logger.LogInformation("Successfully Leaving SetPhoneNumberConfirmedAsync");
            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken = new())
        {
            logger.LogInformation("Entered SetUserNameAsync");

            user.UserName = userName;

            logger.LogInformation("Successfully Leaving SetUserNameAsync");
            return Task.CompletedTask;
        }

        public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken = new())
        {
            logger.LogInformation("Entered UpdateAsync");

            IUserEntity entity = LoadEntity(user);

            try
            {
                await userDataClient.UpdateUserAsync(entity, cancellationToken);
                logger.LogInformation("Successfully Leaving UpdateAsync");
                return IdentityResult.Success;
            }
            catch (Exception e)
            {
                logger.LogWarning(e, "Error in UpdateAsync");

                IdentityError[] errs =
                {
                    new ()
                    {
                        Code = "1",
                        Description = e.Message
                    }
                };

                return IdentityResult.Failed(errs);
            }
        }

        private IUserEntity LoadEntity(User user)
        {
            return new UserEntity(user.DisplayName)
            {
                Id = user.Id,
                City = user.City,
                Country = user.Country,
                DateOfBirth = user.DateOfBirth,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                GivenName = user.GivenName,
                IsActive = user.IsActive,
                IsApproved = user.IsApproved,
                NormalizedEmail = normalizer.NormalizeEmail(user.Email),
                PhoneNumber = user.PhoneNumber,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                PostalCode = user.PostalCode,
                Region = user.Region,
                StreetAddressLine1 = user.StreetAddressLine1,
                StreetAddressLine2 = user.StreetAddressLine2,
                SurName = user.SurName,
            };
        }

        private User LoadModel(IUserEntity entity)
        {
            if (entity == null)
            {
                return null;
            }

            return new User(entity.DisplayName)
            {
                Id = entity.Id,
                City = entity.City,
                Country = entity.Country,
                DateOfBirth = entity.DateOfBirth,
                Email = entity.Email,
                EmailConfirmed = entity.EmailConfirmed,
                GivenName = entity.GivenName,
                IsActive = entity.IsActive,
                IsApproved = entity.IsApproved,
                NormalizedEmail = entity.NormalizedEmail,
                NormalizedUserName = entity.DisplayName,
                PhoneNumber = entity.PhoneNumber,
                PhoneNumberConfirmed = entity.PhoneNumberConfirmed,
                PostalCode = entity.PostalCode,
                Region = entity.Region,
                StreetAddressLine1 = entity.StreetAddressLine1,
                StreetAddressLine2 = entity.StreetAddressLine2,
                SurName = entity.SurName,
                UserName = entity.DisplayName
            };
        }

        private readonly IUserDataClient userDataClient;
        private readonly ILookupNormalizer normalizer;
        private readonly ILogger<UserStore> logger;
    }
}